using CMS.Application.Catalog.Queries;
using CMS.Application.Medias.Commands;
using FluentValidation;

namespace CMS.Application.Catalog.Validators
{
    public class UploadMediaCommandValidator : AbstractValidator<UploadMediaCommand>
    {
        public UploadMediaCommandValidator()
        {
        }
    }
}
