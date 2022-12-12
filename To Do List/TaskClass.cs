using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace To_Do_List
{
    internal record TaskClass
    {
        
        public string Task { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public TaskType TypeOfTask { get; set; }
       
        public TaskClass(string task, DateTime startTime, DateTime finishTime, TaskType typeOfTask)
        {
            Task = task;
            StartDate = startTime;
            FinishDate = finishTime;
            TypeOfTask = typeOfTask;
        }
    }
}
