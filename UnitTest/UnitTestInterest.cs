using Moq;
using FluentAssertions;
using Application.UseCases;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace UnitTest
{
    public class UnitTestInterest
    {
        [Fact]
        public async void GetAll_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();            
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            List<InterestCategory> interestCategories = new List<InterestCategory>();
            InterestCategory category = new InterestCategory { InterestCategoryId = 1, Description = "InteresCategory", Interests = null };
            interestCategories.Add(category);
            mockCategoryQuery.Setup(q => q.GetAll()).ReturnsAsync(interestCategories);

            List<Interest> interestsList = new List<Interest>();
            Interest interest = new Interest { InterestId = 1, Description = "InteresCategory", InterestCategoryId = 0 , Preferences = null};
            interestsList.Add(interest);
            mockQuery.Setup(q => q.GetAll()).ReturnsAsync(interestsList);            

            //Act
            var lista = await service.GetAll();

            //Assert
            Assert.True(lista.Any());
        }

        [Fact]
        public async void GetAll_Empty()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            //Act
            var lista = await service.GetAll();

            //Assert
            Assert.False(lista.Any());
        }

        [Fact]
        public async void GetAll_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            mockCategoryQuery.Setup(q => q.GetAll()).ThrowsAsync(new Exception("Error al consultar"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetAll());
        }

        [Fact]
        public async void GetByIdCategory_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync( new InterestCategory { InterestCategoryId = categoryId, Description = categoryDescription, Interests = null } );

            List<Interest> interestsList = new List<Interest> 
                { new Interest { InterestId = 1, Description = "InteresCategory", InterestCategoryId = 0 } };
            mockQuery.Setup(q => q.GetAllByCategory(It.IsAny<int>())).ReturnsAsync(interestsList);

            //Act
            var response = await service.GetByIdCategory(1);

            //Assert
            response.Id.Should().Be(categoryId);
            response.Description.Should().Be(categoryDescription);
            response.Interes.Should().NotBeEmpty();
        }

        [Fact]
        public async void GetByIdCategory_Empty()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new InterestCategory { InterestCategoryId = categoryId, Description = categoryDescription, Interests = null });

            //Act
            var response = await service.GetByIdCategory(1);

            //Assert
            response.Id.Should().Be(categoryId);
            response.Description.Should().Be(categoryDescription);
            response.Interes.Should().BeEmpty();
        }

        [Fact]
        public async void GetByIdCategory_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            //Act
            var response = await service.GetByIdCategory(1);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void GetByIdCategory_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            mockCategoryQuery.Setup(q => q.GetById(1)).ThrowsAsync(new Exception("Error al consultar"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetByIdCategory(1));
        }

        [Fact]
        public async void GetAllByCategory_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new InterestCategory { InterestCategoryId = categoryId, Description = categoryDescription, Interests = new List<Interest>
                { new Interest { InterestId = 1, Description = "InteresCategory", InterestCategoryId = 0 } } });

            List<Interest> interestsList = new List<Interest>
                { new Interest { InterestId = 1, Description = "Interes", InterestCategory = new InterestCategory { InterestCategoryId = 1 , Description = "InterestCategory" }} };
            mockQuery.Setup(q => q.GetAllByCategory(It.IsAny<int>())).ReturnsAsync(interestsList);

            //Act
            var response = await service.GetAllByCategory(categoryId);

            //Assert
            Assert.True(response.Any());
        }

        [Fact]
        public async void GetAllByCategory_Empty()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new InterestCategory
                {
                    InterestCategoryId = categoryId,
                    Description = categoryDescription,
                    Interests = new List<Interest>
                { new Interest { InterestId = 1, Description = "InteresCategory", InterestCategoryId = 0 } }
                });

            //Act
            var response = await service.GetAllByCategory(categoryId);

            //Assert
            Assert.False(response.Any());
        }

        [Fact]
        public async void GetAllByCategory_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;

            //Act
            var response = await service.GetAllByCategory(categoryId);

            //Assert
            Assert.False(response.Any());
        }

        [Fact]
        public async void GetAllByCategory_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            mockCategoryQuery.Setup(q => q.GetById(1)).ThrowsAsync(new Exception("Error al consultar"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetAllByCategory(1));
        }

        [Fact]
        public async void GetById_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int interestId = 1;
            string interestDescription = "Interest";
            mockQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new Interest { InterestId = interestId, Description = interestDescription, 
                                InterestCategory = new InterestCategory { InterestCategoryId = 1 , Description = "InterestCategory" } });

            //Act
            var response = await service.GetById(1);

            //Assert
            response.Id.Should().Be(interestId);
            response.Description.Should().Be(interestDescription);
            response.InterestCategory.Should().NotBeNull();
        }


        [Fact]
        public async void GetById_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            //Act
            var response = await service.GetById(1);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void GetById_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            mockQuery.Setup(q => q.GetById(1)).ThrowsAsync(new Exception("Error al consultar"));

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.GetById(1));
        }

        [Fact]
        public async void Insert_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new InterestCategory { InterestCategoryId = categoryId, Description = categoryDescription, Interests = null });

            string interestDescription = "Interest";
            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };

            //Act
            var response =  await service.Insert(request);

            //Assert
            response.Id.Should().Be(0);
            response.Description.Should().Be(request.Description);
            response.InterestCategory.Should().NotBeNull();
        }

        [Fact]
        public async void Insert_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;
            string interestDescription = "Interest";
            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };

            //Act
            var response = await service.Insert(request);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Insert_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            mockCategoryQuery.Setup(q => q.GetById(1)).ThrowsAsync(new Exception("Error al consultar"));
            string interestDescription = "Interest";
            int categoryId = 1;         
            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.Insert(request));
        }

        [Fact]
        public async void Update_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            int categoryId = 1;
            string interestDescription = "Interest";
            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };
            //Act
            var response = await service.Update(request, 1);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Update_InterestNull()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            string interestDescription = "Interest";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new InterestCategory { InterestCategoryId = categoryId, Description = categoryDescription, Interests = null });

            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };
            //Act
            var response = await service.Update(request, 1);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Update_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new InterestCategory { InterestCategoryId = categoryId, Description = categoryDescription, Interests = null });

            int interestId = 1;
            string interestDescription = "Interest";
            mockQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new Interest
                {
                    InterestId = interestId,
                    Description = interestDescription,
                    InterestCategory = new InterestCategory { InterestCategoryId = 1, Description = "InterestCategory" }
                });
            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };
            //Act
            var response = await service.Update(request, 1);

            //Assert
            response.Id.Should().Be(interestId);
            response.Description.Should().Be(interestDescription);
            response.InterestCategory.Should().NotBeNull();
        }

        [Fact]
        public async void Update_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            mockCategoryQuery.Setup(q => q.GetById(1)).ThrowsAsync(new Exception("Error al consultar"));
            string interestDescription = "Interest";
            int categoryId = 1;
            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };

            //Act
            //Assert
            await Assert.ThrowsAsync<Exception>(() => service.Update(request, categoryId));
        }

        [Fact]
        public async void Delete_Null()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            //Act
            var response = await service.Delete(1);

            //Assert
            Assert.Null(response);
        }

        [Fact]
        public async void Delete_Ok()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);

            int categoryId = 1;
            string categoryDescription = "InterestCategory";
            mockCategoryQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new InterestCategory { InterestCategoryId = categoryId, Description = categoryDescription, Interests = null });

            int interestId = 1;
            string interestDescription = "Interest";
            mockQuery.Setup(q => q.GetById(It.IsAny<int>())).
                ReturnsAsync(new Interest
                {
                    InterestId = interestId,
                    Description = interestDescription,
                    InterestCategory = new InterestCategory { InterestCategoryId = 1, Description = "InterestCategory" }
                });
            InterestReq request = new InterestReq { Description = interestDescription, InterestCategoryId = categoryId };
            //Act
            var response = await service.Delete(interestId);

            //Assert
            response.Id.Should().Be(interestId);
            response.Description.Should().Be(interestDescription);
            response.InterestCategory.Should().NotBeNull();
        }

        [Fact]
        public async void Delete_ThrowsException()
        {
            //Arrange
            var mockCommand = new Mock<IInterestCommand>();
            var mockQuery = new Mock<IInterestQuery>();
            var mockCategoryQuery = new Mock<IInterestCategoryQuery>();
            InterestService service = new(mockQuery.Object, mockCommand.Object, mockCategoryQuery.Object);
            mockQuery.Setup(q => q.GetById(1)).ThrowsAsync(new Exception("Error al consultar"));
            string interestDescription = "Interest";
            int interestId = 1;

            //Act
            var response = await service.Delete(interestId);

            //Assert
            Assert.Null(response);
        }
    }
}