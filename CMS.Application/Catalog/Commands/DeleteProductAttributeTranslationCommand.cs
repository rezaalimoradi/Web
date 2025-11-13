using CMS.Application.Interfaces.Messaging.Requests;
using CMS.Application.Models.Common;

namespace CMS.Application.Catalog.Commands
{
    public record DeleteProductAttributeTranslationCommand(
        long ProductId,   // برای پیدا کردن محصول
        long AttributeId, // برای پیدا کردن Attribute داخل محصول
        long LanguageId   // برای پیدا کردن Translation داخل Attribute
    ) : IAppRequest<ResultModel>;
}
