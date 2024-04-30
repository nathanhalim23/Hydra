using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class Course
    {
        public Course()
        {
            CandidateEvaluations = new HashSet<CandidateEvaluation>();
        }

        public string Id { get; set; } = null!;
        public int TrainerId { get; set; }
        public string SkillId { get; set; } = null!;
        public int BootcampClassId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? EvaluationDate { get; set; }
        public int Progress { get; set; }

        public virtual BootcampClass BootcampClass { get; set; } = null!;
        public virtual TrainerSkillDetail TrainerSkillDetail { get; set; } = null!;
        public virtual ICollection<CandidateEvaluation> CandidateEvaluations { get; set; }
    }
}
