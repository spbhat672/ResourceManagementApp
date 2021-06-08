using AutoMapper;
using RM_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RM_API.WebMethod
{
    public static class ModelDataConversion
    {
        public static ResourceWithValue RequestModelToDataModel(ResourceRequestModel data)
        {
            return (data.body.itemSet.items.tags);
        }

        public static string DataModelToResponseModel(ResourceRequestModel request)
        {
            string jsonRsponse = @"{  
                                    'header': {
                                                'sourceRequest': {
                                                                   'requestId': "+ request.header.requestId + @"                                                                
                                                                   'requestType': " + request.header.requestType + @"  
	                                                               'version': " + request.header.version + @"  
	                                                             },
	                                            'messageType':" + request.header.requestType + @"  
	                                            'version':" + request.header.version + @"  
	                                            'dateMsg':	" + request.header.dateMsg + @"  
	                                     },
	                                'body':{
                                                'resourceState': {
                                                                    'id':" + request.body.itemSet.items.id + @" 
			                                                        'updateDataDate':" + DateTime.Now + @"
						                                             'tags': {
                                                                               'Id': " + request.body.itemSet.items.tags.Id+ @" , 
									                                            'TypeId': " + request.body.itemSet.items.tags.TypeId + @", 
									                                            'Type': " + request.body.itemSet.items.tags.Type + @", 
					 				                                            'StatusId': " + request.body.itemSet.items.tags.StatusId + @", 
									                                            'Status': " + request.body.itemSet.items.tags.Status + @", 
									                                            'LocationId': " + request.body.itemSet.items.tags.LocationId + @",
									                                            'LocationValue': {
                                                                                                    'Id': " + request.body.itemSet.items.tags.LocationValue.Id + @" ,
														                                            'X': " + request.body.itemSet.items.tags.LocationValue.X + @" ,
														                                            'Y': " + request.body.itemSet.items.tags.LocationValue.Y + @" ,
														                                            'Z': " + request.body.itemSet.items.tags.LocationValue.Z + @" ,
														                                            'Rotation': " + request.body.itemSet.items.tags.LocationValue.Rotation + @"
                                                                                                    }, 
									                                            'Name': " + request.body.itemSet.items.tags.Name + @"
                                                                             }
                                                                  }
                                            }
                                    }";


            jsonRsponse = jsonRsponse.Replace("\n", "").ToString();
            jsonRsponse = jsonRsponse.Replace("\t", "").ToString();
            jsonRsponse = jsonRsponse.Replace("\r", "").ToString();
            return jsonRsponse;
        }

        public static string DataModelToGetResponseModel(ResourceGetRequestModel request, List<ResourceWithValue> resList)
        {
            string jsonStr1 = @"{  
                                    'header': {
                                                'sourceRequest': {
                                                                   'requestId': " + request.header.requestId + @",                                                                
                                                                   'requestType': " + request.header.requestType + @",  
                                                                   'version': 1.0  
	                                                             },
	                                            'messageType':{
                                                                    'messageType':" + request.header.requestType + @", 
                                                                    'version': 1.0  
	                                                             },
                                                'version': 1.0
	                                            'dateMsg':	" + request.header.dateMsg + @",                                                
	                                     },
	                                'body':{
                                                'resourceState': {
                                                                    'id':" + request.body.itemSet.items[0].id + @" 
			                                                        'updateDataDate':" + DateTime.Now + @"
						                                             'tags': {";

            string jsonStr2 = "";
            foreach (var x in resList)
            {
                jsonStr2 += @"{
                              'Id': " + x.Id + @" , 
							  'TypeId': " + x.TypeId + @", 
							  'Type': " + x.Type + @", 
					 		  'StatusId': " + x.StatusId + @", 
							  'Status': " + x.Status + @", 
							  'LocationId': " + x.LocationId + @",
							  'LocationValue': {
                                                'Id': " + x.LocationValue.Id + @" ,
												'X': " + x.LocationValue.X + @" ,
												'Y': " + x.LocationValue.Y + @" ,
												'Z': " + x.LocationValue.Z + @" ,
												'Rotation': " + x.LocationValue.Rotation + @"
                                                }, 
							  'Name': " + x.Name + "" +
                              "},";
            }
            string jsonStr3 = @"}
                                        }
                                    }
                                }";

            string jsonString = jsonStr1 + jsonStr2 + jsonStr3;
            jsonString = jsonString.Replace("\n", "").ToString();
            jsonString = jsonString.Replace("\t", "").ToString();
            jsonString = jsonString.Replace("\r", "").ToString();
            return jsonString;
        }
    }
}