using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManagerAPI.Models
{
    public class Task
    {
        public int Task_ID { get; set; }
        public int? Parent_ID { get; set; }
        public string TaskName { get; set; }
        public string ParentTaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priority { get; set; }
        public bool IsFinished { get; set; }
    }
}