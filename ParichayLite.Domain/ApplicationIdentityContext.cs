namespace ParichayLite.Domain
{
    using ParichayLite.Domain;
    using Microsoft.AspNet.Identity;
    using Microsoft.Data.Entity;
    using System.Collections.Generic;
    using Entities;
    using System;

    public class ApplicationIdentityContext : ISqliteContext
    {
        public ApplicationIdentityContext(ISqliteContext SqliteContext)
            : this(SqliteContext.Users, SqliteContext.Roles)
        {
        }

        public ApplicationIdentityContext(IUserStore<User> users, IRoleStore<Role> roles)
        {

        }

        public List<Client> Clients
        {
            get; set;
        }

        public DbContext Database
        {
            get; set;
        }

        public List<Proj> Projects
        {
            get; set;
        }

        public List<RefreshToken> RefreshTokens
        {
            get; set;
        }

        public IRoleStore<Role> Roles
        {
            get; set;
        }

        public IUserStore<User> Users
        {
            get; set;
        }
    }
}