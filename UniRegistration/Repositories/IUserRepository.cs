using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Threading.Tasks;
using Repositories;

namespace Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        User Find(string email);
        
    }
}
