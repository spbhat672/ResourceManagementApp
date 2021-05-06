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
            //ResourceResponseModel response = new ResourceResponseModel();
            //response.header.sourceRequest.requestId = ;
            //response.header.sourceRequest.requestType = req.header.requestType;
            //response.header.sourceRequest.version = req.header.version;
            //response.header.messageType = req.header.requestType;
            //response.header.version = req.header.version;
            //response.header.dateMsg = DateTime.Now;
            //response.body.resourceState.id = req.body.itemSet.items.id;
            //response.body.resourceState.tags = req.body.itemSet.items.tags;

            return jsonRsponse;
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<ResourceRequestModel, ResourceResponseModel>()
            //       .ForMember(destination => destination.header.sourceRequest.requestId, map => map.MapFrom(x => x.header.requestId))
            //       .ForMember(destination => destination.header.sourceRequest.requestType, map => map.MapFrom(x => x.header.requestType))
            //       .ForMember(destination => destination.header.sourceRequest.version, map => map.MapFrom(x => x.header.version))
            //       .ForMember(destination => destination.header.messageType, map => map.MapFrom(x => x.header.requestType))
            //       .ForMember(destination => destination.header.version, map => map.MapFrom(x => x.header.version))
            //       .ForMember(destination => destination.header.dateMsg, map => map.AddTransform(x => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)))
            //       .ForMember(destination => destination.body.resourceState.id, map => map.MapFrom(x => x.body.itemSet.items.id))
            //       .ForMember(destination => destination.body.resourceState.tags, map => map.MapFrom(x => x.body.itemSet.items.tags));
            //  });

            //IMapper iMapper = config.CreateMapper();
            //ResourceResponseModel response = iMapper.Map<ResourceResponseModel>(request);
            //return response;
        }

        public static string DataModelToGetResponseModel(ResourceGetRequestModel request, List<ResourceWithValue> resList)
        {
            string jsonStr1 = @"{  
                                    'header': {
                                                'sourceRequest': {
                                                                   'requestId': " + request.header.requestId + @"                                                                
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
						                                             'tags': {";

            string jsonStr2 = "";
            foreach(var x in resList)
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
							  'Name': " + x.Name +"" +
                              "},";
            }
            string jsonStr3 =              @"}
                                        }
                                    }
                                }";

            string jsonString = jsonStr1 + jsonStr2 + jsonStr3;
            return jsonString;
        }
    }
}