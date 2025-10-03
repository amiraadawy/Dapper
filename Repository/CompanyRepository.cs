using Dapper;
using DapperAsp.Net.Context;
using DapperAsp.Net.Contract;
using DapperAsp.Net.Dto;
using DapperAsp.Net.Enitities;

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
    }
}
