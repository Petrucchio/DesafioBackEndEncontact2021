﻿using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContactRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }


        public async Task<IContact> SaveAsync(IContact contact)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactDao(contact);
            dao.Id = await connection.InsertAsync(dao);
            return dao.Export();
        }
        public async Task<IContact> UpdateAsync(IContact contact)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactDao(contact);
            await connection.UpdateAsync(dao);
            return dao.Export();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Contact WHERE Id = @Id;");

            await connection.ExecuteAsync(sql.ToString(), new { Id = id });
        }




        public async Task<IEnumerable<IContact>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact";
            var result = await connection.QueryAsync<ContactDao>(query);

            var returnList = new List<IContact>();

            foreach (var ContatoSalvo in result.ToList())
            {
                IContact Contato = new Contact(ContatoSalvo.Id,ContatoSalvo.ContactBookId, ContatoSalvo.CompanyId, ContatoSalvo.Name.ToString(),ContatoSalvo.Phone.ToString(),ContatoSalvo.Email.ToString(),ContatoSalvo.Address.ToString());
                returnList.Add(Contato);
            }

            return returnList.ToList();
        }
        public async Task<IContact> GetAsync(int id)
        {
            var list = await GetAllAsync();

            return list.ToList().Where(item => item.Id == id).FirstOrDefault();
        }
    }
    [Table("Contact")]
    public class ContactDao : IContact
    {
        [Key]
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public ContactDao()
        {
        }

        public ContactDao(IContact contact)
        {
            Id = contact.Id;
            ContactBookId = contact.ContactBookId;
            CompanyId = contact.CompanyId;
            Name = contact.Name;
            Phone = contact.Phone;
            Email = contact.Email;
            Address = contact.Address;
            
        }

        public IContact Export() => new Contact(Id,ContactBookId, CompanyId,Name, Phone,Email, Address);
    }
}
