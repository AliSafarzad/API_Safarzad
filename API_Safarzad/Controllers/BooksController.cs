using IInterfacse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API_Safarzad.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
       /* 28448*/
        private readonly IBook _ibook;
        private readonly IConfiguration _configuration;
        public BooksController(IConfiguration configuration, IBook book)
        {
            _configuration = configuration;
            _ibook = book;
        }
        [HttpPost]
        public IActionResult InsertBook([FromBody] Book book)
        {
           int _natije =  _ibook.InsertBook(book);
            return Ok(_natije);
        }
        [HttpPost]
        public IActionResult UpdateBook(UpdateBook book)
        {
            int _update = _ibook.UpdateBook(book);
            return Ok(_update);
        }
        [HttpPost]
        public IActionResult SelectBooks()
        {
            var _list = _ibook.SelectBooks();
            return Ok(_list);
        }

        [HttpPost]
        public IActionResult SelectBooksById(BookById bookById)
        {
            var _list = _ibook.SelectBooksById(bookById);
            return Ok(_list);
        }
        [HttpPost]
        public IActionResult DeleteBook(BookById bookById)
        {
            var _list = _ibook.DeleteBook(bookById);
            return Ok(_list);
        }

    }
}
