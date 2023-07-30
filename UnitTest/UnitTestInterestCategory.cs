using Xunit;
using Moq;
using FluentAssertions;

using Application.UseCases;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace UnitTest
{
    public class UnitTestInterestCategory
    {
        [Fact]
        public async void GetAll()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);

            //Act
            var lista = await service.GetAll();

            //Assert
            Assert.True(!lista.Any());
        }

        [Fact]
        public async void Insert_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            var request = new InterestCategoryReq { Description = "CategoryTest" };
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);

            //Act
            var response =  await service.Insert(request);

            //Assert
            response.Id.Should().Be(0);
            response.Description.Should().Be(request.Description);
        }

        [Fact]
        public async void Insert_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            var request = new InterestCategoryReq { Description = "CategoryTest" };
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);
            mockCommand.Setup(q => q.Insert(It.IsAny <InterestCategory>())).ThrowsAsync(new Exception("Error al insertar"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.Insert(request));
        }

        [Fact]
        public async void Update_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            var request = new InterestCategoryReq { Description = "CategoryTest" };
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);

            //Act
            var response = await service.Update(request, 1);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Update_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            var request = new InterestCategoryReq { Description = "CategoryTest" };
            int interestCategoryId = 1;
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);

            mockQuery.Setup(q => q.GetById(It.IsAny<int>())).
                                    ReturnsAsync(new InterestCategory 
                                    { InterestCategoryId = interestCategoryId, Description = request.Description, Interests = null });

            //Act
            var response = await service.Update(request, 1);

            //Assert
            response.Id.Should().Be(interestCategoryId);
            response.Description.Should().Be(request.Description);
        }

        [Fact]
        public async void Update_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            var request = new InterestCategoryReq { Description = "CategoryTest" };
            int interestCategoryId = 1;
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);
            mockQuery.Setup(q => q.GetById(It.IsAny<int>())).
                                    ReturnsAsync(new InterestCategory
                                    { InterestCategoryId = interestCategoryId, Description = request.Description, Interests = null });
            mockCommand.Setup(q => q.Update(It.IsAny<InterestCategory>())).ThrowsAsync(new Exception("Error al actualizar"));            

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.Update(request, 1));
        }

        [Fact]
        public async void Delete_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            int interestCategoryId = 1;
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);

            //Act
            var response = await service.Delete(interestCategoryId);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Delete_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCategoryCommand>();
            var mockQuery = new Mock<IInterestCategoryQuery>();
            int interestCategoryId = 1;
            string interesCategoryDescription = "InterestCategory";
            InterestCategoryService service = new(mockQuery.Object, mockCommand.Object);

            mockQuery.Setup(q => q.GetById(It.IsAny<int>())).
                                    ReturnsAsync(new InterestCategory
                                    { InterestCategoryId = interestCategoryId, Description = interesCategoryDescription, Interests = null });

            //Act
            var response = await service.Delete(interestCategoryId);

            //Assert
            response.Id.Should().Be(interestCategoryId);
            response.Description.Should().Be(interesCategoryDescription);
        }
    }
}