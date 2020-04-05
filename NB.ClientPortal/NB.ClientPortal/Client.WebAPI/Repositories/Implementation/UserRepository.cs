﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Client.WebAPI.Repositories
{
	public class UserRepository : IUserRepository
	{
		protected ClientDBContext context;
		public UserRepository(ClientDBContext _context)
		{
			context = _context;
		}
		public async Task<User> GetById(int Id)
		{
			return await this.context.Set<User>().FindAsync(Id);
		}
		public async Task<User> GetUserByUserName(string userName, string password)
		{

			return await this.context.Set<User>().Where(x => x.Username == userName && x.Password == password).FirstOrDefaultAsync();
		}
		public async Task<IList<User>> GetAll()
		{
			return await this.context.Set<User>().ToListAsync();
		}

		public async Task<User> Add(User user)
		{
			this.context.Set<User>().Add(user);
			await this.context.SaveChangesAsync();
			return user;
		}

		public async Task<User> Update(User user)
		{

			//var item = await this.GetById(user.Id);
			this.context.Set<User>().Update(user);
			await this.context.SaveChangesAsync();
			return user;
		}

		public async Task Delete(int Id)
		{
			var user = await context.Set<User>().FindAsync(Id);
			context.Set<User>().Remove(user);
			await context.SaveChangesAsync();
		}
	}
}