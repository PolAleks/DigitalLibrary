using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{
    public class BookRepository 
    {
        private AppContext _context;
        private DbSet<Book> _books;

        public BookRepository(AppContext context)
        {
            _context = context;
            _books = _context.Books;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _books.ToList();
        }

        public Book? GetBookById(int id)
        {
            return _books.FirstOrDefault(book => book.Id == id );
        }

        public void Create(Book book)
        {
            _books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateDateById(int id, int year)
        {
            Book? book = _books.FirstOrDefault(book => book.Id == id) ;
            if (book is not null)
            {
                book.Year = year;
                _context.SaveChanges();
            }
        }

        public void Delete(Book book)
        {
            _books.Remove(book);
            _context.SaveChanges();
        }
        /// <summary>
        /// Список книг по жанру выпущенных в период от dateFrom до dateTo
        /// </summary>
        /// <param name="genre">жанр</param>
        /// <param name="dateFrom">дата от, включительно</param>
        /// <param name="dateTo">год до, включительно</param>
        /// <returns></returns>
        public IQueryable<Book> GetBookByGenre(string genre, int dateFrom, int dateTo)
        {
            return _books.Where(b => b.Genre == genre)
                         .Intersect(
                   _books.Where(b => Enumerable.Range(dateFrom,dateTo).Contains(b.Year)));
        }
        /// <summary>
        /// Кол-во книг определенного автора в библиотеке
        /// </summary>
        /// <param name="author">автор</param>
        /// <returns></returns>
        public int GetCountBookByAuthor(string author)
        {
            return _books.Where(b => b.Author == author).Count();
        }
        /// <summary>
        /// Кол-во книг определенного жанра
        /// </summary>
        /// <param name="genre">жанр</param>
        /// <returns></returns>
        public int GetCountBookByGenre(string genre)
        {
            return _books.Where(b => b.Genre.Contains(genre)).Count();
        }
        /// <summary>
        /// Признак наличия книги по автору и названию в библиотеке
        /// </summary>
        /// <param name="author">автор</param>
        /// <param name="name">название книги</param>
        /// <returns>Возвращает true/false</returns>
        public bool HasBookByAuthorAndName(string author, string name)
        {
            return _books.Where(b => b.Author.Contains(author) && b.Name.Contains(name)).Any();
        }
        /// <summary>
        /// Признак выдачи книги на руки
        /// </summary>
        /// <param name="name">название книги</param>
        /// <returns>Возвращает признак выдачи книги true/false</returns>
        public bool IsBookIssued(string name)
        {
            Book book = _books.First(b => b.Name == name);
            if (book.UserId != null) return true;
            return false;
        }
        /// <summary>
        /// Метод возвращающий последнюю вышедшую книгу
        /// </summary>
        /// <returns>Возвращает экземпляр книги</returns>
        public Book GetLastPublishedBook()
        {
            int last = _books.Max(b => b.Year);
            return _books.First(b => b.Year == last);
        }
        /// <summary>
        /// Список всех книг отсортированных по названию
        /// </summary>
        /// <returns></returns>
        public IQueryable<Book> GetAllSortedBooks()
        {
            return _books.OrderBy(b => b.Name);
        }
        /// <summary>
        /// Список всех книг отсортированных по дате публикации в порядке убывания
        /// </summary>
        /// <returns></returns>
        public IQueryable<Book> GetAllSortedBookByYear()
        {
            return _books.OrderByDescending(b => b.Year);
        }

    }
}
