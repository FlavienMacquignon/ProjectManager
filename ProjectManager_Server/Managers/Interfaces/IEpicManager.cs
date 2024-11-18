using System;
using ProjectManager_Server.Models.Data.ViewModels;

namespace ProjectManager_Server.Managers.Interfaces;

public interface IEpicManager
{
    public EpicViewModel GetOneById(Guid id);
}