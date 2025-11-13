using Refit;
using Shared.Dtos.Account;
using Web.Common;


namespace Web
{
    /// <summary>
    /// رابط API برای ارتباط با سرویس احراز هویت و حساب کاربری
    /// </summary>
    public interface IHasibApi
    {
        #region User Endpoints

        /// <summary>
        /// دریافت کاربر با شناسه
        /// </summary>
        [Get("/api/account/user/{userId}")]
        Task<ApiResponse<ResultModel<List<UserDto>>>> GetUserByIdAsync(Guid userId);

        /// <summary>
        /// دریافت لیست کاربران
        /// </summary>
        [Get("/api/account/user")]
        Task<ApiResponse<ResultModel<List<UserDto>>>> GetUsersAsync();

        #endregion

        #region Auth Endpoints


        [Post("/api/auth/login")]
        Task<ApiResponse<LoginResponse>> LoginAsync([Body] LoginRequest request);


        /// <summary>
        /// تنظیم کوکی احراز هویت در مرورگر
        /// </summary>
        [Post("/api/auth/set-cookie")]
        Task<ApiResponse<object>> SetAuthCookieAsync([Body] SetCookieRequestDto request);

        #endregion
    }
}
