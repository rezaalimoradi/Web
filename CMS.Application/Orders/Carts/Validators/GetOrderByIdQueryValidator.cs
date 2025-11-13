using FluentValidation;

namespace CMS.Application.Orders.Orders.Validators
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("شناسه سفارش باید بزرگتر از صفر باشد.");

            // CustomerIdentifier اختیاری است اما اگر پر شده باشد باید:
            // - یا قابل تبدیل به long باشد (شناسه کاربر)
            // - یا اگر از الگوی guest- استفاده می‌کنید، اجازه دهید (مثال زیر فقط تبدیل به long را می‌پذیرد)
            RuleFor(x => x.CustomerIdentifier)
                .Must(ci => string.IsNullOrWhiteSpace(ci) || long.TryParse(ci, out _))
                .WithMessage("شناسه مشتری باید یا خالی باشد یا یک عدد معتبر (یا در صورت نیاز الگوی guest- را هندل کنید).");
        }
    }
}
