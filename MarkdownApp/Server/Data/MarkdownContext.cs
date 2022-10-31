using MarkdownApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

#nullable disable


namespace MarkdownApp.Server.Data
{
    public class MarkdownContext : DbContext
    {
        public MarkdownContext()
        {
        }
        public MarkdownContext(DbContextOptions<MarkdownContext> option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}

