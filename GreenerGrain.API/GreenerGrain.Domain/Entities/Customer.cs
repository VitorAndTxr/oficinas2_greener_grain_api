using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class Customer : AuditEntity<Guid>
    {       
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public virtual ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

        protected Customer() { }
        public Customer(string name, string email, string phoneNumber, string address)
        {
            SetId(Guid.NewGuid());
            Activate();

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void UpdateDetails(string name, string phoneNumber, string address)
        {
            Name = name;            
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
