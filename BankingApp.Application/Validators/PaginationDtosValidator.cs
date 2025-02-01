using FluentValidation;
using BankingApp.Application.DTOs;

public class PaginationDtosValidator : AbstractValidator<PaginationDtos>
{
   public PaginationDtosValidator()
   {
       RuleFor(x => x.page)
           .GreaterThan(0).WithMessage("Page number must be greater than zero.");

       RuleFor(x => x.pageSize)
           .GreaterThan(0).WithMessage("Page size must be greater than zero.")
           .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");
   }
}
