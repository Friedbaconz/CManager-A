using CManager.Domain.ConsoleApp.Models.Costumers;

namespace TestProjectRepository.Domain.Models;

public class Member_Test
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

}
