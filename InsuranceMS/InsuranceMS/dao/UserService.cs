using InsuranceMS.utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMS.dao
{
    public class UserService : IUserService
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public UserService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }

        public bool RegisterUser(string username, string password, string role)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to register user, Try again!" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool IsUsernameExists(string username)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", username);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Username does not exists" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool IsLoginValid(string username, string password)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

        }
    }
}
