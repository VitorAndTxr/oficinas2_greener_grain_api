using GreenerGrain.Domain.Enumerators;
using GreenerGrain.Framework.Database.EfCore.Model;
using System;
using System.Collections.Generic;

namespace GreenerGrain.Domain.Entities
{
    public class Unit : AuditEntity<Guid>
    {
        public Unit()
        {
            SetId(Guid.NewGuid());
        }

        public void isAlive(string ip)
        {
            Ip = ip;
            State = UnitStateEnum.Idle;
            UpdateDate = DateTime.Now;
        }

        public void isOffline()
        {
            State = UnitStateEnum.Offline;
            UpdateDate = DateTime.Now;
        }

        public string Code { get; private set; }
        public string MAC { get; private set; }

        public string Ip { get; private set; }
        public UnitStateEnum State { get; private set; }

        public Guid OwnerId { get; private set; }

        public virtual Account Owner { get; private set; }
        public virtual ICollection<Module> Modules { get; private set; } = new List<Module>();

    }
}