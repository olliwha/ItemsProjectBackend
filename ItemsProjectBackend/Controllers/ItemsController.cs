using ItemLibrary;
using ItemsProjectBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItemsProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsRepository _repository;
        public ItemsController(ItemsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<ItemsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Item>> Get()
        {
            List<Item> items = _repository.GetAll();
            if (items.Count < 1) return NoContent();
            return Ok(items);
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Item> Get(int id)
        {
            Item? item = _repository.GetById(id);
            if (item == null) return NotFound();
            return Ok(item); // Correct method call is Ok()
        }

        // POST api/<ItemsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Item> Post([FromBody] Item item)
        {
            try
            {
                Item created = _repository.Add(item);
                return Created($"api/item/{created.Id}", created);
                //return Created($"/{created.Id}", created);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Item> Put(int id, [FromBody] Item item)
        {
            try
            {
                Item itemToUpdate = _repository.Update(id, item);
                if (itemToUpdate == null) return null;
                return Ok(itemToUpdate);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Item> Delete(int id)
        {
            Item itemToDelete = _repository.Delete(id);
            if (itemToDelete == null) return NotFound();
            return Ok(itemToDelete);
        }
    }
}
