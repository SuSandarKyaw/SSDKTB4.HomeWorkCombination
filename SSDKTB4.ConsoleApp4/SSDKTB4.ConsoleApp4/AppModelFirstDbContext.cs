using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SSDKTB4.ConsoleApp4
{
    internal class AppModelFirstDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
			{

				DataSource = ".",
				InitialCatalog = "SSDKMiniPOS",
				UserID = "sa",
				Password = "sasa@123",
				TrustServerCertificate = true
			};

			optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
		}

		public DbSet<FestivalEntity> Festivals { get; set; }
	}
    }

