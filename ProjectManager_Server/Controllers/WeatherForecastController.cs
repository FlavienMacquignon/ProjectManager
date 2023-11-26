using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Models;

namespace ProjectManager_Server.Controllers;

/// <summary>
/// Base example controller
/// </summary>
[ApiController]
[Route("[controller]")]
public class BugController : ControllerBase
{

    /// <summary>
    /// Contextual Logger
    /// </summary>
    private readonly ILogger<BugController> _logger;

    /// <summary>
    /// Bug Controller
    /// </summary>
    /// <param name="logger">Contextual Logger</param>
    public BugController(ILogger<BugController> logger) => _logger = logger;


    /// <summary>
    /// Get One Bug
    /// </summary>
    /// <returns>An HTPP 200 containing a Bug object</returns>
    [HttpGet(Name = "GetOne")]
    public IActionResult Get()
    {
        _logger.LogDebug("GetOne Called");
        return Ok(new Bug());
    }
}
