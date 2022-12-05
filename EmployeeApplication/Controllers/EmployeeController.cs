using EmployeeApplication.Models;
using EmployeeApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using X.PagedList;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IActionResult Grid(string Search, int sortby, bool isAsc = true, int? page = 1)
        {
            string SearchValue = string.Empty;
            if (!string.IsNullOrEmpty(Search))
            {
                SearchValue = Regex.Replace(Search, @"[^a-zA-Z0-9\s]", string.Empty);
            }

            if (page < 0)
            {
                page = 1;
            }

            EmployeePagingInfo EmployeePagingInfo = new EmployeePagingInfo();
            var pageIndex = (page ?? 1) - 1;
            var pageSize = 5;

            string sortColumn;
            #region SortingColumn
            switch (sortby)
            {
                case 1:
                    if (isAsc)
                        sortColumn = "Id";
                    else
                        sortColumn = "Id Desc";
                    break;

                case 2:
                    if (isAsc)
                        sortColumn = "Name";
                    else
                        sortColumn = "Name Desc";
                    break;

                case 3:
                    if (isAsc)
                        sortColumn = "Email";
                    else
                        sortColumn = "Email Desc";
                    break;
                default:
                    sortColumn = "Id asc";
                    break;

            }
            #endregion

            int totalProductCount = employeeRepository.GetEmployeeCount(SearchValue);
            var employee = employeeRepository.EmployeePagination(SearchValue, sortColumn, page, pageSize);
            var employeePagedList = new StaticPagedList<Employee>(employee, pageIndex + 1, pageSize, totalProductCount);
            EmployeePagingInfo.Employees = employeePagedList;
            EmployeePagingInfo.pageSize = page;
            EmployeePagingInfo.sortBy = sortby;
            EmployeePagingInfo.isAsc = isAsc;
            EmployeePagingInfo.Search = SearchValue;
            return View(EmployeePagingInfo);

        }
    }
}
