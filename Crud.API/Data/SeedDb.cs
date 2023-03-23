
using Crud.API.Data;
using Crud.Shared.Entities;

namespace Crud.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "categoria default 1" });
                _context.Categories.Add(new Category { Name = "categoria default 2" });
                _context.Categories.Add(new Category { Name = "otra categoria default" });

            }

            await _context.SaveChangesAsync();
        }
    }
}
