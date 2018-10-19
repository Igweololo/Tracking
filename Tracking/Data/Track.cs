using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tracking.Data
{
    public class Track
    {
        public int TrackId { get; set; }
        public int DetailId { get; set; }
        [Required, AllowHtml]
        public string Destination { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required, AllowHtml]
        public string Consignment { get; set; }
        public virtual Detail Code { get; set; }

    }
}