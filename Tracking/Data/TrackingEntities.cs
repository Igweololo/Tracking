using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tracking.Data
{
    public class TrackingEntities : DbContext
    {
        public virtual DbSet<Track> Tracks { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Detail> Details { get; set; }

    }
}