using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud.API.Data;
using Crud.Shared.Entities;

namespace Crud.API.Controllers
{

    [ApiController]
    [Route("/api/categories")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;


        public CountriesController(DataContext context)
        {
            _context = context;
        }


        //Método GET LIST

        [HttpGet]
        public async Task<ActionResult> Get()
        {

            return Ok(await _context.Categories.ToListAsync());

        }

        //´Método GET con parámetro

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {

            var country = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (country is null)
            {
                return NotFound(); //404
            }

            return Ok(country);

        }




        // Método POST -- CREAR
        [HttpPost]
        public async Task<ActionResult> Post(Category category)
        {
            _context.Add(category);
            try
            {

                await _context.SaveChangesAsync();
                return Ok(category);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una categoria con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }





        //Método PUT --- UPDATE

        [HttpPut]
        public async Task<ActionResult> Put(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }



        // Método DELETE-- Eliminar
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Categories
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound(); //404
            }

            return NoContent(); //204
        }







    }
}