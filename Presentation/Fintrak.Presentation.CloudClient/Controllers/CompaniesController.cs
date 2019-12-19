using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Fintrak.Presentation.CloudClient.Domain;
using Fintrak.Shared.Common.Utils;
using System.Configuration;
using System.Net.Mail;

namespace Fintrak.Presentation.CloudClient.Controllers
{
     [RoutePrefix("api/Companies")]
    public class CompaniesController : ApiController
    {
        private FintrakCloudDBEntities db = new FintrakCloudDBEntities();

        // GET: api/Companies
        public IQueryable<Company> GetCompanies()
        {
            return db.Companies;
        }

        // GET: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT: api/Companies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompany(int id, Company company)
        {
            var companyEntity = db.Companies.Where(c => c.Id == company.Id).FirstOrDefault();

            if (companyEntity != null)
            {
                var host = ConfigurationManager.AppSettings["MailServer"];
                var sender = ConfigurationManager.AppSettings["EmailFromAddress"];
                var recipient = ConfigurationManager.AppSettings["EmailToAddress"];

                var subject = "";
                var body = "Hello </br>";

                if (id == 1)
                {
                    companyEntity.IsDemo = !company.IsDemo;

                    if (companyEntity.IsDemo)
                    {
                        companyEntity.DemoDuration = 100;
                        companyEntity.DemoStartDate = DateTime.Now;
                        companyEntity.DemoEndDate = DateTime.Now.AddDays(companyEntity.DemoDuration);
                    }

                    if (companyEntity.IsDemo)
                    {
                        subject = "Activate PI 360 demo for company:" + companyEntity.Name;
                        body += "Please find below the information required to activate demo for the above named company" + "</br>";
                    }
                    else
                    {
                        subject = "Deactivate PI 360 demo for company:" + companyEntity.Name;
                        body += "Please find below the information required to deactivate demo for the above named company" + "</br>";
                    }
                }
                else
                {
                    companyEntity.IsActivated = !company.IsActivated;

                    if (companyEntity.IsActivated)
                    {
                        subject = "Activate PI 360 for company:" + companyEntity.Name;
                        body += "Please find below the information required to activate for the above named company" + "</br>";
                    }
                    else
                    {
                        subject = "Deactivate PI 360 for company:" + companyEntity.Name;
                        body += "Please find below the information required to deactivate for the above named company" + "</br>";
                    }
                }

                db.SaveChanges();

                body += "Name : " + companyEntity.Name + "</br>";
                body += "Code : " + companyEntity.Code + "</br>";
                body += "</br> Regards.</br>";

                MailMessage mailMessage = new MailMessage();
                MailAddress fromAddress = new MailAddress(sender);
                mailMessage.From = fromAddress;
                mailMessage.To.Add(recipient);
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = host;
                smtpClient.Send(mailMessage);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Companies
        [ResponseType(typeof(Company))]
        public IHttpActionResult PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Companies.Add(company);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [ResponseType(typeof(Company))]
        public IHttpActionResult DeleteCompany(int id)
        {
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return NotFound();
            }

            db.Companies.Remove(company);
            db.SaveChanges();

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.Id == id) > 0;
        }

        // PUT: api/Companies/ProcessDemo/5
        [Route("ProcessDemo/{companyId}")]
        [HttpGet]
        [ResponseType(typeof(void))]
        public IHttpActionResult ProcessDemo(int companyId)
        {
            var company = db.Companies.Where(c => c.Id == companyId).FirstOrDefault();

            if (company != null)
            {
                company.IsDemo = !company.IsDemo;

                if (company.IsDemo)
                {
                    company.DemoDuration = 100;
                    company.DemoStartDate = DateTime.Now;
                    company.DemoEndDate = DateTime.Now.AddDays(company.DemoDuration);
                }

                db.SaveChanges();

                var sender = ConfigurationManager.AppSettings["EmailFromAddress"];
                var recipient = ConfigurationManager.AppSettings["EmailToAddress"];

                var subject = "";
                var body = "Test";

                if (company.IsDemo)
                {
                    subject = "Activate PI 360 demo for company:" + company.Name;
                }
                else
                {
                    subject = "Deactivate PI 360 demo for company:" + company.Name;
                }

                var mailHelper = new MailHelper
                {
                    Sender = sender,
                    Recipient = recipient,
                    RecipientCC = null,
                    Subject = subject,
                    Body = body
                };

                mailHelper.Send();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}