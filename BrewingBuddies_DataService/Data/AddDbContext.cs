﻿using BrewingBuddies_Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


namespace BrewingBuddies_DataService.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<LeagueUserEntity> LeagueUsers { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

        }
    }
}
