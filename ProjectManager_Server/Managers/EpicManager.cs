using System;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.Data.ViewModels;
using ProjectManager_Server.Repository.Interfaces;

namespace ProjectManager_Server.Managers;

/// <summary>
///     
/// </summary>
public class EpicManager : IEpicManager
{
    private readonly IEpicRepository _epicRepository;
    private ILogger<EpicManager> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="epicRepository"></param>
    public EpicManager(ILogger<EpicManager> logger, IEpicRepository epicRepository)
    {
        _logger = logger;
        _epicRepository = epicRepository;
    }

    /// <inheritdoc />
    public EpicViewModel GetOneById(Guid id)
    {
        var epic = _epicRepository.GetOneById(id);
        NotFoundException<Epic>.ThrowIfNull(epic);
        return epic.ToViewModel();
    }
}