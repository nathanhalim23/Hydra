using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class Skill
    {
        public Skill()
        {
            TrainerSkillDetails = new HashSet<TrainerSkillDetail>();
        }

        public string Id { get; set; } = null!;
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<TrainerSkillDetail> TrainerSkillDetails { get; set; }
    }
}
