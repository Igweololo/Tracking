using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracking.Data;

namespace Tracking.Models
{
    public class TrackViewModel
    {
        public Detail Details { get; set; } = new Detail();
        public List<Track> Tracks { get; set; } = new List<Track>();
    }
}