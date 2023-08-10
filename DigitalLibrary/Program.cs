using DigitalLibrary.Repository;

namespace DigitalLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Инициализация данных
            using (var db = new AppContext())
            {

                User user1 = new User { Name = "Вася", Books = new List<Book>() };
                User user2 = new User { Name = "Петя", Books = new List<Book>() };
                User user3 = new User { Name = "Маша", Books = new List<Book>() };
                User user4 = new User { Name = "Оля", Books = new List<Book>() };

                db.Users.AddRange(user1, user2, user3, user4);

                Book book1 = new Book { Name = "Книга 1", Year = 1900, Genre = "Жанр 1", Author = "Автор 1" };
                Book book2 = new Book { Name = "Книга 2", Year = 1901, Genre = "Жанр 2", Author = "Автор 2" };
                Book book3 = new Book { Name = "Книга 3", Year = 1902, Genre = "Жанр 3", Author = "Автор 3" };
                Book book4 = new Book { Name = "Книга 4", Year = 1903, Genre = "Жанр 4", Author = "Автор 4" };

                db.Books.AddRange(book1, book2, book3, book4);

                user1.Books.Add(book2);
                user2.Books.Add(book3);
                user3.Books.Add(book4);
                user3.Books.Add(book1);

                db.SaveChanges();
            }
            #endregion

            using ( var db = new AppContext() )
            {
                BookRepository books = new BookRepository(db);
                if (books.IsBookIssued("Книга 4"))
                    Console.WriteLine("Книга на руках");
                else
                    Console.WriteLine("Книга в билиотеке");

                Console.WriteLine(new string('-', 50));

                var list = books.GetBooks();
                foreach( var book in list )
                {
                    Console.WriteLine($"{book.Name} - {book.Author}");
                }

                Console.WriteLine(new string('-', 50));

                UserRepository users = new UserRepository(db);
                Console.WriteLine($"Кол-во книг у первого пользователя - {users.GetCountBooksIssued(1)}");

                Console.WriteLine(new string('-',50));

                var booksByGenre = books.GetBookByGenre("Жанр 2", 1900, 1902);
                
                foreach(var book in booksByGenre)
                {
                    Console.WriteLine($"{book.Name} - {book.Year} года");
                }

                
            }
        }
    }
}