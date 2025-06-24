using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Controllers;
using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MyMvcApp.Tests
{
    public class UserControllerSimpleTests
    {
        private AppDbContext GetDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(connection)
                .Options;
            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            var context = GetDbContext();
            var controller = new UserController(context);

            var result = await controller.Index("");
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var context = GetDbContext();
            var controller = new UserController(context);

            var result = controller.Create();
            Assert.IsType<ViewResult>(result);
        }
    }
}
