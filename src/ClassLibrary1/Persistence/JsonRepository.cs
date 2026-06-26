using SalonApp.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalonApp.Infratructure.Persistence
{
    public class JsonRepository<T> : IRepository<T> where T : Entity
    {
        private readonly string _filePath;
        private List<T> _items;

        public JsonRepository(string dataDirectory)
        {
            _filePath = Path.Combine(dataDirectory, $"{typeof(T).Name}.json");
            _items = LoadFromFile();
        }
        public Task AddAsync(T entity)
        {
            entity.Id = _items.Any() ? _items.Max(e => e.Id) + 1 : 1;
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(_items);
        }

        public Task<T?> GetByIdAsync(int id)
        {
            return Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
        }

        public void Remove(T entity)
        {
            _items.RemoveAll(x => x.Id == entity.Id);
        }

        public Task SaveChangesAsync()
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
            return Task.CompletedTask;
        }

        public void Update(T entity)
        {
            var index = _items.FindIndex(x => x.Id == entity.Id);
            if (index >= 0)
                _items[index] = entity;
        }

        private List<T> LoadFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<T>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }
}
