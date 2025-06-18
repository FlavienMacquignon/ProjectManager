using Microsoft.EntityFrameworkCore;
using ProjectManager_Server.Repository.Abstract.Interface;
using System;

namespace ProjectManager_Server.Repository.Abstract;

/// <summary>
///     Abstract Repository for shared methods
/// </summary>
public class AbstractRepository(DbContext db) : IRepository
{
    /// <summary>
    ///     The DbContext
    /// </summary>
    protected DbContext Db { get; set; } = db;

    /// <inheritdoc />
    public void SaveChanges()
    {
        var errorCode = Db.SaveChanges();
        // TODO Throw CustomException ??
        if (errorCode == 0) throw new Exception("Database issue");
    }
    
    /// <summary>
    ///     unassign Db Context for sanity
    /// </summary>
    ~AbstractRepository()
    {
        Db = null!;
    }
}

