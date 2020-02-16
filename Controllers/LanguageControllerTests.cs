using Flashcards.Controllers;
using Flashcards.Data;
using Flashcards.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Flashcards.Tests.Controllers
{
    public class LanguageControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult()
        {
            //Arrange
            var mockRepo = MockLanguageRepository();
            var mockLanguages = MockLanguages();
            mockRepo.Setup(repo => repo.GetLanguagesAsync())
                .ReturnsAsync(mockLanguages);
            var controller = new LanguageController(mockRepo.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Language>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult_WithLanguage()
        {
            //Arrange
            var mockRepo = MockLanguageRepository();
            var controller = new LanguageController(mockRepo.Object);

            //Act
            var result = controller.Add();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<Language>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_Post_ReturnsRedirectToResult_OnSuccess()
        {
            var mockRepo = MockLanguageRepository();
            var mockLanguage = MockLanguage();
            mockRepo.Setup(repo => repo.AddAsync(mockLanguage))
                .ReturnsAsync(true);

            var controller = new LanguageController(mockRepo.Object);
            var result = await controller.Add(mockLanguage);

            Assert.IsType<RedirectToActionResult>(result);
        }

        private Language MockLanguage()
        {
            return new Language()
            {
                LanguageId = 1,
                EnglishName = "French",
                NativeName = "Français",
                Abbreviation = "fr"
            };
        }

        private IEnumerable<Language> MockLanguages()
        {
            var languages = new List<Language>();
            languages.Add(MockLanguage());
            return languages;
        }

        private Language MockEmptyLanguage()
        {
            return new Language(){};
        }

        private Mock<ILanguageRepository> MockLanguageRepository()
        {
            return new Mock<ILanguageRepository>();
        }
        
    }
}
