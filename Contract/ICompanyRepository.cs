using DapperAsp.Net.Dto;
using DapperAsp.Net.Enitities;

namespace DapperAsp.Net.Contract
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompany(int id);
        public Task<Company> CreateCompany(CompanyForcreationDTO company);
        public Task<Company> UpdateCompany(int id, CompanyForUpdate company);
        public Task<bool> DeleteCompany(int id);
        public Task<Company> GetCompanyByEmployeeId(int id);
        public Task<Company> GetMulipleResult(int id);
        public Task<List<Company>> GetMulipleMapping();

    }
}
