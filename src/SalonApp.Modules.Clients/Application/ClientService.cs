using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SalonApp.SharedKernel;
using SalonApp.Modules.Clients.Domain;

namespace SalonApp.Modules.Clients.Application;

public class ClientService
{
    private readonly IRepository<Client> _repository;

    public ClientService(IRepository<Client> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddClientAsync(Client client)
    {
        await _repository.AddAsync(client);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateClientAsync(Client client)
    {
        _repository.Update(client);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await _repository.GetByIdAsync(id);
        if (client == null)
            throw new InvalidOperationException("Klijent nije pronadjen.");

        _repository.Remove(client);
        await _repository.SaveChangesAsync();
    }
}