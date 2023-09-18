﻿using System;
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

        public DbSet<User> users { get; set; }

        public DbSet<Notes> Notes { get; set; }

    }
}
