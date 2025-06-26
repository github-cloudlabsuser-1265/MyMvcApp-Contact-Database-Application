using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Tests.Controllers
{
    public class UserControllerTest
    {
        private UserController GetControllerWithCleanState()
        {
            // Reset static fields for isolation
            typeof(UserController)
                .GetField("_users", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, new List<User>
                {
                    new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                    new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
                });
            typeof(UserController)
                .GetField("_nextId", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .SetValue(null, 3);
            return new UserController();
        }

        [Fact]
        public void Index_ReturnsViewWithUsers()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(result.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenIdIsNull()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenUserNotFound()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsView_WhenUserExists()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Details(1) as ViewResult;

            Assert.NotNull(result);
            var user = Assert.IsType<User>(result.Model);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_InvalidModel_ReturnsView()
        {
            var controller = GetControllerWithCleanState();
            controller.ModelState.AddModelError("Name", "Required");
            var user = new User();

            var result = controller.Create(user);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_ValidModel_AddsUserAndRedirects()
        {
            var controller = GetControllerWithCleanState();
            var user = new User { Name = "Alice", Email = "alice@example.com" };

            var result = controller.Create(user);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            // Check user was added
            var users = controller.Index() as ViewResult;
            Assert.Contains(((IEnumerable<User>)users.Model), u => u.Name == "Alice" && u.Email == "alice@example.com");
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsNotFound_WhenUserNotFound()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Edit(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Get_ReturnsView_WhenUserExists()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            var user = Assert.IsType<User>(result.Model);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void Edit_Post_IdMismatch_ReturnsNotFound()
        {
            var controller = GetControllerWithCleanState();
            var user = new User { Id = 2, Name = "Test", Email = "test@example.com" };

            var result = controller.Edit(1, user);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_InvalidModel_ReturnsView()
        {
            var controller = GetControllerWithCleanState();
            controller.ModelState.AddModelError("Name", "Required");
            var user = new User { Id = 1, Name = "", Email = "test@example.com" };

            var result = controller.Edit(1, user);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_Post_UserNotFound_ReturnsNotFound()
        {
            var controller = GetControllerWithCleanState();
            var user = new User { Id = 999, Name = "Test", Email = "test@example.com" };

            var result = controller.Edit(999, user);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_ValidModel_UpdatesUserAndRedirects()
        {
            var controller = GetControllerWithCleanState();
            var user = new User { Id = 1, Name = "Updated", Email = "updated@example.com" };

            var result = controller.Edit(1, user);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            var users = controller.Index() as ViewResult;
            Assert.Contains(((IEnumerable<User>)users.Model), u => u.Name == "Updated" && u.Email == "updated@example.com");
        }

        [Fact]
        public void Delete_Get_ReturnsNotFound_WhenIdIsNull()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Delete(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Get_ReturnsNotFound_WhenUserNotFound()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Get_ReturnsView_WhenUserExists()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.Delete(1) as ViewResult;

            Assert.NotNull(result);
            var user = Assert.IsType<User>(result.Model);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void DeleteConfirmed_RemovesUserAndRedirects()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.DeleteConfirmed(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            var users = controller.Index() as ViewResult;
            Assert.DoesNotContain(((IEnumerable<User>)users.Model), u => u.Id == 1);
        }

        [Fact]
        public void DeleteConfirmed_NonExistingUser_Redirects()
        {
            var controller = GetControllerWithCleanState();

            var result = controller.DeleteConfirmed(999);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
    }
}