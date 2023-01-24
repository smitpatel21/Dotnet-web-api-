using Dapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IConfiguration _configuration;

        public BookRepository(BookStoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public List<BookModel> GetAllBooksAsync()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connection.Query<BookModel>("select * from books").ToList();

            /*var records = _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CoverPicUrl = x.CoverPicUrl,
                UserId = x.UserId,
                mission = x.mission,
                //CoverPic=x.CoverPic,
                Date = x.Date
            }).ToList();
            return records;*/
        }

        public BookModel GetBookByIdAsync(int bookId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connection.Query<BookModel>("select * from books where Id=@bookId", new { bookId = bookId }).FirstOrDefault();
            /*            var record = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Date = x.Date,
                            UserId = x.UserId,
                            CoverPicUrl = x.CoverPicUrl,
                            mission = x.mission,

                            Description = x.Description
                        }).FirstOrDefaultAsync();
                        return record;*/
        }

        public async Task<List<BookModel>> GetBooksByUserId(string userId)
        {
            var record = await _context.Books.Where(x => x.UserId == userId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Date = x.Date,
                CoverPicUrl = x.CoverPicUrl,
                mission = x.mission,
                UserId = x.UserId,
                Description = x.Description
            }).ToListAsync();
            return record;
        }

        public int AddBookAsync(BookModel bookModel)
        {
                var sql =
                    "Insert Into books (Title,Description,Date,mission,CoverPicUrl,UserId) Values (@Title,@Description,@Date,@mission,@CoverPicUrl,@UserId)";
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            int id = connection.Query<int>(sql,
                new
                {
                    Title = bookModel.Title,
                    Description = bookModel.Description,
                    Date = bookModel.Date,
                    mission = bookModel.mission,
                    CoverPicUrl = bookModel.CoverPicUrl,
                    UserId = bookModel.UserId
                }).FirstOrDefault();
            bookModel.Id = id;
            /*var book = new BookModel()
            {
                Title = bookModel.Title,
                Description = bookModel.Description,
                Date = bookModel.Date,
                mission = bookModel.mission,
                CoverPicUrl = bookModel.CoverPicUrl,
                UserId = bookModel.UserId
            };
            _context.Books.Add(book);
            _context.SaveChanges();
            return book.Id;*/
            return id;
        }

        public void UpdateBookAsync(int id, BookModel bookModel)
        {
            var sql =
               "Update books set Title=@Title,Description=@Description,Date=@Date,mission=@mission,CoverPicUrl=@CoverPicUrl,UserId=@UserId where Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Execute(sql,
                new
                {
                    Id=id,
                    Title = bookModel.Title,
                    Description = bookModel.Description,
                    Date = bookModel.Date,
                    mission = bookModel.mission,
                    CoverPicUrl = bookModel.CoverPicUrl,
                    UserId = bookModel.UserId
                });
            /*var book = new BookModel()
            {
                Id = id,
                Title = bookModel.Title,
                Date = bookModel.Date,
                mission = bookModel.mission,
                //CoverPicUrl = bookModel.CoverPicUrl,
                Description = bookModel.Description,
                UserId = bookModel.UserId
            };
            _context.Books.Update(book);
            _context.SaveChanges();*/

        }

        public async Task UpdatePatchBookAsync(int id, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = new BookModel() { Id = id };
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddFavourites(int missionId, string id)
        {
            var favouriteMission = new FavouriteMission()
            {
                MissionId = missionId,
                UserId = id
            };
            _context.favouriteMissions.Add(favouriteMission);
            await _context.SaveChangesAsync();
            return favouriteMission.Id;
        }

        public void RemoveFromFavourites(int missionId)
        {
            var sql =
                "Delete from favouriteMissions where MissionId=@Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Execute(sql, new { Id = missionId });
           /* var mission = new FavouriteMission()
            {
                MissionId = missionId,
            };
            _context.favouriteMissions.Remove(mission);
            _context.SaveChanges();*/
        }

        public FavouriteMission GetFavouriteMissionById(int missionId,string userId)
        {
            var sql =
                "select * from favouriteMissions where MissionId=@Id and UserId=@userId";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return connection.Query<FavouriteMission>(sql, new { Id=missionId,userId=userId }).FirstOrDefault();
        }

    }
}
