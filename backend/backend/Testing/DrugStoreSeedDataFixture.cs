using backend.DAL;
using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Testing
{
    public class DrugStoreSeedDataFixture : IDisposable
    {
        public DrugStoreContext context { get; private set; } 

        public DrugStoreSeedDataFixture()
        {
            var options = new DbContextOptionsBuilder<DrugStoreContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            context = new DrugStoreContext(options);
         
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
