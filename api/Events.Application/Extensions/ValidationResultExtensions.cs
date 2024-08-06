using FluentValidation.Results;

namespace Events.Application.Extensions;

public static class ValidationResultExtensions
{
	public static string GetMessage(this ValidationResult validationResult)
	{
		var message = string.Empty;

		foreach(var error in validationResult.Errors)
		{
			message += error.ErrorMessage;
			
			if (error != validationResult.Errors.Last())
				message += ", ";
		}

		return message;
	}
}
