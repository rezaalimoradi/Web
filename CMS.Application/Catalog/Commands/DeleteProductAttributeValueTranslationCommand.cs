using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public record DeleteProductAttributeValueTranslationCommand(
        long ProductId,                // برای پیدا کردن محصول
        long AttributeValueTranslationId // برای پیدا کردن translation داخل محصول
    ) : IAppRequest<ResultModel>;
}
