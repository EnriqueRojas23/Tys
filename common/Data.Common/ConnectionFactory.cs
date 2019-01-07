

using Data.Common.DbConnectionFactories;
using System;
using System.Configuration;
using System.Data.SqlClient;
namespace Data.Common
{
    public class ConnectionFactory
    {
        ////public static SqlConnection CreateDefault()
        ////{
        ////    var connection = new SqlConnection(DefaultConnectionFactory.DefaultConnectionString);
        ////    try
        ////    {
        ////        connection.Open();
        ////        return connection;
        ////    }
        ////    catch (Exception)
        ////    {
        ////        connection.Close();
        ////        connection.Dispose();
        ////        throw;
        ////    }
        ////}

        public static SqlConnection CreateFromSecuritySession()
        {
            var connection = new SqlConnection(DefaultConnectionFactory.DefaultConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }


        }
        public static SqlConnection CreateFromUserSession()
        {
            var connection = new SqlConnection(UserSessionConnectionFactory.UserSessionConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }

        }

        public static SqlConnection CreateFromUserSGCWSession()
        {
            var connection = new SqlConnection(UserSessionConnectionFactory.UserSGCWSessionConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }
        }

        public static SqlConnection CreateFromUserNPTP2Session()
        {
            var connection = new SqlConnection(UserSessionConnectionFactory.UserNPTP2SessionConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }
        }
        public static SqlConnection CreateFromUserCRMSession()
        {
            var connection = new SqlConnection(UserSessionConnectionFactory.UserCRMSessionConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }
        }
        public static SqlConnection CreateFromUserSGCSession()
        {
            var connection = new SqlConnection(UserSessionConnectionFactory.UserSGCSessionConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }
        }


      
    }
}
