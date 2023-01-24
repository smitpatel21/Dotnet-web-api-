using Microsoft.AspNetCore.JsonPatch;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IBookRepository
    {
        public List<BookModel> GetAllBooksAsync();
        public BookModel GetBookByIdAsync(int bookId);
        public Task<List<BookModel>> GetBooksByUserId(string userId);
        public int AddBookAsync(BookModel bookModel);
        public void UpdateBookAsync(int id, BookModel bookModel);
        public Task UpdatePatchBookAsync(int id, JsonPatchDocument bookModel);
        public Task DeleteBookAsync(int id);
        public void RemoveFromFavourites(int missionId);
        public Task<int> AddFavourites(int missionId, string id);
        public FavouriteMission GetFavouriteMissionById(int missionId,string userId);
    }
}
