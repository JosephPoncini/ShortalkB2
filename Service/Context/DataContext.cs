using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortalkB2.Models;

namespace ShortalkB2.Service.Context
{

    public class DataContext : DbContext
    {
        
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;

        public DbSet<GameModel> GameInfo { get; set; }
        // public DbSet<GameModel> GameInfo {get; set;}


        public DataContext(DbContextOptions options) : base(options) { }

        //this function will build out our table in the database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        internal object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}