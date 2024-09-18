using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WorkManagerSystemBackend.Controllers;
using WorkManagerSystemBackend.Core.AutoMapperConfig;
using WorkManagerSystemBackend.Core.Context;
using WorkManagerSystemBackend.Core.Dtos.Space;
using WorkManagerSystemBackend.Core.Entities;
using Xunit;

namespace WorkManagerSystemBackend.Tests
{
    public class SpaceControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SpaceController _controller;

        public SpaceControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfigProfile>();
            });

            _mapper = config.CreateMapper();

            _controller = new SpaceController(_context, _mapper);
        }

        [Fact]
        public async Task CreateSpaceTest()
        {
            // Arrange
            var dto = new SpaceCreateDto
            {
                Name = "New Space",
                Description = "Description of the new space",
                UsersId = 1
            };

            // Act
            var result = await _controller.CreateSpace(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var newSpaceId = Assert.IsType<long>(okResult.Value);

            // Provjerite da li je Space dodan u bazu
            var space = await _context.Spaces.FindAsync(newSpaceId);
            Assert.NotNull(space);
            Assert.Equal("New Space", space.Name);
            Assert.Equal("Description of the new space", space.Description);
            Assert.Equal(1, space.UsersId);
        }
    }
}
