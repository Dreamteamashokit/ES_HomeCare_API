﻿using Dapper;
using ES_HomeCare_API.Model.Location;
using ES_HomeCare_API.WebAPI.Data.IData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data
{
    public class LocationData : ILocationData
    {
        private IConfiguration configuration;
        public LocationData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> AddLocation(LocationModel _model)
        {
            ServiceResponse<string> resObj = new ServiceResponse<string>();
            try
            {
                using (IDbConnection conn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "INSERT INTO tblLocation (CompanyId,LocationName,BillingName,Contact,Email,Address,Phone,Fax,City,State,ZipCode,IsBilling,Description,TaxId,LegacyId,NPI,ISA06,ModifiedBy,ModifiedOn,CreatedBy,CreatedOn) VALUES (@CompanyId,@LocationName,@BillingName,@Contact,@Email,@Address,@Phone,@Fax,@City,@State,@ZipCode,@IsBilling,@Description,@TaxId,@LegacyId,@NPI,@ISA06,@ModifiedBy,@ModifiedOn,@CreatedBy,@CreatedOn) ;";

                    int rowsAffected = await conn.ExecuteAsync(sqlQuery, _model);
                    if (rowsAffected > 0)
                    {
                        resObj.Result = true;
                        resObj.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        resObj.Data = null;
                        resObj.Message = "Failed new creation.";
                    }
                }
            }
            catch (Exception ex)
            {
                resObj.Message = ex.Message;
                return resObj;
            }
            finally
            {

            }
            return resObj;
        
        }




        public async Task<ServiceResponse<IEnumerable<LocationModel>>> GetLocationList()
        {
            ServiceResponse<IEnumerable<LocationModel>> obj = new ServiceResponse<IEnumerable<LocationModel>>();
            using (var conn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlQuery = "select * from tblLocation;";

                IEnumerable<LocationModel> resObj = (await conn.QueryAsync<LocationModel>(sqlQuery));

                obj.Data = resObj;
                obj.Result = resObj.Any() ? true : false;
                obj.Message = resObj.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<LocationModel>>> SearchLocation(string search)
        {
            ServiceResponse<IEnumerable<LocationModel>> obj = new ServiceResponse<IEnumerable<LocationModel>>();
            using (var conn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlQuery = "Select x.* from tblLocation x Where x.LocationName Like '%'+@search+'%' OR x.Address Like '%'+@search+'%' OR x.City Like '%'+@search+'%' OR x.State Like '%'+@search+'%' OR x.Description Like '%'+@search+'%' ;";

                IEnumerable<LocationModel> resObj = (await conn.QueryAsync<LocationModel>(sqlQuery, new { @search = search }));

                obj.Data = resObj;
                obj.Result = resObj.Any() ? true : false;
                obj.Message = resObj.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }
        








    }
}