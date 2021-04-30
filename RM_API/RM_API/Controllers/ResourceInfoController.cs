using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RM_API.Models;
using RM_API.WebMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RM_API.Controllers
{
    [System.Web.Http.RoutePrefix("ResourceInfo")]
    public class ResourceInfoController : ApiController
    {
        [System.Web.Http.HttpGet]        
        [System.Web.Http.Route("api/Get")]
        public HttpResponseMessage Get([FromUri]long? id)
        {
            try
            {
                var resourceList = new List<ResourceWithValue>();
                resourceList = ResourceRepository.GetResourceInfo(id);                
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resourceList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Server - Error Fetching resource Information");
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Post")]
        public HttpResponseMessage Post([FromUri]string model)
        {
            try
            {
                JObject jObj = JObject.Parse(model);
                ResourceRequestModel myModel = new ResourceRequestModel();
                myModel = jObj.ToObject<ResourceRequestModel>();
                //var obj = JsonConvert.DeserializeObject<ResourceRequestModel>(model); //even works fine....

                var resource = new ModelDataConversion().RequestModelToDataModel(myModel);
                ResourceRepository.AddResourceInfo(resource);

                var response = new ModelDataConversion().DataModelToResponseModel(resource, myModel);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Put")]
        public HttpResponseMessage Put([FromUri] string model)
        {
            try
            {
                JObject jObj = JObject.Parse(model);
                ResourceRequestModel myModel = new ResourceRequestModel();
                myModel = jObj.ToObject<ResourceRequestModel>();

                var resource = new ModelDataConversion().RequestModelToDataModel(myModel);
                var response = new ModelDataConversion().DataModelToResponseModel(resource, myModel);
                ResourceRepository.UpdateResourceInfo(resource);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, 202);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, ex);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Delete")]
        public HttpResponseMessage Delete(long id)
        {
            try
            {
                ResourceRepository.DeleteResourceInfo(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, 202);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, ex);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetTypeList")]
        public HttpResponseMessage GetTypeList()
        {
            try
            {
                var typeList = new List<Models.Type>();
                typeList = ResourceRepository.GetTypeInfo();
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, typeList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Server - Error Fetching type data");
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetStatusList")]
        public HttpResponseMessage GetStatusList()
        {
            try
            {
                var statusList = new List<Models.Status>();
                statusList = ResourceRepository.GetStatusInfo();
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, statusList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Server - Error Fetching status data");
            }
        }
    }
}