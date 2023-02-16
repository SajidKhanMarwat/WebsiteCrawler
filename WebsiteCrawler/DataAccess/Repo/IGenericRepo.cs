using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public interface IGenericRepo<TModel> where TModel : class
    {
        //IEnumerable<TModel> GetAll();
        //IEnumerable<TModel> Get(int id);

        Task<IEnumerable<TModel>> DoAuthentication();

    }
}
