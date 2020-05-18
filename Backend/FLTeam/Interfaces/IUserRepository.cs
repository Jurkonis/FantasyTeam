using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLTeam.Model
{
	public interface IUserRepository
	{
		Task<UserLogin> Authenticate(string username, string password);
	}
}
