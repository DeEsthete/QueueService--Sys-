using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueService
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [ForeignKey("Delivery")]
        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        public int TaskId { get; set; }
    }
}
