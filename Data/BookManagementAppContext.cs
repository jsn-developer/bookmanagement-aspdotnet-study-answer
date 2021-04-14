using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookManagementApp.Models;

    public class BookManagementAppContext : DbContext
    {
        public BookManagementAppContext (DbContextOptions<BookManagementAppContext> options)
            : base(options)
        {
        }

        public DbSet<BookManagementApp.Models.Person> Person { get; set; }

        public DbSet<BookManagementApp.Models.Book> Book { get; set; }
    }
