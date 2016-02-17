namespace ParichayLite.Data
{
    using ParichayLite.Domain.Entities;
    using ParichayLite.Domain.Models;
    using ParichayLite.Data;
    using Microsoft.AspNet.Identity;
    using MongoDB.Driver.Builders;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ParichayLite.Domain;

    public class ProjectsRepository
    {
        private readonly IMongoContext mongoContext;

        public ProjectsRepository(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        public IList<Proj> GetProjects()
        {
            List<Proj> proj = mongoContext.Projects.FindAll().ToList(); 

            return proj;
        }

    }
}
