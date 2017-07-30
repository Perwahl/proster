using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proster.Models
{
    public class RCA
    {
        public int RcaId { get; set; }
        public Case Case { get; set; }
        public User User { get; set; }
        public string Component { get; set; }
        public string RootCause { get; set; }
        public string RootCauseDescripton { get; set; }
        public string CorrectiveAction { get; set; }
    }
}