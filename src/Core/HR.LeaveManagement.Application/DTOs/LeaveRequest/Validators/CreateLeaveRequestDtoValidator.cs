using System;
using System.Data;
using FluentValidation;
using HR.LeaveManagement.Application.Persistance.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator:AbstractValidator<CreateLeaveRequestDto>
    {
        private ILeaveRequestRepository _leaveRequestRepository;
        public CreateLeaveRequestDtoValidator(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            Include(new ILeaveRequestDtoValidator(_leaveRequestRepository));
           
            // RuleFor(x => x.StartDate)
            //     .LessThan(x => x.EndDate)
            //     .WithMessage("{PropertyName} must be before {ComparisonValue}.");

            // RuleFor(x => x.EndDate)
            //     .LessThan(x => x.StartDate)
            //     .WithMessage("{PropertyName} must be after {ComparisonValue}.");

            // RuleFor(x => x.LeaveTypeId)
            //     .GreaterThan(0)
            //     .MustAsync(async (id, cancellation) => 
            //     {
            //         var leaveTypeExists = await _leaveRequestRepository.Exist(id);
            //         return !leaveTypeExists;
            //     })
            //     .WithMessage("{PropertyName} does not exist.");
        
        }
        
    }
}
