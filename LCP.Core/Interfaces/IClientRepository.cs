using LCP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LCP.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClientByIdAsync(int Id);
        Task<IReadOnlyList<Client>> GetClientsAsync();
        Task<Client> AddClientAsync(Client client);
        Task<bool> SaveAll();
        // Generic type of method,  we constrain the method for just classes
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
    }
}
