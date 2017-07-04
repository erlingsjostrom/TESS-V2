using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TestRestfulAPI.Entities.TESS;
using TestRestfulAPI.Entities.User;

namespace TestRestfulAPI.Infrastructure.Helpers
{
    public class DbConnectionFactory
    {
        public static DbContext getContext(string dbName, Type type)
        {
            if (type == typeof(UserEntities))
            {
                return new UserEntities(GetConnectionString(dbName, typeof(UserEntities).Name));
            } else if (type == typeof(TESSEntities))
            {
                return new TESSEntities(GetConnectionString(dbName, typeof(TESSEntities).Name));
            }
            else
            {
                throw new InvalidDbContextTypeException("Provided context type: " + type.Name + " is not supported.");
            }
            
        }


        private static string GetConnectionString(string dbName, string contextName)
        {
            ValidateDbName(dbName);
           
            var entityConnSB = new EntityConnectionStringBuilder
            {
                Metadata = @"res://*/" + contextName + ".csdl|" +
                            @"res://*/" + contextName + ".ssdl|" +
                            @"res://*/" + contextName + ".msl;"
            };

            var sqlConnStringBuilder = new SqlConnectionStringBuilder(entityConnSB.ProviderConnectionString)
            {
                DataSource = GetDataSourceString(),
                InitialCatalog = dbName,
                MultipleActiveResultSets = true,
                UserID = GetDefaultUser(),
                Password = GetDefaultPassword()
            };
            return sqlConnStringBuilder.ConnectionString;  
        }

        private static string GetDataSourceString()
        {
            try
            {
                var basicDataSource = ConfigurationManager.ConnectionStrings["DefaultDataSource"].ConnectionString;
                if (String.IsNullOrEmpty(basicDataSource))
                {
                    throw new InvalidDbConnectionFactoryInput(
                        "The Default data source string, with db server uri, is missing.");
                }
                return basicDataSource;
            }
            catch (ConfigurationErrorsException e)
            {
                throw new InvalidDbConnectionFactoryInput(
                    "The Default data source string, with db server uri, is missing.");
            }
        }

        private static string GetDefaultUser()
        {
            try
            {
                var basicInfo = ConfigurationManager.ConnectionStrings["DefaultUser"].ConnectionString;
                if (String.IsNullOrEmpty(basicInfo))
                {
                    throw new InvalidDbConnectionFactoryInput(
                        "The Default DB user is missing.");
                }
                return basicInfo;
            } 
            catch (ConfigurationErrorsException e)
            {
                throw new InvalidDbConnectionFactoryInput(
                    "The Default DB user is missing.");
            }
        }
        private static string GetDefaultPassword()
        {
            try
            {
                var basicInfo = ConfigurationManager.ConnectionStrings["DefaultPassowrd"].ConnectionString;
                if (String.IsNullOrEmpty(basicInfo))
                {
                    throw new InvalidDbConnectionFactoryInput(
                        "The Default DB user is missing.");
                }
                return basicInfo;
            }
            catch (ConfigurationErrorsException e)
            {
                throw new InvalidDbConnectionFactoryInput(
                    "The Default DB user is missing.");
            }
        }

        private static void ValidateDbName(string dbName)
        {
            if (String.IsNullOrEmpty(dbName))
            {
                throw new InvalidDbConnectionFactoryInput("The dbName cannot be null or empty");
            }
        }
    }
}