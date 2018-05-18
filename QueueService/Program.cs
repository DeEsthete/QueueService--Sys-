using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueService
{
    class Program
    {

        static void Main(string[] args)
        {
            using (QueueServiceContext context = new QueueServiceContext())
            {
                context.Exercises.ToList();

                Status statusNew = new Status();
                statusNew.StatusName = "Новый";
                Status statusWork = new Status();
                statusWork.StatusName = "Выполняется";
                Status statusOverdue = new Status();
                statusOverdue.StatusName = "Просроченный";
                Status statusComplete = new Status();
                statusComplete.StatusName = "Выполненый";

                context.Statuses.Add(statusNew);
                context.Statuses.Add(statusWork);
                context.Statuses.Add(statusOverdue);
                context.Statuses.Add(statusComplete);

                context.SaveChanges();
            }
        }

        private void Method()
        {
        }
    }
}
