using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Nigerian", "Mexcian", "American"];

        public CreateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name).Length(3, 100);
            RuleFor(dto => dto.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(dto => dto.Category)
                .Must(category => validCategories.Contains(category))
                .WithMessage("Invalid categories. Please choose from the valid categories");
            //RuleFor(dto => dto.Category).NotEmpty().WithMessage("Insert a valid category");
            RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please provide a valid email address");
            RuleFor(dto => dto.PostalCode).Matches(@"^\d{2}\d{3}$").WithMessage("Please provide a valid postal code (xx-xxx)");
            RuleFor(dto => dto.ContactNumber).Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Please provide a valid phone number");
        }
    }
}
