using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Model;

namespace MinimalAPI.Data { 

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {


    }

        public DbSet<BankRequest> BankRequests => Set<BankRequest>();

        public DbSet<Bank> Banks => Set<Bank>();

    }

}







