using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueService
{
    public class Delivery
    {
        [Key]
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
