using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Users.Commands;
using CMS.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace CMS.Application.Users.CommandHandlers
{
    public class UpdateUserProfileCommandHandler : IAppRequestHandler<UpdateUserProfileCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserProfileCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new Exception("کاربر یافت نشد.");

            user.UserName = request.FirstName;
            user.UserName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.UserName = request.Email; // در صورت نیاز به هماهنگی با Identity

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("خطا در بروزرسانی اطلاعات کاربر.");

            return true;
        }
    }
}
