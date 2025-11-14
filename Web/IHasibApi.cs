using Refit;
using Shared.Dtos.User;
using Web.Common;

namespace Web
{
    public interface IHasibApi
    {
        #region User Endpoints

        /// دریافت لیست کاربران
        [Get("/api/user/all")]
        Task<ApiResponse<ResultModel<List<UserDto>>>> GetUsersAsync();

        /// دریافت کاربر با شناسه
        [Get("/api/user/{id}")]
        Task<ApiResponse<UserDto>> GetUserByIdAsync(Guid id);

        #endregion

        #region Auth

        [Post("/api/auth/login")]
        Task<ApiResponse<LoginResponse>> LoginAsync([Body] LoginRequest request);

        [Post("/api/auth/set-cookie")]
        Task<ApiResponse<object>> SetAuthCookieAsync([Body] SetCookieRequestDto request);

        #endregion
    }
}
