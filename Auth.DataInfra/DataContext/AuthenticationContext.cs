using Auth.DataInfra.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.DataInfra.DataContext
{
	public class AuthenticationContext : DbContext
	{
		/// <summary>
		/// Constructor to build dbcontext
		/// </summary>
		/// <param name="options"></param>
		public AuthenticationContext(DbContextOptions options)
			: base(options)
		{

		}

		public DbSet<AuthEntity> AuthEntities { get; set; }

		/// <summary>
		/// Map Entity to table
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Mapping the table
			modelBuilder.Entity<AuthEntity>().ToTable("LoginDetails").HasKey(b => b.Email);

			//Configuring columns for SQL table
			modelBuilder.Entity<AuthEntity>()
				.Property(b => b.Email)
				.IsRequired().HasColumnType("nvarchar(20)");
			modelBuilder.Entity<AuthEntity>()
				.Property(b => b.Password)
				.IsRequired().HasColumnType("nvarchar(20)");
			modelBuilder.Entity<AuthEntity>()
				.Property(b => b.LoginIndex)
				.IsRequired().HasColumnType("nvarchar(MAX)");

			//Seeding the data
			modelBuilder.Entity<AuthEntity>().HasData(new AuthEntity
			{
				Email = "test@gmail.com",
				Password = "pwd",
				LoginIndex = "06/29/2020 05:50"

			}, new AuthEntity
			{
				Email = "test@yahoo.com",
				Password = "pwd",
				LoginIndex = "06/29/2020 05:50"
			});

		}
	}
}
