using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;
using RestWithASPNetUdemy.Hypermedia.Filters;

namespace RestWithASPNetUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookService)
        {
            _logger = logger;
            _bookBusiness = bookService;
        }

        [HttpGet]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Get(int id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        [TypeFilter(typeof(HypermediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null)
                return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);            
            return NoContent();
        }

    }
}