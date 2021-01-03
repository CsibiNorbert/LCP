using LCP.Core.Entities;
using LCP.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LCP.Infrastructure.SeedData
{
    public class LcpContextSeed
    {
		public static async Task SeedAsync(LcpContext lcpContext, ILoggerFactory loggerFactory)
		{
			try
			{
				// Any checks for any records
				if (!lcpContext.Clients.Any())
				{
					var clientsData = File.ReadAllText("../LCP.Infrastructure/SeedData/clients.json");
					var clients = JsonSerializer.Deserialize<List<Client>>(clientsData);

					foreach (var client in clients)
					{
						lcpContext.Clients.Add(client);
					}

					await lcpContext.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<LcpContextSeed>();
				logger.LogError(ex.Message);
			}
		}
	}
}
