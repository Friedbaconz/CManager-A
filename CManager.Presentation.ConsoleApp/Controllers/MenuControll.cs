using CManager.Domain.ConsoleApp.Interface.Costumers;
using CManager.Domain.ConsoleApp.Models.Costumers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Presentation.ConsoleApp.Controllers;

public sealed class MenuControll
{
    private readonly ICostumerService _costumerService;
    private List<ProfileInfo> _profiles = [];
    public MenuControll(ICostumerService costumerService)
    {
        _costumerService = costumerService;
        _costumerService.GetAllProfilesAsync().Wait();
    }
}
