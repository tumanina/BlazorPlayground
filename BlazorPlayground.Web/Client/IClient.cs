using BlazorPlayground.Web.Models;

namespace BlazorPlayground.Web.Client
{
    public interface IClient
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<List<Department>> GetUserDepartmentsById(Guid userId);
        Task CreateOrUpdateUser(User user);
        Task AddDepartmentsToUser(Guid userId, List<Department> departments);
        Task DeleteUserDepartment(Guid userId, Department department);
    }
}