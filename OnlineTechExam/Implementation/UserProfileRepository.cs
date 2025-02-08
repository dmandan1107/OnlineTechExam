using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OnlineTechExamAPI.Model;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly string _connectionString;

    public UserProfileRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultDataContext");
    }

    private DbConnection GetDbConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public async Task<IEnumerable<UserProfile>> GetAllAsync()
    {
        using var db = GetDbConnection();
        return await db.QueryAsync<UserProfile>("GetAllUser", commandType: CommandType.StoredProcedure);
    }

    public async Task<UserProfile> GetByIdAsync(int id)
    {
        using var db = GetDbConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@UserID", id);
        return await db.QueryFirstOrDefaultAsync<UserProfile>("GetUserProfileById", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<UserProfile> CreateAsync(UserProfile user)
    {
        try
        {
            using var db = GetDbConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@Name", user.Name);
            parameters.Add("@EmailAddress", user.EmailAddress);
            parameters.Add("@Gender", user.Gender);
            parameters.Add("@Birthday", user.Birthday);
            parameters.Add("@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await db.ExecuteAsync("CreateUserProfile", parameters, commandType: CommandType.StoredProcedure);

            var newID = parameters.Get<int>("@UserID");

            return await GetByIdAsync(newID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }
        
    }

    public async Task<bool> UpdateAsync(UserProfile user)
    {
        try
        {
            using var db = GetDbConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", user.UserID);
            parameters.Add("@Name", user.Name);
            parameters.Add("@EmailAddress", user.EmailAddress);
            parameters.Add("@Gender", user.Gender);
            parameters.Add("@Birthday", user.Birthday);


            var rowsAffected = await db.ExecuteAsync("UpdateUserProfile",parameters,commandType: CommandType.StoredProcedure);

            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                throw new Exception("Failed to update records!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }
        
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            using var db = GetDbConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@UserID", id);
            var rowsAffected = await db.ExecuteAsync("DeleteUserProfile", parameters, commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex;
        }
        
    }
}
