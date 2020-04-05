using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Client.WebAPI.Entities
{
	public class ClientDBContext: DbContext
	{
		public ClientDBContext(DbContextOptions options)
			: base(options)
		{
		
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().ToTable("Users");
		}
		public DbSet<User> User { get; set; }
	}
}
