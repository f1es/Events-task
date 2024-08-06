using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Validators;

public class ImageValidation : AbstractValidator<IFormFile>
{
    public ImageValidation()
    {
        RuleFor(i => i.Length)
            .LessThan(2000000).WithMessage("The image cannot be larger than 2 mb");

        RuleFor(i => i.ContentType)
            .Must(type => 
            type.Equals("image/png") ||
            type.Equals("image/jpeg") ||
            type.Equals("image/jpg")).WithMessage("The uploaded file must be an image");
            
    }
}
