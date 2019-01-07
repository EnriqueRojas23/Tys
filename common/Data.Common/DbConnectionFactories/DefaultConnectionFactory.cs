

namespace Data.Common.DbConnectionFactories
{
    using Infraestructure.Common.UserDatabaseConnection;
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;

    public class DefaultConnectionFactory : IDbConnectionFactory
    {
        private const string InvariantName = "System.Data.SqlClient";
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(InvariantName);
            if (providerFactory == null)
                throw new InvalidOperationException(String.Format("The '{0}' provider is not registered on the local machine.", InvariantName));

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = DefaultConnectionString;
            return connection;

        }

        public static string DefaultConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CnnSeg"].ConnectionString;
            }
        }

        public static string SGCWConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CnnSegSGCW"].ConnectionString;
            }
        }

        public static string NPTP2ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CnnSegNPTP2"].ConnectionString;
            }
        }
        public static string CRMConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CnnSegCRM"].ConnectionString;
            }
        }
        public static string SGCConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CnnSegSgc"].ConnectionString;
            }
        }
            
    }

    
}
