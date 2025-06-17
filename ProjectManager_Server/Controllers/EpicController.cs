using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.Data.ViewModels.GetOneEpic;

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
    ///     ctor
    /// </summary>
    /// <param name="logger">Custom Logger</param>
    /// <param name="epicManager">Epic Manager</param>
    public EpicController(ILogger<EpicController> logger, IEpicManager epicManager)
    {
        _logger = logger;
        _epicManager = epicManager;
    }

    /// <summary>
    ///     Retrieve One Epic by its id
    /// </summary>
    /// <param name="id">id of the Epic to retrieve</param>
    /// <returns>The found Epic, if any</returns>
    [HttpGet(Name = "GetOneEpic")]
    [ProducesResponseType(typeof(OneEpicViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Get(Guid id)
    {
        IActionResult resp;
        try
        {
            resp = Ok(_epicManager.GetOneById(id));
        }
        catch (Exception e)
        {
            var errorMessage = e.Message;
            _logger.LogError("{Message}", errorMessage);
            resp = e switch
            {
                NotFoundException<Epic> => NotFound(errorMessage),
                _ => StatusCode(500, errorMessage)
            };
        }

        return resp;
    }
}