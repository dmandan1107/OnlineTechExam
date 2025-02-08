using OnlineTechExamAPI.Model;

public interface IUserProfileRepository
{
    Task<IEnumerable<UserProfile>> GetAllAsync();
    Task<UserProfile> GetByIdAsync(int id);
    Task<UserProfile> CreateAsync(UserProfile user);
    Task<bool> UpdateAsync(UserProfile user);
    Task<bool> DeleteAsync(int id);
}
