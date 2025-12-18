using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Application.ConsoleApp.Services.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using NSubstitute;
using System.Diagnostics;
using System.Reflection;

namespace TestProjectRepository;

public class MemberCreationTestClass
{
    [Fact]
    public void Costumer_ShouldHaveDefaultValues()
    {
        var costumer = new ProfileInfo();

        Assert.Equal(string.Empty, costumer.FirstName);
        Assert.Equal(string.Empty, costumer.LastName);
        Assert.Equal(string.Empty, costumer.Id);
        Assert.Equal(string.Empty, costumer.Email);
        Assert.Equal(string.Empty, costumer.PhoneNumber);
    }


    [Fact]

    public void CreateProfile_ShouldReturn_Failed_WhenProfileFailsToBeMade()
    {
        var filePath = Substitute.For<ICostumerRepository>();
            filePath.Exists(Arg.Any<Func<ProfileInfo, bool>>()).Returns(true);
            filePath.Add(Arg.Any<ProfileInfo>()).Returns(new ProfileResult ( false , ""));

        var profileservice = new CostumerService(filePath);

        var request = new ProfileCreateRequest
        {
            FirstName = "sis",
            LastName = "Dod",
            Email = "dem@domain.com",
            PhoneNumber = "123324789",

        };

        var result = profileservice.CreateProfileAsync(request).GetAwaiter().GetResult();

        Assert.False(result.Success);
        Assert.Equal("Profile with this email already exists.", result.Message);
    }

}
