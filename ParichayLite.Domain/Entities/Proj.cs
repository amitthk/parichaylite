using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ParichayLite.Domain.Entities
{

   public class Proj
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public ICollection<Goal> Goals { get; set; }

        public ICollection<Story> Stories { get; set; }

    }

   public class Goal
   {
       public int Id { get; set; }

       public string Name { get; set; }
       public string Summary { get; set; }
   }

   public class Story
   {
       public int Id { get; set; }

       public string Name { get; set; }
       public string Summary { get; set; }
   }
}
