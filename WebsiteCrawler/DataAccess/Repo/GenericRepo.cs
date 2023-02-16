using DataAccess.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repo
{
    public class GenericRepo<TModel> : IGenericRepo<TModel> where TModel : class
    {
        private readonly CrawlerDBContext _crawlerDBContext;

        public GenericRepo(CrawlerDBContext crawlerDBContext)
        {
            _crawlerDBContext = crawlerDBContext;
        }

        public Task<IEnumerable<TModel>> DoAuthentication()
        {
            throw new NotImplementedException();
        }
    }
}
