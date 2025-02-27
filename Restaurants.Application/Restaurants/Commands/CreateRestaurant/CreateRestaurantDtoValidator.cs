using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Nigerian", "Mexican", "American"];

        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name).Length(3, 100);
            RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(dto => dto.Category)
			//.Must(category => validCategories.Contains(category)) // lambda version
			.Must(validCategories.Contains) // or directly feed the incoming argument
			.WithMessage("Invalid category. Plese choose from the valid categories");
			RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please provide a valid email address");
			RuleFor(dto => dto.PostalCode)
			.Matches(@"^\d{2}-\d{3}$")
			.WithMessage("Please provide a valid postal code in the format XX-XXX");
			RuleFor(dto => dto.ContactNumber).Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Please provide a valid phone number");
        }
    }
}
