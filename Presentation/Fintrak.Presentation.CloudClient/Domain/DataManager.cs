using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fintrak.Presentation.CloudClient.Domain
{
    public class DataManager
    {
        public DataManager()
        {
            _context = new FintrakCloudDBEntities();
        }

        private FintrakCloudDBEntities _context = null;

        public List<Company> GetCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company InsertCompany(Company company)
        {
            _context.Companies.Add(company);

            _context.SaveChanges();
            return company;
        }

        public Company UpdateCompany(Company company)
        {
            var entity = _context.Companies.Where(c=>c.Id == company.Id).FirstOrDefault();

            if (!entity.IsDemo && company.IsDemo )
            {
                entity.IsDemo = company.IsDemo;
                entity.DemoDuration = 100;
                entity.DemoStartDate = DateTime.Now;
                entity.DemoEndDate = DateTime.Now.AddDays(entity.DemoDuration);
            }

            if (!entity.IsActivated && company.IsActivated)
            {
                entity.IsActivated = company.IsActivated;
                entity.ActivationDate = DateTime.Now;
            }

            _context.SaveChanges();
            return company;
        }


    }
}