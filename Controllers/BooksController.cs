using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Http.Headers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("")]
        public IActionResult GetAllBooks()
        {
            //throw new Exception();
            var books =  _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleBook([FromRoute] int id)
        {
            var book = _bookRepository.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetBooksByUserId([FromRoute] string id)
        {
            var book = await _bookRepository.GetBooksByUserId(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel bookModel)
        {
            var id = _bookRepository.AddBookAsync(bookModel);
            /*  if (bookModel.CoverPic != null)
              {
                  string folder = "Helper/Images/";
                  folder += DateTime.Now.ToString() + bookModel.CoverPic.FileName;
                  string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                  await bookModel.CoverPic.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
              }*/
            return CreatedAtAction(nameof(GetSingleBook), new { id = id, Controller = "books" }, id);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage()
        {
            var file = Request.Form.Files[0];
            string folder = "src/assets/";
            folder += DateTime.Now.ToString("yyyyMMddHHmmssffff") + file.FileName;
            string serverFolder = Path.Combine("D:\\Angular\\CRUD", folder);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return Ok(new {folder});
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookModel bookModel)
        {
            _bookRepository.UpdateBookAsync(id, bookModel);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchBook([FromRoute] int id, [FromBody] JsonPatchDocument bookModel)
        {
            await _bookRepository.UpdatePatchBookAsync(id, bookModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }

        [HttpPost("addFav")]    
        public async Task<IActionResult> AddFavourite([FromBody] FavouriteMission favouriteMission)
        {
            var id = await _bookRepository.AddFavourites(favouriteMission.MissionId,favouriteMission.UserId);
            return Ok(id);
        }

        [HttpDelete("removeFav/{id}")]
        public IActionResult RemoveFromFavourite([FromRoute] int id)
        {
            _bookRepository.RemoveFromFavourites(id);
            return Ok();
        }

        [HttpPost("getFavouriteMission")]
        public IActionResult GetFavMissionInfoById([FromBody] FavouriteMission favouriteMission)
        {
            var missionInfo = _bookRepository.GetFavouriteMissionById(favouriteMission.MissionId,favouriteMission.UserId);
            return Ok(missionInfo);
        }

    }
}
