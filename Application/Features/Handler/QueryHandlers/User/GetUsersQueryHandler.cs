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
    public class GetUsersQueryHandler
        : IRequestHandler<GetUsersQuery, ResultModel<List<UserDto>>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResultModel<List<UserDto>>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return ResultModel<List<UserDto>>.Fail("درخواست نمی‌تواند خالی باشد.");

            try
            {
                var users = await _userManager.Users.ToListAsync();

                if (users == null || !users.Any())
                    return ResultModel<List<UserDto>>.Success(new List<UserDto>());

                // 📌 اینجاست که Mapper استفاده میشه
                var dto = _mapper.Map<List<UserDto>>(users);

                return ResultModel<List<UserDto>>.Success(dto);
            }
            catch
            {
                return ResultModel<List<UserDto>>.Fail("خطا در دریافت لیست کاربران");
            }
        }
    }
}
