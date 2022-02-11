using System;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;

namespace TesteBackendEnContact.Controllers.Models.ContactCsv
{
    public class Csv
    {
        [Name("Id")]
        public int Id { get; set; }
        [Name("ContactBookId")]
        public int ContactBookId { get; set; }
        [Name("CompanyId")]
        public int CompanyId { get; set; }
        [Name("Name")]
        public string Name { get; set; }
        [Name("Phone")]
        public string Phone { get; set; }
        [Name("Email")]
        public string Email { get; set; }
        [Name("Address")]
        public string Address { get; set; }
    }
    public class ContactMap : ClassMap<Csv>
    {
        public ContactMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.ContactBookId).Name("ContactBookId");
            Map(m => m.CompanyId).Name("CompanyId");
            Map(m => m.Name).Name("Name");
            Map(m => m.Phone).Name("Phone");
            Map(m => m.Email).Name("Email");
            Map(m => m.Address).Name("Address");
        }
    }
}