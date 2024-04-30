using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class CandidateEvaluation
    {
        public string CourseId { get; set; } = null!;
        public int CandidateId { get; set; }
        public int Mark { get; set; }
        public string? Notes { get; set; }
        public bool Passed { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
    }
}
