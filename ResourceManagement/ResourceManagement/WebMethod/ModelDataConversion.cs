using ResourceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagement.WebMethod
{
    public class ModelDataConversion
    {
        public ResourceRequestModel DataModelToRequestModel(ResourceWithValue data)
        {
            ResourceRequestModel request = new ResourceRequestModel();
            //request.header.requestId = 1;
            //request.header.requestType = "resourceState";
            //request.header.producer.id = "1";
            //request.header.version = "1.0";
            //request.header.dateMsg = Convert.ToDateTime("20180203 10:00:00");
            //request.body.dates = Convert.ToDateTime("2018-01-30T08:00:00Z");
            //request.body.itemSet.items.id = "extid1";
            //request.body.itemSet.items.tags = data;

            return request;
        }

        public ResourceWithValue ResponseModelToDataModel(ResourceResponseModel response)
        {
            return (response.body.resourceState.tags);
        }
    }
}