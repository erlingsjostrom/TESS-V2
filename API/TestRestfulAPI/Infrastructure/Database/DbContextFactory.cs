﻿using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using TestRestfulAPI.Infrastructure.Contexts;
using TestRestfulAPI.Infrastructure.Exceptions;
using TestRestfulAPI.Infrastructure.Helpers;

namespace TestRestfulAPI.Infrastructure.Database
{
    /// <summary>
    /// Factory class to return instances of DbContext for supported types
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// Get DbContext instance for provided database
        /// </summary>
        /// <param name="dbName">name of database to connect to</param>
        /// <returns></returns>
        public static DbContext Get<T>(string dbName) where T : DbContext
        {
            if (typeof(T) == typeof(UserEntities))
            {
                return new UserEntities(GetEntityConnection(dbName, typeof(T).Name));
            }
            else if (typeof(T) == typeof(TESSEntities))
            {
                return new TESSEntities(GetEntityConnection(dbName, typeof(T).Name));
            }
            throw new InvalidDbContextTypeException("Provided DbContext: " + typeof(T).Name + " is not supported.");
            
        }

        public static DbContext Get(string dbName, Type type)
        {
            if (type == typeof(UserEntities))
            {
                return new UserEntities(GetEntityConnection(dbName, type.Name));
            }
            else if (type == typeof(TESSEntities))
            {
                return new TESSEntities(GetEntityConnection(dbName, type.Name));
            }

            throw new InvalidDbConnectionFactoryInput("Provided type: " + type.Name + " is not an instance of DbContext");
        }

        /// <summary>
        /// Gets an EntityConnection for provided database and context
        /// </summary>
        /// <param name="dbName">name of database to connect to</param>
        /// <param name="contextName">name of context</param>
        /// <returns></returns>
        private static SqlConnection GetEntityConnection(string dbName, string contextName)
        {
            ValidateDbName(dbName);

            var sqlConnStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = GetDataSourceString(),
                InitialCatalog = dbName,
                MultipleActiveResultSets = true,
                ApplicationName = "EntityFramework",
                UserID = GetDefaultUser(),
                Password = GetDefaultPassword()
            };

            var entityConnStringBuilder = new EntityConnectionStringBuilder
            {
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = sqlConnStringBuilder.ConnectionString,
                //Metadata = "res://*/" + contextName + ".csdl|" +
                //           "res://*/" + contextName + ".ssdl|" +
                //           "res://*/" + contextName + ".msl"
            };

            var entityConnection = new SqlConnection(sqlConnStringBuilder.ConnectionString);
            return entityConnection;
        }

        /// <summary>
        /// Get DataSource from config and validate input
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get DefaultUser from config and validate input
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get GetDefaultPassword from config and validate input
        /// </summary>
        /// <returns></returns>
        private static string GetDefaultPassword()
        {
            try
            {
                var basicInfo = ConfigurationManager.ConnectionStrings["DefaultPassword"].ConnectionString;
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

        /// <summary>
        /// Validate the provided Database name
        /// </summary>
        /// <returns></returns>
        private static void ValidateDbName(string dbName)
        {
            if (String.IsNullOrEmpty(dbName))
            {
                throw new InvalidDbConnectionFactoryInput("The dbName cannot be null or empty");
            }
        }
    }
}