using CManager.Domain.ConsoleApp.Models.Costumers;
using CManager.Domain.ConsoleApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Domain.ConsoleApp.Factory;

public static class ProfileFactory
{
    public static ProfileInfo Create()
    {
        return new ProfileInfo();
    }

    public static ProfileInfo Create(ProfileCreateRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new DomainException($"{nameof(request.FirstName)} is required.");

        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new DomainException($"{nameof(request.LastName)} is required.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new DomainException($"{nameof(request.Email)} is required.");

        var profile = new ProfileInfo
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };

        return profile;
    }
}
