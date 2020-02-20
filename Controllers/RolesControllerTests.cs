using Flashcards.Controllers;
using Flashcards.ViewModels;
using Flashcards.Tests.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Flashcards.Tests.Controllers
{
    public class RolesControllerTests
    {
        [Fact]
        public void CreateRole_ReturnsViewResult()
        {
            var mockRoleManager = IdentityHelpers.MockRoleManager<IdentityRole>();
            var mockUserManager = IdentityHelpers.MockUserManager<IdentityUser>();
            var controller = new RolesController(mockRoleManager.Object, mockUserManager.Object);

            var result = controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreateRoleViewModel>(viewResult.ViewData.Model);
        }
    }
}
