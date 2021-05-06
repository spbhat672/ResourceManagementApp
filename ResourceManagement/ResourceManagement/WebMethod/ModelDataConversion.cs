using ResourceManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagement.WebMethod
{
    public static class ModelDataConversion
    {
        public static string DataModelToRequestModel(ResourceWithValue data)
        {
            string request = @"{  
								'header': {
											'requestId' : 1, 					
											'requestType' : 'resourceState',		
											'producer' : { 'id':'1'}, 				
											'version' :'1.0',	
											'dateMsg': " + DateTime.Now + @"
											},
								'body': {
											'dates': " + DateTime.Now + @", 
											'itemSet': {
												'items': {
													'id': 'extId1',
															'tags': {
																		'Id': " + data.Id + @", 
																		'TypeId': " + data.TypeId + @", 
																		'Type': " + data.Type + @", 
					 													'StatusId': " + data.StatusId + @", 
																		'Status': " + data.Status + @", 
																		'LocationId': " + data.LocationId + @", 
																        'LocationValue': {
															                                'Id': " + data.LocationValue.Id + @",
																							'X': " + data.LocationValue.X + @",
																							'Y': " + data.LocationValue.Y + @",
																							'Z': " + data.LocationValue.Z + @",
																							'Rotation': " + data.LocationValue.Rotation + @",
																					}, 
																'Name': " + data.Name + @",
															}
												    }
                                              }
										}
									}";

            return request;
        }

		public static string DataModelToGetRequestModel(long? id)
        {
			string request = @"{  
								'header': {
											'requestId' : 1, 					
											'requestType' : 'resourceState',		
											'producer' : { 'id':'1'}, 				
											'version' :'1.0',	
											'dateMsg': " + DateTime.Now + @"
											},
								'body': {
											'dates': " + DateTime.Now + @", 
											'itemSet': {
												'items': {
                                                    'Id': " + id + @"
												}
											}
										}
									}";
			return request;
		}

        public static ResourceWithValue ResponseModelToDataModel(ResourceResponseModel response)
        {
            return (response.body.resourceState.tags);
        }
    }
}