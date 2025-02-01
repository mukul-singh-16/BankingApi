using BankingApp.Core.DTOs;
using FluentValidation;

namespace BankingApp.Application.Validators
{
   public class UserDtoValidator : AbstractValidator<UserDto>
   {
       public UserDtoValidator()
       {
           RuleFor(user => user.Id)
               .NotEmpty().WithMessage("User ID is required.");

           RuleFor(user => user.Username)
               .NotEmpty().WithMessage("Username is required.")
               .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
               .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");
       }
   }

   public class LoginDtoValidator : AbstractValidator<LoginDto>
   {
       public LoginDtoValidator()
       {
           RuleFor(login => login.Username)
               .NotEmpty().WithMessage("Username is required.")
               .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
               .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

           RuleFor(login => login.Password)
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(3).WithMessage("Password must be at least 6 characters long.");
       }
   }
}
