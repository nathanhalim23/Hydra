using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            CandidateEvaluations = new HashSet<CandidateEvaluation>();
        }

        public int Id { get; set; }
        public int BootcampClassId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Domicile { get; set; } = null!;
        public bool HasPassed { get; set; }
        public bool? IsActive { get; set; }

        public virtual BootcampClass BootcampClass { get; set; } = null!;
        public virtual ICollection<CandidateEvaluation> CandidateEvaluations { get; set; }
    }
}
