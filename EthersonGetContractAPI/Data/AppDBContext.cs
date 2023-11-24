using EthersonGetContractAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace EthersonGetContractAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> option) : base(option)
        {

        }
        public DbSet<Contracts> contracts { set; get; }
   
    }
}
