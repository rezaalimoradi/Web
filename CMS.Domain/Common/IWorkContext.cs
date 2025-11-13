namespace CMS.Domain.Common
{
    /// <summary>
    /// Provides contextual information about the current request, user, and environment.
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets the unique identifier of the currently authenticated user, if available.
        /// </summary>
        long? CurrentUserId { get; }

        /// <summary>
        /// Gets the ISO code of the current language (e.g. "en", "fa").
        /// </summary>
        Task<string?> CurrentLanguage();

        /// <summary>
        /// Gets the currency code being used in the current context (e.g. "USD", "IRR").
        /// </summary>
        string? CurrentCurrency { get; }

        /// <summary>
        /// Gets the current tax percentage or rate applicable in the context (nullable).
        /// </summary>
        decimal? CurrentTaxRate { get; }
    }
}
