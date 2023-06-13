using GreenerGrain.Framework.Database.EfCore.Model;
using System;

namespace GreenerGrain.Domain.Entities
{
    public class AccountWallet : AuditEntity<Guid>
    {
        public AccountWallet()
        {
            SetId(Guid.NewGuid());
        }

        public void RemoveCredits(float value)
        {
            Credits = Credits - value;
        }

        public void AddCredits(float value)
        {
            Credits = Credits - value;
        }
        public float Credits { get; private set; }
        public Guid AccountId { get; private set; }
        public Account Account { get; private set; }
    }
}