using System.Diagnostics;
using System.Net.Http.Headers;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace BlogTestUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BlogDataLibrary.Data.SqlData db = GetConnection();

            Authenticate(db);

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            Register(db);

            AddPost(db);

            ListPosts(db);

            ShowPostDetails(db);
        }
        static BlogDataLibrary.Data.SqlData GetConnection()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");
            IConfiguration config = builder.Build();
            ISqlDataAccess dbAccess = new SqlDataAccess(config);
            BlogDataLibrary.Data.SqlData db = new BlogDataLibrary.Data.SqlData(dbAccess);

            return db;
        }
        private static UserModel GetCurrentUser(BlogDataLibrary.Data.SqlData db)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            UserModel user = db.Authenticate(username, password);

            return user;
        }
        public static void Authenticate(BlogDataLibrary.Data.SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            if (user == null)
            {
                Console.WriteLine("Invalid Credentials.");
            }
            else
            {
                Console.WriteLine($"Welcome, {user.UserName}");
            }
        }

        public static void Register(BlogDataLibrary.Data.SqlData db)
        {
            Console.Write("Enter new username: ");
            var username = Console.ReadLine();

            Console.Write("Enter new password: ");
            var password = Console.ReadLine();

            Console.Write("Enter first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            var lastName = Console.ReadLine();

            db.Register(username, firstName, lastName, password);
        }
        private static void AddPost(BlogDataLibrary.Data.SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Write body: ");
            string body = Console.ReadLine();

            PostModel post = new PostModel
            {
                Title = title,
                Body = body,
                DateCreated = DateTime.Now,
                UserId = user.Id,
            };

            db.AddPost(post);
        }

        private static void ListPosts(BlogDataLibrary.Data.SqlData db)
        {
            List<ListPostModel> posts = db.listPosts();

            foreach (ListPostModel post in posts)
            {
                Console.WriteLine($"{post.Id}. Title: {post.Title} by {post.UserName}[{post.DateCreated.ToString("yyyy-MM-dd")}]");
                Console.WriteLine($"{post.Body.Substring(0, 20)}...");
                Console.WriteLine();

            }
        }

        private static void ShowPostDetails(BlogDataLibrary.Data.SqlData db)
        {
            Console.Write("Enter a post ID: ");
            int id = Int32.Parse(Console.ReadLine());
            
            ListPostModel post = db.ShowPostDetails(id);
            Console.WriteLine(post.Title);
            Console.WriteLine($"by {post.FirstName} {post.LastName} [{post.UserName}]");

            Console.WriteLine();

            Console.WriteLine(post.Body);

            Console.WriteLine(post.DateCreated.ToString("MMM d yyyy"));
        }
    }
    
}