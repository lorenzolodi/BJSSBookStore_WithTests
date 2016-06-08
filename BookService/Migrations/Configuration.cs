namespace BookService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BookService.Models.BookServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookService.Models.BookServiceContext context)
        {
        }
    }
}
