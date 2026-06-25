using SalonApp.Modules.Services.Domain;
using SalonApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.Modules.Services.Application
{
    public class ServiceManager
    {
        private readonly IRepository<Service> _repository;
        public ServiceManager(IRepository<Service> serviceRepository)
        {
            _repository = serviceRepository;
        }
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Service?> GetServiceByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task AddServiceAsync(Service service)
        {
            await _repository.AddAsync(service);
            await _repository.SaveChangesAsync();
        }
        public async Task UpdateServiceAsync(Service service)
        {
            _repository.Update(service);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteServiceAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);
            if (service == null)
                throw new InvalidOperationException($"Service with id {id} not found.");

                _repository.Remove(service);
                await _repository.SaveChangesAsync();
            }
        }
}

