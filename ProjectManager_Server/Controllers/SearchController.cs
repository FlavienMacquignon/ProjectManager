using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Helpers;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Models.Shared.Internal.Filter;
using System.ComponentModel.DataAnnotations;

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
    ///     Search for Epics or Bugs using filter
    /// </summary>
    /// <param name="rules">The set of rules used to filter Epics and Bugs</param>
    /// <returns>A minimal representation of the results</returns>
    [HttpPost(template: "search", Name = "Search")]
    [ProducesResponseType(typeof(List<Responses>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public IActionResult Search([FromBody] FilterObject rules)
    {
        IActionResult resp;
        try
        {
            ObjectValidatorHelper.Validate(rules);   
            resp = Ok(_searchManager.Search(rules));
        }
        catch ( Exception e )
        {
            var errorMessage = e.Message;
            _logger.LogError("{Message}", errorMessage);
            _logger.LogDebug("{Stack}", e.StackTrace);
            resp = e switch
            {
                NotFoundException<List<Responses>> => NotFound(errorMessage),
                ValidationException => BadRequest(),
                _ => StatusCode(500, errorMessage)
            };
        }

        return resp;
    }
}