using Application.Common;
using Application.Features.Request.Queries.User;
using AutoMapper;
using Domain.User.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos.User;

namespace Application.Features.Handler.QueryHandlers.User
{
    public class GetUserByIdQueryHandler
        : IRequestHandler<GetUserByIdQuery, ResultModel<UserDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResultModel<UserDto>> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return ResultModel<UserDto>.Fail("درخواست نمی‌تواند خالی باشد.");

            if (request.UserId == Guid.Empty)
                return ResultModel<UserDto>.Fail("شناسه کاربر معتبر نیست.");

            try
            {
                var user = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

                if (user == null)
                    return ResultModel<UserDto>.Fail("کاربر یافت نشد.");

                var dto = _mapper.Map<UserDto>(user);

                return ResultModel<UserDto>.Success(dto);
            }
            catch
            {
                return ResultModel<UserDto>.Fail("خطا در دریافت اطلاعات کاربر");
            }
        }
    }
}
