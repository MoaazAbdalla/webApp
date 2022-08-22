using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCreate.model;
using MyCreate.Models;

namespace MyCreate.model
{


    public class NewsDb : DbContext
    {
        public NewsDb(DbContextOptions<NewsDb> options)
            : base(options)
        {
        }
        public DbSet<Categoty> categoties { set; get; }
        public DbSet<News> news { set; get; }
        public DbSet<ContactUs> contactUs { set; get; }
        public DbSet<Teammember> teammembers { set; get; }
        public DbSet<anahaber> anahaberleri { get; set; }


    }
}

