using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StajIlkProje.Models
{
    public class ActivityDBContext: DbContext
    {
        public virtual DbSet<Activity> Activities { get; set; }
    }
}