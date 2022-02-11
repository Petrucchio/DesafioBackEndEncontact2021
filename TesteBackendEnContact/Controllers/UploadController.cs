using Microsoft.AspNetCore.Mvc;
using CsvHelper.Configuration;
using System;
using System.IO;
using CsvHelper;
using TesteBackendEnContact.Controllers.Models.ContactCsv;

namespace TesteBackendEnContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        static void ops(string[] args)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader("C:/Users/Farias/Documents/GitHub/DesafioBackEndEncontact2021/Contacts_import.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var contacts = csv.GetRecords<Csv>();

                foreach (var contact in contacts)
                    Console.WriteLine($"Id: {contact.Id}, ContactBookId: {contact.ContactBookId}, CompanyId: {contact.CompanyId} ");
            }
        }
    }
}



