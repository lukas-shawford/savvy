using System.Web.Http;
using savvy.Data;
using savvy.Web.Models.Factories;

namespace savvy.Web.Controllers
{
    public abstract class BaseController : ApiController
    {
        private ISavvyRepository _repo;
        private ModelFactory _modelFactory;

        protected BaseController(ISavvyRepository repo)
        {
            _repo = repo;
        }

        protected ISavvyRepository Repository
        {
            get { return _repo; }
        }

        protected ModelFactory ModelFactory
        {
            get { return _modelFactory ?? (_modelFactory = new ModelFactory()); }
        }
    }
}
