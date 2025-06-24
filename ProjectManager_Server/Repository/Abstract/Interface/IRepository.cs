namespace ProjectManager_Server.Repository.Abstract.Interface;

/// <summary>
///     Common interface for Repository
/// </summary>
public interface IRepository
{
    /// <summary>
    ///     Save changes to DbContext and handle Errors
    /// </summary>
    public void SaveChanges();
}