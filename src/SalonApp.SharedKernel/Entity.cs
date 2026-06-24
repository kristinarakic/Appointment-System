using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.SharedKernel
{
    public abstract class Entity
    {
        public int Id { get; set; }

        private readonly List<IDomainEvent> _domainEvents = new ();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomaintEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents() 
        {
            _domainEvents.Clear();
        }
    }
}
