using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class Category
    {
        public Category()
        {
            Skills = new HashSet<Skill>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Level { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
