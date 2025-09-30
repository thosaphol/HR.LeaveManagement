using System;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Application.Contracts.Identity;

public interface IAuthService
{
    Task<RegistrationResponse> Register(RegistrationRequest request);
    Task<AuthResponse> Login(AuthRequest request);
}
