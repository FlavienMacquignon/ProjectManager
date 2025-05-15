using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Shared.Internal.Filter;

namespace ProjectManager_Server.Controllers;

/// <summary>
///     Search Controller
/// </summary>
[Route("/Search")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchManager _searchManager;

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="searchMan">Search Manager</param>
    /// <param name="logger">Custom Logger</param>
    public SearchController(ISearchManager searchMan, ILogger<SearchController> logger)
    {
        _searchManager = searchMan;
        _logger = logger;
    }

    /// <summary>
    ///     Search for Bugs or Epic using filter
    /// </summary>
    /// <param name="rules">The set of rules used to filter epics and bugs</param>
    /// <returns>A Minimal representation of the results</returns>
    [HttpPost(template: "filter", Name = "filter")]
    [ProducesResponseType(typeof(List<Responses>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Filter([FromBody] FilterObject rules)
    {
        IActionResult resp;

        try
        {
            resp = Ok(_searchManager.Filter(rules));
        }
        catch ( Exception e )
        {
            var errorMessage = e.Message;
            _logger.LogError("{Message}", errorMessage);
            _logger.LogDebug("{Stack}", e.StackTrace);
            resp = e switch
            {
                NotFoundException<Responses> => NotFound(errorMessage),
                _ => StatusCode(500, errorMessage)
            };
        }

        return resp;
    }
}