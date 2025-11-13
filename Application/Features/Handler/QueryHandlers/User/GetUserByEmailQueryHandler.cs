// Application/Features/Handler/QueryHandlers/User/GetUserByEmailQueryHandler.cs
using Application.Common;
using Application.Features.Request.Queries.User;
using CMS.Domain.Common;
using Domain.User.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.User;

namespace Application.Features.Handler.QueryHandlers.User
{
    public class GetUserByEmailQueryHandler
        : IRequestHandler<GetUserByEmailQuery, ResultModel<List<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public GetUserByEmailQueryHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ResultModel<List<UserDto>>> Handle(
            GetUserByEmailQuery request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return ResultModel<List<UserDto>>.Fail("ایمیل نمی‌تواند خالی باشد.");

            try
            {
                var users = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

                var userGroupPermissionsRepository = _unitOfWork.GetRepository<Permission>();

                if (users == null)
                    return ResultModel<List<UserDto>>.Fail("کاربر یافت نشد.");

                return ResultModel<UserDto>.Success(users);
            }
            catch (Exception)
            {
                return ResultModel<List<UserDto>>.Fail("خطا در جستجوی کاربر.");
            }
        }
    }
}