using Moq;
using FluentAssertions;
using Application.UseCases;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using System.Reflection;

namespace UnitTest
{
    public class UnitTestGenderPreference
    {
        [Fact]
        public async void GetAllByUserId_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            string userurl = "https://localhost:7020";

            List<GenderResponse> genders = new List<GenderResponse>();
            GenderResponse genderResponse = new GenderResponse{ GenderId = 1, Description = "Masculino" };
            genders.Add(genderResponse);

            mockUserApiService.Setup(s => s.GetAllGenders(It.IsAny<string>())).
                                    ReturnsAsync(genders);

            List<GenderPreference> listaResponse = new List<GenderPreference>();
            GenderPreference genderPreference = new GenderPreference { UserId = 1, GenderId = 1 };
            listaResponse.Add(genderPreference);

            mockQuery.Setup(q => q.GetAllByUserId(It.IsAny<int>())).
                                    ReturnsAsync(listaResponse);

            //Act
            var lista = await service.GetAllByUserId(userurl, userId);

            //Assert
            Assert.True(lista.Any());
        }

        [Fact]
        public async void GetAllByUserId_Empty()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            string userurl = "https://localhost:7020";
            List<GenderResponse> genders = new List<GenderResponse>();
            GenderResponse genderResponse = new GenderResponse
            {
                GenderId = 1,
                Description = "Masculino"
            };
            genders.Add(genderResponse);

            mockUserApiService.Setup(q => q.GetAllGenders(It.IsAny<string>())).
                                    ReturnsAsync(genders);

            //Act
            var lista = await service.GetAllByUserId(userurl, userId);

            //Assert
            Assert.True(!lista.Any());
        }

        [Fact]
        public async void GetAllByUserId_Null()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            string userurl = "https://localhost:7020";

            //Act
            var lista = await service.GetAllByUserId(userurl, userId);

            //Assert
            Assert.Null(lista);
        }

        [Fact]
        public async void GetAllByUserId_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            string userurl = "https://localhost:7020";

            mockUserApiService.Setup(q => q.GetAllGenders(It.IsAny<string>())).ThrowsAsync(new Exception("Error al realizar consulta"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetAllByUserId(userurl, userId));
        }

        [Fact]
        public async void Insert_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            int genderId = 1;
            string userurl = "https://localhost:7020";
            GenderPreferenceReq request = new GenderPreferenceReq { GenderId = genderId };

            GenderResponse genderResponse = new GenderResponse { GenderId = genderId, Description = "Masculino" };
            mockUserApiService.Setup(s => s.GetGenderById(It.IsAny<string>(), It.IsAny<int>())).
                                    ReturnsAsync(genderResponse);

            //Act
            var response = await service.Insert(userurl, request, userId);

            //Assert
            response.UserId.Should().Be(userId);
            response.GenderId.Should().Be(genderResponse.GenderId);
            response.GenderName.Should().Be(genderResponse.Description);
        }

        [Fact]
        public async void Insert_Null()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            int genderId = 1;
            string userurl = "https://localhost:7020";
            GenderPreferenceReq request = new GenderPreferenceReq { GenderId = genderId };

            GenderResponse genderResponse = new GenderResponse { GenderId = genderId, Description = "Masculino" };
            mockUserApiService.Setup(s => s.GetGenderById(It.IsAny<string>(), It.IsAny<int>())).
                                    ReturnsAsync(genderResponse);

            GenderPreference genderPreference = new GenderPreference { UserId = userId, GenderId = genderId };
            mockQuery.Setup(q => q.GetById(It.IsAny<int>(), It.IsAny<int>())).
                                    ReturnsAsync(genderPreference);

            //Act
            var response = await service.Insert(userurl, request, userId);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Insert_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            int genderId = 1;
            string userurl = "https://localhost:7020";
            GenderPreferenceReq request = new GenderPreferenceReq { GenderId = genderId };

            mockUserApiService.Setup(q => q.GetGenderById(It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new Exception("Error al insertar"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.Insert(userurl, request, userId));
        }


        [Fact]
        public async void Delete_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            int genderId = 1;
            string userurl = "https://localhost:7020";
            GenderPreferenceReq request = new GenderPreferenceReq { GenderId = genderId };

            GenderResponse genderResponse = new GenderResponse { GenderId = genderId, Description = "Masculino" };
            mockUserApiService.Setup(s => s.GetGenderById(It.IsAny<string>(), It.IsAny<int>())).
                                    ReturnsAsync(genderResponse);

            GenderPreference genderPreference = new GenderPreference { UserId = userId, GenderId = genderId };
            mockQuery.Setup(q => q.GetById(It.IsAny<int>(), It.IsAny<int>())).
                                    ReturnsAsync(genderPreference);

            //Act
            var response = await service.Delete(userurl, request, userId);

            //Assert
            response.UserId.Should().Be(userId);
            response.GenderId.Should().Be(genderResponse.GenderId);
            response.GenderName.Should().Be(genderResponse.Description);
        }

        [Fact]
        public async void Delete_Null()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            int genderId = 1;
            string userurl = "https://localhost:7020";
            GenderPreferenceReq request = new GenderPreferenceReq { GenderId = genderId };

            GenderResponse genderResponse = new GenderResponse { GenderId = genderId, Description = "Masculino" };
            mockUserApiService.Setup(s => s.GetGenderById(It.IsAny<string>(), It.IsAny<int>())).
                                    ReturnsAsync(genderResponse);

            //Act
            var response = await service.Delete(userurl, request, userId);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Delete_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IGenderPreferenceCommand>();
            var mockQuery = new Mock<IGenderPreferenceQuery>();
            var mockUserApiService = new Mock<IUserApiService>();
            GenderPreferenceService service = new(mockQuery.Object, mockCommand.Object, mockUserApiService.Object);
            int userId = 1;
            int genderId = 1;
            string userurl = "https://localhost:7020";
            GenderPreferenceReq request = new GenderPreferenceReq { GenderId = genderId };

            mockUserApiService.Setup(q => q.GetGenderById(It.IsAny<string>(), It.IsAny<int>())).ThrowsAsync(new Exception("Error al insertar"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.Delete(userurl, request, userId));
        }
    }
}
