using System;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handlers.Command
{
    using AutoMapper;
    using HR.LeaveManagement.Application.Contracts.Persistance;
    using HR.LeaveManagement.Application.DTOs.LeaveRequest;
    using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
    using HR.LeaveManagement.Application.Exceptions;
    using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);
            var leaveRequest = await _leaveRequestRepository.Get(request.Id);
            if (request.LeaveRequestDto != null)
            {

                _mapper.Map(request, leaveRequest);
                await _leaveRequestRepository.Update(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApproveDto != null)
            {
                // _mapper.Map(request, leaveRequest);
                await _leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApproveDto.Approved);
                // Handle change approval logic here
            }

            return Unit.Value;
        }

        private async Task ValidateRequest(UpdateLeaveRequestCommand request)
        {
            var validator = new UpdateLeaveRequestDtoValidator(_leaveRequestRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }
        }
    }
}
