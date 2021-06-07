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

        #region Postaman Api methods
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("ResourceApi")]
        public HttpResponseMessage ResourceApi([FromUri]string model)
        {
            try
            {
                JObject jObj = JObject.Parse(model);
                ResourceGetRequestModel myModel = new ResourceGetRequestModel();
                myModel = jObj.ToObject<ResourceGetRequestModel>();
                //if (Convert.ToInt32(myModel.body.itemSet.items.tags.tagId) == 0)
                //{
                //    // POST request

                //    var resource = ModelDataConversion.RequestModelToDataModel(myModel);
                //    ResourceRepository.AddResourceInfo(resource);

                //    var response = ModelDataConversion.DataModelToResponseModel(myModel);
                //    return Request.CreateResponse(System.Net.HttpStatusCode.OK, model);
                //}
                //else
                //{
                // GET request

                //var resource = ModelDataConversion.RequestModelToDataModel(myModel);
                List<string> idArray = new List<string>();
                foreach(var x in myModel.body.itemSet.items[0].tags)
                {
                    if (x.tagId == "")
                        continue;
                    else
                    {
                        idArray.Add(x.tagId);
                    }
                }
                
                var resourceList = new List<ResourceWithValue>();
                    resourceList = ResourceRepository.GetResourceInfoFor3ds(idArray);

                var response = ModelDataConversion.DataModelToGetResponseModel(myModel, resourceList);
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, response);
                //}
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Server - Error Fetching resource Information");
            }
        }             

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Put")]
        public HttpResponseMessage Put([FromUri]string model)
        {
            try
            {
                JObject jObj = JObject.Parse(model);
                ResourceRequestModel myModel = new ResourceRequestModel();
                myModel = jObj.ToObject<ResourceRequestModel>();

                var resource = ModelDataConversion.RequestModelToDataModel(myModel);                
                ResourceRepository.UpdateResourceInfo(resource);

                var response = ModelDataConversion.DataModelToResponseModel(myModel);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, ex);
            }
        }

        

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Delete")]
        public HttpResponseMessage Delete([FromUri]string idStr)
        {
            try
            {
                long id = Convert.ToInt64(idStr);
                ResourceRepository.DeleteResourceInfo(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, 202);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, ex);
            }
        }

        #endregion

        #region MVC_Clinet_APIs

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetResource")]
        public HttpResponseMessage GetResource([FromUri] long? id)
        {
            try
            {
                List<ResourceWithValue> resourceList = ResourceRepository.GetResourceInfo(id);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, resourceList);
                return null;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, "Server - Error Fetching resource Information");
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/PostResource")]
        public HttpResponseMessage PostResource([FromBody] ResourceWithValue model)
        {
            try
            {
                ResourceRepository.AddResourceInfo(model);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, 202);
            }
            catch (Exception ex)

            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/PutResource")]
        public HttpResponseMessage PutResource([FromBody] ResourceWithValue model)
        {
            try
            {
                ResourceRepository.UpdateResourceInfo(model);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, 202);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.NotFound, ex);
            }
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/DeleteResource")]
        public HttpResponseMessage DeleteResource([FromUri] long id)
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

        #endregion
    }
}