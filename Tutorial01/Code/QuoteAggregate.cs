using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tutorial01.Code
{
    public class QuoteAggregate
    {
        [Key]
        public int Id { get; set; }
        public string Emotion { get; set; }
        public int Qty { get; set; }
    }
}