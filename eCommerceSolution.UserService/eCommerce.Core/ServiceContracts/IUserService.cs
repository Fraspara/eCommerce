using eCommerce.Core.Dto;

namespace eCommerce.Core.ServiceContracts;

/// <summary>
/// Contracts for users service that contains use cases for users
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Method to handle user login use case and return an AuthenticationResponse object that contains user status of login.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);

    /// <summary>
    /// Method to handle register user registration use case and return an object of AuthenticationResponse type that represent status of user registration.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);
}
