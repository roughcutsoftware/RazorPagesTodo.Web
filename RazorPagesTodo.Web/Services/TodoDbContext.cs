using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPagesTodo.Web.Models;

namespace RazorPagesTodo.Web.Services
{
    public class TodoDbContext : DbContext, ITodoRepository
    {
        public DbSet<Todo> Todos { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todo").HasKey(todo => todo.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Todo");
        }

        async Task ITodoRepository.Add([Bind(nameof(Todo.Description))] Todo todo)
        {
            Todos.Add(todo);
            await SaveChangesAsync();
        }

        async Task<IEnumerable<Todo>> ITodoRepository.GetAll()
        {
            return await Todos.ToListAsync();
        }

        async Task ITodoRepository.Remove(Guid id)
        {
            var todo = await Todos.FindAsync(id);
            Todos.Remove(todo);
            await SaveChangesAsync();
        }

        async Task<Todo> ITodoRepository.Find(Guid id)
        {
            return await Todos.FindAsync(id);
        }

        async Task ITodoRepository.Update(Todo todo)
        {
            Todo toUpdate = await Todos.FindAsync(todo.Id);
            if (toUpdate != null)
            {
                toUpdate.Description = todo.Description;
                await SaveChangesAsync();
            }
        }
    }
}
