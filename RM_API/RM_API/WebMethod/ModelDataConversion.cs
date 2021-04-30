using RM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RM_API.WebMethod
{
    public class ModelDataConversion
    {
        public ResourceWithValue RequestModelToDataModel(ResourceRequestModel data)
        {
            return (data.body.itemSet.items.tags);
        }

        public ResourceResponseModel DataModelToResponseModel(ResourceWithValue data,ResourceRequestModel request)
        {
            ResourceResponseModel response = new ResourceResponseModel();
            response.header.sourceRequest.requestId = request.header.requestId;
            response.header.sourceRequest.requestType = request.header.requestType;
            response.header.sourceRequest.version = request.header.version;
            response.header.messageType = request.header.requestType;
            response.header.version = request.header.version;
            response.header.dateMsg = DateTime.Now;
            response.body.resourceState.id = request.body.itemSet.items.id;
            response.body.resourceState.tags = request.body.itemSet.items.tags;

            return response;
        }
    }
}