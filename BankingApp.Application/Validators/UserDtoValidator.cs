using BankingApp.Core.DTOs;
using BankingApp.Core.Interfaces;
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
            .NotNull().WithMessage("Username cannot be null")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
            .Matches(@"^\S*$").WithMessage("Username cannot contain spaces.");

           RuleFor(login => login.Password)
               .NotEmpty().WithMessage("Password is required.")
               .NotNull().WithMessage("Username cannot be null")
               .Matches(@"^\S*$").WithMessage("Password cannot contain spaces.")
               .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
       }
   }  


   public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
   {
       public UserRegisterDtoValidator()
        {
        

            RuleFor(register => register.username)
                .NotEmpty().WithMessage("Username is required.")
                .NotNull().WithMessage("Username cannot be null")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.")
                .Matches(@"^\S*$").WithMessage("Username cannot contain spaces.");
                

            RuleFor(register => register.password)
                .NotEmpty().WithMessage("Password is required.")
                .NotNull().WithMessage("Password cannot be null")
                .Matches(@"^\S*$").WithMessage("Password cannot contain spaces.")
                .MinimumLength(4).WithMessage("Password must be at least 6 characters long.");

            RuleFor(register => register.emailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .NotNull().WithMessage("Email address cannot be null.")
                .EmailAddress().WithMessage("Invalid email format.")
                .Matches(@"^\S*$").WithMessage("Email address cannot contain spaces.");
        }
    
   }

}
