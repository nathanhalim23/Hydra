using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class TrainerSkillDetail
    {
        public TrainerSkillDetail()
        {
            Courses = new HashSet<Course>();
        }

        public int TrainerId { get; set; }
        public string SkillId { get; set; } = null!;
        public DateTime AssignedDate { get; set; }
        public DateTime? AchievedDate { get; set; }

        public virtual Skill Skill { get; set; } = null!;
        public virtual Trainer Trainer { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; }
    }
}
