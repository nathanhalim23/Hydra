using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class BootcampClass
    {
        public BootcampClass()
        {
            Candidates = new HashSet<Candidate>();
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Progress { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
