using Cats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Cats.Services
{
    public class ServiceController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            var json = JsonConvert.SerializeObject(CatModel.GetCats());

            return json;
        }

        // POST api/<controller>
        public void Post(CatModel cat)
        {
            if (cat != null)
            {
                bool? ret = CatModel.Create(cat);
                if (ret == null)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error creating your cat."));
                }
            }
        }

        // PUT api/<controller>/5
        public void Put(CatModel cat)
        {
            if (cat != null)
            {
                bool? ret = CatModel.Update(cat);
                if (ret == null)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error updating your cat."));
                }
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            if (id > 0)
            {
                bool? ret = CatModel.Delete(id);
                if (ret == null)
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error deleting your cat."));
                }
            }
        }
    }
}