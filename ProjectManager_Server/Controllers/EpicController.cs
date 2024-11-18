using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.Data.ViewModels;

namespace ProjectManager_Server.Controllers;

/// <summary>
///     Epic Controller to manage Epics
/// </summary>
[Route("/Epic")]
public class EpicController : ControllerBase
{
    private readonly IEpicManager _epicManager;
    private readonly ILogger<EpicController> _logger;

    /// <summary>
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="epicManager"></param>
    public EpicController(ILogger<EpicController> logger, IEpicManager epicManager)
    {
        _logger = logger;
        _epicManager = epicManager;
    }

    [HttpGet(Name = "GetOneEpic")]
    [ProducesResponseType(typeof(EpicViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Get(Guid id)
    {
        IActionResult ret;
        try
        {
            ret = Ok(_epicManager.GetOneById(id));
        }
        catch (NotFoundException<Epic> ex)
        {
            _logger.LogError(ex.Message);
            ret = NotFound(ex.Message);
        }

        return ret;
    }
}