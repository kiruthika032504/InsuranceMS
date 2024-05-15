using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using InsuranceMS.model;
using InsuranceMS.exception;
using InsuranceMS.utility;

namespace InsuranceMS.dao
{
    public class PolicyService : IPolicyService
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public PolicyService()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
        }
        public bool CreatePolicy(Policy policy)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "INSERT INTO Policies (PolicyName, Premium) VALUES (@PolicyName, @Premium)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PolicyName", policy.PolicyName);
                cmd.Parameters.AddWithValue("@Premium", policy.Premium);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error creating policy: " + ex.Message);
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public Policy GetPolicy(int policyId)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "SELECT * FROM Policies WHERE PolicyId = @PolicyId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PolicyId", policyId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Policy policy = new Policy()
                    {
                        PolicyId = Convert.ToInt32(reader["PolicyId"]),
                        PolicyName = reader["PolicyName"].ToString(),
                        Premium = Convert.ToDouble(reader["Premium"])
                    };
                    return policy;
                }
                else
                {
                    throw new PolicyNotFoundException("Policy not found with ID: ");
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error retrieving policy: " + ex.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public ICollection<Policy> GetAllPolicies()
        {
            try
            {
                List<Policy> policies = new List<Policy>();
                sqlConnection.Open();
                cmd.CommandText = "SELECT * FROM Policies";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Policy policy = new Policy
                    {
                        PolicyId = Convert.ToInt32(reader["PolicyId"]),
                        PolicyName = reader["PolicyName"].ToString(),
                        Premium = Convert.ToDouble(reader["Premium"])
                    };
                    policies.Add(policy);
                }
                return policies;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error retrieving all policies: " + ex.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdatePolicy(Policy policy)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "UPDATE Policies SET PolicyName = @PolicyName, Premium = @Premium WHERE PolicyId = @PolicyId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PolicyId", policy.PolicyId);
                cmd.Parameters.AddWithValue("@PolicyName", policy.PolicyName);
                cmd.Parameters.AddWithValue("@Premium", policy.Premium);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error updating policy: " + ex.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeletePolicy(int policyId)
        {
            try
            {
                sqlConnection.Open();
                cmd.CommandText = "DELETE FROM Policies WHERE PolicyId = @PolicyId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PolicyId", policyId);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error deleting policy: " + ex.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public Policy GetPolicyById(int policyId)
        {
            cmd.CommandText = "SELECT * FROM Policies WHERE PolicyId = @PolicyId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PolicyId", policyId);
            try
            {
                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    Policy policy = new Policy
                    {
                        PolicyId = Convert.ToInt32(reader["PolicyId"]),
                        PolicyName = reader["PolicyName"].ToString(),
                        Premium = Convert.ToDouble(reader["Premium"])
                    };
                    return policy;
                }
                else
                {
                    return null;
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Sql Erro :" + ex.Message);
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
