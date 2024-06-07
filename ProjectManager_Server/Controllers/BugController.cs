using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Manager;
using ProjectManager_Server.Models;
using ProjectManager_Server.Models.ViewModels;

namespace ProjectManager_Server.Controllers;

/// <summary>
/// Base Bug controller for primary objects
/// </summary>
[ApiController]
[Route("/Bug")]
public class BugController : ControllerBase
{

    /// <summary>
    /// Contextual Logger
    /// </summary>
    private readonly ILogger<BugController> _logger;

    /// <summary>
    /// IBugManager that handle logic
    /// </summary>
    private IBugManager _bugManager;

    /// <summary>
    /// Bug Controller ctor
    /// </summary>
    /// <param name="logger">Contextual Logger</param>
    /// <param name="bugManager">IBugManager for Bug specific logic</param>
    public BugController(ILogger<BugController> logger, IBugManager bugManager) { 
        _logger = logger;
        _bugManager = bugManager;
        }


    /// <summary>
    /// Get One Bug for now only display the first Bug in the database, this demonstrate communication with BDD
    /// </summary>
    /// <returns>An HTPP 200 containing a Bug object</returns>
    [HttpGet(Name = "GetOne")]
    public IActionResult Get()
    {
        return Ok(_bugManager.GetOne());
    }


    /// <summary>
    /// Add a new bug in the database
    /// </summary>
    /// <param name="entityToAdd">The object to add to the database</param>
    /// <returns>An HTPP 200 containing a Bug object</returns>
    [HttpPost(Name ="Insert")]
    public IActionResult Add(DescriptionContentViewModel entityToAdd){
        return Ok(_bugManager.Add(entityToAdd));
    }
}
