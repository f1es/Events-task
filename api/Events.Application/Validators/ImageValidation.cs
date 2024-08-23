using Events.Domain.Shared.DTO.Request;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Validators;

public class ImageValidation : AbstractValidator<ImageRequestDto>
{
    public ImageValidation()
    {
        RuleFor(i => i.Image.Length)
            .LessThan(2 * 1024 * 1024).WithMessage("The image cannot be larger than 2 mb");

        RuleFor(i => i.Image.ContentType)
            .Must(type =>
            type.Equals("image/png") ||
            type.Equals("image/jpeg") ||
            type.Equals("image/jpg")).WithMessage("The uploaded file must be an image");

    }
}
