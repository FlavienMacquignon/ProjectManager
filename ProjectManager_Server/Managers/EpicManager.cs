using System;
using Microsoft.Extensions.Logging;
using ProjectManager_Server.Exceptions;
using ProjectManager_Server.Managers.Interfaces;
using ProjectManager_Server.Models.Data.Entity;
using ProjectManager_Server.Models.Data.ViewModels.GetOneEpic;
using ProjectManager_Server.Repository.Interfaces;

namespace ProjectManager_Server.Managers;

/// <summary>
///     Epic Manager
/// </summary>
public class EpicManager : IEpicManager
{
    private readonly IEpicRepository _epicRepository;

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="logger">custom logger</param>
    /// <param name="epicRepository">Epic Repository</param>
    public EpicManager(ILogger<EpicManager> logger, IEpicRepository epicRepository)
    {
        _epicRepository = epicRepository;
    }

    /// <inheritdoc />
    public OneEpicViewModel GetOneById(Guid id)
    {
        var epic = _epicRepository.GetOneById(id);
        NotFoundException<Epic>.ThrowIfNull(epic);
        return epic!.ToViewModel();
    }
}