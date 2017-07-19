using System;

namespace Common.Parameters
{
    public interface IParameters
    {
        Guid? ID { get; set; }
        int? PageSize { get; set; }
        int? PageNumber { get; set; }
        int? Skip { get; set; }
        int? Take { get; set; }
        string SearchString { get; set; }
        string SortOrder { get; set; }
        string IncludeProperties { get; set; }
        bool Paged { get; set; }
    }

    public class Parameters : IParameters
    {
        public Guid? ID { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public string IncludeProperties { get; set; }
        public bool Paged { get; set; }
    }
}
