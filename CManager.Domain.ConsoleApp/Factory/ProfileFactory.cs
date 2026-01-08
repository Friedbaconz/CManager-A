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

        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            throw new DomainException($"{nameof(request.PhoneNumber)} is required.");

        if (string.IsNullOrWhiteSpace(request.Address.Ort))
            throw new DomainException($"{nameof(request.Address.Ort)} is required.");

        if (string.IsNullOrWhiteSpace(request.Address.PostNumbers))
            throw new DomainException($"{nameof(request.Address.PostNumbers)} is required.");

        if (string.IsNullOrWhiteSpace(request.Address.StreetName))
            throw new DomainException($"{nameof(request.Address.StreetName)} is required.");


        var profile = new ProfileInfo
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = request.Email.Trim(),
            PhoneNumber = request.PhoneNumber.Trim(),
            Address = new AddressInfo
            {
                Ort = request.Address.Ort.Trim(),
                PostNumbers = request.Address.PostNumbers.Trim(),
                StreetName = request.Address.StreetName.Trim()
            }
        };

        return profile;
    }
}
