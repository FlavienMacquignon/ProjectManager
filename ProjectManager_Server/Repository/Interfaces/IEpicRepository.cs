using System;
using ProjectManager_Server.Models.Data.Entity;

namespace ProjectManager_Server.Repository.Interfaces;

public interface IEpicRepository
{
    public Epic GetOneById(Guid id);
}