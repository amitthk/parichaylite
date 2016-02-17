namespace ParichayLite.Domain
{
    using ParichayLite.Domain.Entities;
    using System.Collections.Generic;
    using Microsoft.Data.Sqlite;
    using Microsoft.Data.Entity;
    using Microsoft.AspNet.Identity;
    public interface ISqliteContext
    {
        DbContext Database { get; }

        IUserStore<User> Users { get; }
        IRoleStore<Role> Roles { get; }
        List<Client> Clients { get; }
        List<RefreshToken> RefreshTokens { get; }
        List<Proj> Projects { get; }
    }
}
