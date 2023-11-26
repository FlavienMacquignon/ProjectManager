using System.Linq;
using ProjectManager_Server.Models;
using ProjectManager_Server.Services;

namespace ProjectManager_Server.Repository;

/// <summary>
/// Bug Repository
/// </summary>
public class BugRepository
{

    private ProjectManagerContext _db { get; set; }

    /// <summary>
    /// ctor
    /// </summary>
    public BugRepository()
    {
        _db = new ProjectManagerContext();
    }

    /// <summary>
    /// Retrieve the first Bug in the Database
    /// </summary>
    /// <returns>The first Bug in the Db Set</returns>
    
    public Bug GetOne(){
        return _db.Bugs.First();
    }

    /// <summary>
    /// unassign Db Context
    /// </summary>
    ~BugRepository()
    {
        _db = null;
    }
}