using System;

namespace MVC.Models.AssignmentViewModels
{
    public class AssignmentIndexViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Materials { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Note { get; set; }
    }
}
