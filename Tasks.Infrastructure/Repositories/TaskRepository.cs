using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Entities;
using Tasks.Domain.Interfaces.Repositories;
using Tasks.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task AddAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {

            var existingTask = await _context.TaskItems.FindAsync(task.Id);

            if (existingTask == null)
            {
                throw new KeyNotFoundException("La tarea con el id especificado no se encuentra.");
            }

            _context.Entry(existingTask).CurrentValues.SetValues(task);

            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
