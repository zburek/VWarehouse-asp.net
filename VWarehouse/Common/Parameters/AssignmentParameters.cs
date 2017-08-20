using System;

namespace Common.Parameters
{
    public interface IAssignmentParameters : IParameters
    {
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
    }

    public class AssignmentParameters : Parameters, IAssignmentParameters
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}