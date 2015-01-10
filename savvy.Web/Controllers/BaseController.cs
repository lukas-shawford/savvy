using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using savvy.Data;

namespace savvy.Web.Controllers
{
    public abstract class BaseController : ApiController
    {
        private ISavvyRepository _repo;

        protected BaseController(ISavvyRepository repo)
        {
            _repo = repo;
        }

        protected ISavvyRepository Repository
        {
            get { return _repo; }
        }
    }
}
