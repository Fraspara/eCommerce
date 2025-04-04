namespace eCommerce.Core.Dto;

public record RegisterRequest(
    string? Email,
    string? Password,
    string? Name,
    GenderOptions Gender);
