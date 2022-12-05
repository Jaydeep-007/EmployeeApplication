using Dapper;
using EmployeeApplication.Models;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeApplication.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public int GetEmployeeCount(string Search)
        {
            using (SqlConnection con = new SqlConnection(ShareConnectionString.Value))
            {
                var para = new DynamicParameters();
                para.Add("@Search", Search);
                var data = con.Query<int>("GetEmployeeCount_Search", para, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return data;
            }
        }

        public IEnumerable<Employee> EmployeePagination(string Search, string orderBy, int? pageNumber, int pageSize)
        {
            using (SqlConnection con = new SqlConnection(ShareConnectionString.Value))
            {
                var para = new DynamicParameters();
                para.Add("@orderBy", orderBy);
                para.Add("@PageNumber", pageNumber);
                para.Add("@PageSize", pageSize);
                para.Add("@Search", Search);
                var data = con.Query<Employee>("EmployeePagination_Search", para, commandType: CommandType.StoredProcedure).ToList();
                return data;
            }
        }
    }
}
