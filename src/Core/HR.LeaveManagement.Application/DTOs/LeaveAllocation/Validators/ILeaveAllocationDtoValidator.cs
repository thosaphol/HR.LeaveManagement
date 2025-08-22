using System;
using FluentValidation;
using HR.LeaveManagement.Application.Persistance.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator:AbstractValidator<ILeaveAllocationDto>
    {
        private ILeaveAllocationRepository _leaveAllocationRepository;
        public ILeaveAllocationDtoValidator(ILeaveAllocationRepository leaveAllocationRepository)
       {
        _leaveAllocationRepository = leaveAllocationRepository;
            RuleFor(x => x.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(async (id, cancellation) =>
            {
                var leaveTypeExists = await _leaveAllocationRepository.Exist(id);
                return !leaveTypeExists;

            })
                .NotNull().WithMessage("{PropertyName} does not exist.");

            RuleFor(x => x.NumberOfDays)
                // .NotNull().WithMessage("{PropertyName} must be present")
                .GreaterThan(0).WithMessage("{PropertyName} must be befor {ComparisonValue}.")
                // .LessThan(100).WithMessage("{PropertyName} must be less than {ComparisonValue}.")
                ;

            RuleFor(x => x.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
                .NotNull().WithMessage("{PropertyName} must be after {ComparisonValue}.");
        }
    }
}