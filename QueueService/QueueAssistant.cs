using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace QueueService
{
    public class QueueAssistant
    {
        private List<Exercise> Exercises { get; set; }

        Task taskFirst;
        Task taskSecond;
        Task taskThird;
        Task taskFourth;
        Task taskFifth;

        public void GetExercise(ref Task<int> task)
        {
            List<Exercise> exercises;
            Exercise exercise = new Exercise();

            using (QueueServiceContext context = new QueueServiceContext())
            {
                exercises = context.Exercises.ToList();
                for (int i = 0; i < exercises.Count; i++)
                {
                    if (exercises[i].Status.StatusName != "Новая")
                    {
                        exercises.RemoveAt(i);
                    }
                }

                if (exercises.Count != 0)
                {
                    exercise = exercises.Last();
                }
                
            }

            //
            //
            //Проблема
            task = Task<int>.Run(new Func<Exercise>(TaskWork(exercise)));
            //
            //
            // task = Task.Factory.StartNew(new Action<Exercise>(TaskWork(exercise)));
        }

        public static void TaskWork(Exercise exercise)
        {
            List<Exercise> exercises;
            List<Status> statuses;
            using (QueueServiceContext context = new QueueServiceContext())
            {
                exercises = context.Exercises.ToList();
                statuses = context.Statuses.ToList();

                context.Exercises.Remove(exercise);

                foreach (var i in statuses)
                {
                    if (i.StatusName == "Выполняется")
                    {
                        exercise.Status = i;
                        exercise.StatusId = i.Id;
                    }
                }
                exercise.TaskId = Thread.CurrentThread.ManagedThreadId;
                context.Exercises.Add(exercise);
                context.SaveChanges();

                Random rand = new Random();
                Thread.Sleep(rand.Next(6, 9));

                context.Exercises.Remove(exercise);

                foreach (var i in statuses)
                {
                    if (i.StatusName == "Выполнен")
                    {
                        exercise.Status = i;
                        exercise.StatusId = i.Id;
                    }
                }
                context.Exercises.Add(exercise);
                context.SaveChanges();
                
            }
        }

        public void CreateDelivery()
        {
            List<Status> statuses;
            List<Delivery> deliveries;
            int indexStatusNew = -1;
            using (QueueServiceContext context = new QueueServiceContext())
            {
                statuses = context.Statuses.ToList();

                Delivery tempDelivery = new Delivery();
                context.Deliveries.Add(tempDelivery);
                context.SaveChanges();

                deliveries = context.Deliveries.ToList();

                Exercise tempExcercise = new Exercise();
                foreach (var i in statuses)
                {
                    if (i.StatusName == "Новый") indexStatusNew = i.Id;
                }
                tempExcercise.StatusId = indexStatusNew;
                tempExcercise.Status = statuses[indexStatusNew];
                tempExcercise.DeliveryId = deliveries.Last().Id;
                tempExcercise.Delivery = deliveries.Last();
                context.Exercises.Add(tempExcercise);
                context.SaveChanges();
            }
        }
    }
}
