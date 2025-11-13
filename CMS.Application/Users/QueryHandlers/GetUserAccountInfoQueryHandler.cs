using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Orders.Carts.Dtos;
using CMS.Application.Tenants;
using CMS.Application.Users.Queries;
using CMS.Domain.Common;
using CMS.Domain.Orders;
using CMS.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Users.QueryHandlers
{
    public class GetUserAccountInfoQueryHandler : IAppRequestHandler<GetUserAccountInfoQuery, AccountViewModel>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public GetUserAccountInfoQueryHandler(
            UserManager<AppUser> userManager,
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<AccountViewModel> Handle(GetUserAccountInfoQuery request, CancellationToken cancellationToken)
        {
            // 🧩 دریافت کاربر از Identity
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
                throw new Exception("کاربر یافت نشد.");

            // 🧩 دریافت سفارشات کاربر
            var orderRepo = _unitOfWork.GetRepository<Order>();
            var orders = await orderRepo.GetAllAsync(
                predicate: o => o.CustomerId == request.UserId,
                func: q => q.Include(o => o.OrderItems)
                            .Include(o => o.ShippingAddress)
                            .Include(o => o.BillingAddress)
            );

            // 🧩 مدل خروجی
            var model = new AccountViewModel
            {
                UserId = user.Id.ToString(),
                DisplayName = user.UserName ?? user.Email ?? "کاربر",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

                BillingAddress = orders
                .Select(o => o.BillingAddress)
                .Where(a => a != null)
                .Select(a => new AddressViewModel
                {
                    FullName = user.UserName ?? user.Email,
                    Phone = a.Phone
                })
                .FirstOrDefault() ?? new AddressViewModel(),

                            ShippingAddress = orders
                .Select(o => o.ShippingAddress)
                .Where(a => a != null)
                .Select(a => new AddressViewModel
                {
                    FullName = user.UserName ?? user.Email,
                    Phone = a.Phone
                })
                .FirstOrDefault() ?? new AddressViewModel(),


                Orders = orders.Select(o => new OrderSummaryViewModel
                {
                    Id = o.Id,
                    OrderDate = o.CreatedOn.UtcDateTime,
                    Status = o.OrderStatus.ToString(),
                    TotalPrice = o.OrderTotal,
                    TotalItems = o.OrderItems.Sum(i => i.Quantity)
                }).OrderByDescending(o => o.OrderDate).ToList()
            };

            return model;
        }
    }
}
