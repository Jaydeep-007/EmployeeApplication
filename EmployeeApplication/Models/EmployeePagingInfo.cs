using X.PagedList;

namespace EmployeeApplication.Models
{
    public class EmployeePagingInfo
    {
        public int? pageSize;
        public int sortBy;
        public string Search;
        public bool isAsc { get; set; }
        public StaticPagedList<Employee> Employees { get; set; }
    }
}
