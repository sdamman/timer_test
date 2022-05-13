using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTimer.Data.Services
{
	public static class Connection
	{

		public static string Text { get; }


		// Connection to PigCHAMP's agview database.
		static Connection()
		{
			string jsonPath = @"appsettings.json";
			string mPath = AppDomain.CurrentDomain.BaseDirectory;
			mPath = mPath.Replace("MyTimer.Data", "MyTimer.Web");
			jsonPath = System.IO.Path.Combine(mPath, jsonPath);
			
			IConfigurationRoot config;
			var builder = new ConfigurationBuilder()
            .AddJsonFile(jsonPath);

			config = builder.Build();

			string connStr = config["ConnectionStrings:DataSource"] 
										 + config["ConnectionStrings:DefaultConnection"];
			string id = config["ConnectionStrings:UserId"];
			string pwd = config["ConnectionStrings:Pwd"];
			string key = config["ConnectionStrings:RndEnd"];
			connStr = connStr.Replace("UID", Encryption.Decrypt(id, key));
			Text = connStr.Replace("PWD", Encryption.Decrypt(pwd, key));
		}

	}
}
