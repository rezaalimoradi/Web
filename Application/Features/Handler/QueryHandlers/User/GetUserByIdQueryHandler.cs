using Application.Common;
using Application.Features.Request.Queries.User;
using Domain.User.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.User;

namespace Application.Features.Handler.QueryHandlers.User
{
    public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, ResultModel<List<UserDto>>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserByIdQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResultModel<List<UserDto>>> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return ResultModel<List<UserDto>>.Fail("درخواست نمی‌تواند خالی باشد.");

            if (request.UserId == null)
                return ResultModel<List<UserDto>>.Fail("شناسه کاربر نامتعبر است.");

            try
            {
                var users = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (users == null)
                    return ResultModel<List<UserDto>>.Fail("کاربر یافت نشد.");

                return ResultModel<List<UserDto>>.Success(users);
            }
            catch (Exception ex)
            {
                return ResultModel<List<UserDto>>.Fail("خطا در دریافت لیست کاربران");
            }
        }
    }
}