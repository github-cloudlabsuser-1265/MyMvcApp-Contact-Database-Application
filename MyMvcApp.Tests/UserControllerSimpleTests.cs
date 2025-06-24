using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;

namespace MyMvcApp.Tests
{
    public class UserControllerSimpleTests
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            var controller = new UserController();
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            var controller = new UserController();
            var result = controller.Create();
            Assert.IsType<ViewResult>(result);
        }
    }
}
