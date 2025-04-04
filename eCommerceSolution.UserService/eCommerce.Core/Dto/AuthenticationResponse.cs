﻿namespace eCommerce.Core.Dto;

public record AuthenticationResponse(
    Guid UserId,
    string? Email,
    string? Name,
    string? Gender,
    string? Token,
    bool Success);
