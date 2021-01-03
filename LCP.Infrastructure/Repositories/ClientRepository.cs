using LCP.Core.Entities;
using LCP.Core.Interfaces;
using LCP.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LCP.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly LcpContext _lcpContext;

        public ClientRepository(LcpContext lcpContext)
        {
            _lcpContext = lcpContext;
        }

        public async Task<Client> AddClientAsync(Client client)
        {
            await _lcpContext.Clients.AddAsync(client);
            var result = await _lcpContext.SaveChangesAsync();

            if (result != 0)
            {
                return client;
            }

            return null;
        }

        public async Task<Client> GetClientByIdAsync(int Id)
        {
            return await _lcpContext.Clients.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<IReadOnlyList<Client>> GetClientsAsync()
        {
            // EF Eager loading (We include the FKs)
            return await _lcpContext.Clients
                .ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            // if this return more than 0 we return true
            // It means we saved objects
            return await _lcpContext.SaveChangesAsync() > 0;
        }

        // We don`t use async because we don`t querying the context
        // This is saved in memory until we actually save changes
        public void Add<T>(T entity) where T : class
        {
            _lcpContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _lcpContext.Remove(entity);
        }

    }
}
