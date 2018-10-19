using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tracking.Data
{
    public class Detail
    {
        public int DetailId { get; set; }       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Digit { get; set; }
        [Required, AllowHtml]
        public string Sender { get; set; }
        [Required, AllowHtml]
        public string Receiver { get; set; }

        public DateTime CreatedDate { get; set; }

        public User User { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new HashSet<Track>();

    }
}