using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;

namespace RepoLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public DbSet<User> UserTable { get; set; }

        public DbSet<Notes> NoteTable { get; set; }

        public DbSet<LabelEntity> Label { get; set; } 

        public DbSet<CollabEntity> Collab { get; set; }

    }
}
