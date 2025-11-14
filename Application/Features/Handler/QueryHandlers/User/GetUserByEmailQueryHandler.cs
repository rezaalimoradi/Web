using Application.Common;
using Application.Features.Request.Queries.User;
using AutoMapper;
using CMS.Domain.Common;
using Domain.User.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.User;

namespace Application.Features.Handler.QueryHandlers.User
{
    public class GetUserByEmailQueryHandler
        : IRequestHandler<GetUserByEmailQuery, ResultModel<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResultModel<UserDto>> Handle(
            GetUserByEmailQuery request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
                return ResultModel<UserDto>.Fail("ایمیل نمی‌تواند خالی باشد.");

            try
            {
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

                if (user == null)
                    return ResultModel<UserDto>.Fail("کاربر یافت نشد.");

                var dto = _mapper.Map<UserDto>(user);

                return ResultModel<UserDto>.Success(dto);
            }
            catch
            {
                return ResultModel<UserDto>.Fail("خطا در جستجوی کاربر.");
            }
        }
    }
}
