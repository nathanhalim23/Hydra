using System;
using System.Collections.Generic;

namespace HydraDataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Trainers = new HashSet<Trainer>();
            Roles = new HashSet<Role>();
        }

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Trainer> Trainers { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
