using EmployeeApplication.Models;

namespace EmployeeApplication.Repository
{
    public interface IEmployeeRepository
    {
        int GetEmployeeCount(string Search);
        IEnumerable<Employee> EmployeePagination(string Search, string orderBy, int? pageNumber, int pageSize);
    }
}
