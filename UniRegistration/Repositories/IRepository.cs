using Models;
using System.Collections.Generic;

namespace Repositories
{
    public interface IRepository<T> where T : class
    {

        T Find(int id);
        
        int Insert(T user);
        int Update(T user);
        List<T> FetchAll();





    }
}
