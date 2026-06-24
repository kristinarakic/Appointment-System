using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.SharedKernel
{
    internal interface IEventDispatcher
    {
        Task DispatchAsync(IDomainEvent domainEvent);
    }
}
