using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Tasks.Domain.Entities;
using Tasks.Domain.Interfaces;
using Tasks.Application.Services;
using Tasks.Domain.Interfaces.Repositories;

namespace Tasks.Test
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockRepo;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _mockRepo = new Mock<ITaskRepository>();
            _service = new TaskService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetTasksAsync_ReturnsTasks()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = 1,
                    Title = "Test Task",
                    Description = "Test Description",
                    AssignedTo = "User",
                    DueDate = DateTime.Now,
                    Completed = false
                }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(tasks);

            var result = await _service.GetTasksAsync();

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateTaskAsync_AddsTask()
        {
            var newTask = new TaskItem
            {
                Id = 1,
                Title = "New Task",
                Description = "Desc",
                AssignedTo = "User",
                DueDate = DateTime.Now,
                Completed = false
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<TaskItem>())).Returns(Task.CompletedTask);

            await _service.CreateTaskAsync(newTask);

            _mockRepo.Verify(r => r.AddAsync(It.Is<TaskItem>(t => t.Title == "New Task")), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_TaskExists_UpdatesTask()
        {
            var existingTask = new TaskItem
            {
                Id = 1,
                Title = "Old Title",
                Description = "Desc",
                AssignedTo = "User",
                DueDate = DateTime.Now,
                Completed = false
            };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingTask);
            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<TaskItem>())).Returns(Task.CompletedTask);

            var updatedTask = new TaskItem
            {
                Id = 1,
                Title = "Updated Title",
                Description = "Desc",
                AssignedTo = "User",
                DueDate = DateTime.Now,
                Completed = true
            };

            await _service.UpdateTaskAsync(updatedTask);

            _mockRepo.Verify(r => r.UpdateAsync(It.Is<TaskItem>(t => t.Title == "Updated Title" && t.Completed)), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_TaskDoesNotExist_ThrowsKeyNotFoundException()
        {
           
            _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
         .ThrowsAsync(new KeyNotFoundException("La tarea con el id especificado no se encuentra."));


            var updatedTask = new TaskItem
            {
                Id = 1,
                Title = "Updated Title",
                Description = "Desc",
                AssignedTo = "User",
                DueDate = DateTime.Now,
                Completed = true
            };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateTaskAsync(updatedTask));
        }

        [Fact]
        public async Task DeleteTaskAsync_DeletesTask()
        {
            _mockRepo.Setup(r => r.DeleteAsync(1)).Returns(Task.CompletedTask);

            await _service.DeleteTaskAsync(1);

            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }
}
