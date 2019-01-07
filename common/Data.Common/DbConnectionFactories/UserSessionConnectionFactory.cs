

namespace Data.Common.DbConnectionFactories
{
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    using Infraestructure.Common.UserDatabaseConnection;

    public class UserSessionConnectionFactory : IDbConnectionFactory
    {
        private const string InvariantName = "System.Data.SqlClient";
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(InvariantName);
            if (providerFactory == null)
                throw new InvalidOperationException(String.Format("The '{0}' provider is not registered on the local machine.", InvariantName));

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = UserSessionConnectionFactory.GetSeguridadDB() ? UserSessionConnectionString : DefaultConnectionFactory.DefaultConnectionString;
            return connection;

        }

        public static string UserSessionConnectionString
        {
            get
            {
                if (UserSessionConnectionFactory.GetSeguridadDB())
                {
                    Connection connectionHeader = Connection.GetHeaderFromMessage();

                    var builder = new SqlConnectionStringBuilder(DefaultConnectionFactory.DefaultConnectionString)
                    {
                        UserID = connectionHeader.User,
                        Password = connectionHeader.Password
                    };
                    return builder.ConnectionString;
                }
                else 
                {
                    return DefaultConnectionFactory.DefaultConnectionString;
                }

                
            }
        }

        public static string UserSGCWSessionConnectionString 
        {
            get { return DefaultConnectionFactory.SGCWConnectionString; }
        }

        public static string UserNPTP2SessionConnectionString
        {
            get { return DefaultConnectionFactory.NPTP2ConnectionString; }
        }
        public static string UserCRMSessionConnectionString
        {
            get { return DefaultConnectionFactory.CRMConnectionString; }
        }
        public static string UserSGCSessionConnectionString
        {
            get { return DefaultConnectionFactory.SGCConnectionString; }
        }

        public static bool GetSeguridadDB()
        {
            var segdb = Convert.ToString(ConfigurationManager.AppSettings["SeguridadDB"]);
            return string.IsNullOrEmpty(segdb) ? false : Convert.ToBoolean(segdb);
        }


    }


}
