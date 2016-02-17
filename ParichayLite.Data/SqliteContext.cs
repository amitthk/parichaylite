namespace ParichayLite.Data
{
    using ParichayLite.Domain;
    using ParichayLite.Domain.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.Data.Entity;
    using System;
    using System.Collections.Generic;
    using Microsoft.Framework.ConfigurationModel;
    public class SqliteContext : ISqliteContext
    {
        private readonly IUserStore<User> userCollection;
        private readonly IRoleStore<Role> roleCollection;
        private readonly List<Client> clientCollection;
        private readonly List<RefreshToken> refreshTokenCollection;
        private readonly List<Proj> projectsCollection;
        private IConfiguration config;

        public SqliteContext()
        {
            //var pack = new ConventionPack()
            //{
            //    new CamelCaseElementNameConvention(),
            //    new EnumRepresentationConvention(BsonType.String)
            //};

            //ConventionRegistry.Register("CamelCaseConvensions", pack, t => true);
            config = new Configuration()
    .AddEnvironmentVariables();
            //var SqliteUrlBuilder = new SqliteUrlBuilder(configSection.ConnectionString.Value);

            var SqliteClient = new SqliteClient(SqliteUrlBuilder.ToSqliteUrl());
            var server = SqliteClient.GetServer();

            Database = server.GetDatabase(SqliteUrlBuilder.DatabaseName);

            userCollection = Database.GetCollection<User>("users");
            roleCollection = Database.GetCollection<Role>("roles");
            clientCollection = Database.GetCollection<Client>("clients");
            refreshTokenCollection = Database.GetCollection<RefreshToken>("refreshTokens");
            projectsCollection = Database.GetCollection<Proj>("Projects");
        }

        public DbContext Database { get; private set; }

        public IUserStore<User> Users
        {
            get { return userCollection; }
        }

        public IRoleStore<Role> Roles
        {
            get { return roleCollection; }
        }

        public List<Client> Clients
        {
            get { return clientCollection; }
        }

        public List<RefreshToken> RefreshTokens
        {
            get { return refreshTokenCollection; }
        }


        public List<Proj> Projects
        {
            get { return projectsCollection; }
        }
        
    }
}
