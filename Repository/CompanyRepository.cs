using Dapper;
using DapperAsp.Net.Context;
using DapperAsp.Net.Contract;
using DapperAsp.Net.Dto;
using DapperAsp.Net.Enitities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data;
using System.Linq.Expressions;

namespace DapperAsp.Net.Repository
{
    public class CompanyRepository: ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateCompany(CompanyForcreationDTO company)
        {
            var Query = "Insert into companies (Name ,Address,Country) values(@Name,@Address,@Country)"
                + "select Cast(SCOPE_IDENTITY  () AS int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name);
            parameters.Add("Address", company.Address);
            parameters.Add("Country", company.Country);

            using (var connection = _context.CreateConnection())
            {
                var id =await connection.QuerySingleAsync<int>(Query, parameters);
                Company company1 = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };

                return company1;
            }
        }

        public async Task<bool> DeleteCompany(int id)
        {
            var query = "Delete from companies where Id=@Id;";
            using (var connection = _context.CreateConnection())
            {
                var result =await connection.ExecuteAsync(query, new { id });
                return  result>0;
            }

        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var  query= "Select * from Companies";
            using (var connection =_context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var Query = "Select * From companies where Id=@Id;";
            using (var connection =_context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(Query, new { id });
                return company;
            }
        }

        public async Task<Company> GetCompanyByEmployeeId(int id)
        {
            var ProcedureName = "ShowCompanyByEmployeeID";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id,DbType.Int32,ParameterDirection.Input);
            using(var connection =_context.CreateConnection())
            {
                var company = await connection.QueryFirstOrDefaultAsync<Company>(ProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return company;
            }
        }

        public async Task<List<Company>> GetMulipleMapping()
        {
            var query = "SELECT * FROM Companies c JOIN Employees e ON c.Id = e.CompanyId;";

            using (var connection = _context.CreateConnection())
            {
                var companyDic = new Dictionary<int, Company>();

                var companies = await connection.QueryAsync<Company, Employee, Company>(
                    query,
                    (company, employee) =>
                    {
                        if (!companyDic.TryGetValue(company.Id, out var currentCompany))
                        {
                            currentCompany = company;
                            currentCompany.Employees = new List<Employee>(); 
                            companyDic[company.Id] = currentCompany;
                        }
                        currentCompany.Employees.Add(employee);
                        return currentCompany;
                    },
                    splitOn: "Id" 
                );

                return companyDic.Values.ToList();
            }
        }


        public async Task<Company> GetMulipleResult(int id)
        {
           var query = "Select * From companies where Id=@Id;"
                + "Select * From Employees where CompanyId=@Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryMultipleAsync(query, parameters);
                var company = await result.ReadSingleOrDefaultAsync<Company>();
                if (company != null)
                    company.Employees = (await result.ReadAsync<Employee>()).ToList();
                return company;
            }
        }

        public async Task<Company> UpdateCompany(int id, CompanyForUpdate company)
        {
            var CompanyFromDb = await GetCompany(id);
            if (CompanyFromDb == null) return null;
            var query = "Update companies set Name=@Name, Address=@Address,Country=@Country where Id=@Id;";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Name", company.Name);
            parameters.Add("Address", company.Address);
            parameters.Add("Country", company.Country);
            using (var Connection = _context.CreateConnection())
            {
                await Connection.ExecuteAsync(query, parameters);
                return new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };

            }
        }
    }
}
