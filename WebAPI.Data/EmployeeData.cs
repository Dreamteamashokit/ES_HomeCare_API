using Dapper;
using ES_HomeCare_API.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Data.IData;

namespace WebAPI_SAMPLE.WebAPI.Data
{
    public class EmployeeData : IEmployeeData
    {
        private IConfiguration configuration;
        public EmployeeData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> savenewemployee(Employee client)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_SAVE_Employee", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", client.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", client.LastName);
                    cmd.Parameters.AddWithValue("@CellPhone", client.CellPhone);
                    cmd.Parameters.AddWithValue("@HomePhone", client.HomePhone);
                    cmd.Parameters.AddWithValue("@Email", client.Email);
                    cmd.Parameters.AddWithValue("@DateOfHire", client.DateOfHire);
                    cmd.Parameters.AddWithValue("@DateOfFirstCase", client.DateOfFirstCase);
                    cmd.Parameters.AddWithValue("@DOB", client.DOB);
                    cmd.Parameters.AddWithValue("@SSN", client.SSN);
                    cmd.Parameters.AddWithValue("@ExtEmpId", client.ExtEmpId);
                    cmd.Parameters.AddWithValue("@Gender", client.Gender);
                    cmd.Parameters.AddWithValue("@HRSupervisor", client.HrSupervisor);
                    cmd.Parameters.AddWithValue("@Enthnicity", client.Enthnicity);
                    cmd.Parameters.AddWithValue("@MaritalStatus", client.MaritalStatus);
                    cmd.Parameters.AddWithValue("@City", client.City);
                    cmd.Parameters.AddWithValue("@Country", client.Country);
                    cmd.Parameters.AddWithValue("@CState", client.CState);
                    cmd.Parameters.AddWithValue("@ZipCode", client.ZipCode);
                    cmd.Parameters.AddWithValue("@EmgContact", client.EmgContact);
                    cmd.Parameters.AddWithValue("@EmgPhone", client.EmgPhone);
                    cmd.Parameters.AddWithValue("@IsActive", client.IsActive);

                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (value > 0)
                    {
                        sres.Result = true;
                        sres.Data = "New Employee Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new employee creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

            }
            return sres;
        }

        public async Task<ServiceResponse<List<Employee>>> GetEmployeesList()
        {
            ServiceResponse<List<Employee>> obj = new ServiceResponse<List<Employee>>();
            List<Employee> emp = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_EMPLOYEES", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            emp.Add(new Employee
                            {
                                EmpID = Convert.ToInt32(table.Rows[i]["EmpId"].ToString()),
                                EmpName = table.Rows[i]["EmpName"].ToString(),
                                CellPhone = table.Rows[i]["CellPhone"].ToString(),
                                HasEmail = table.Rows[i]["HasEmail"].ToString(),
                                HasDOB = table.Rows[i]["HasDOB"].ToString(),
                                IsActive = Convert.ToInt32(table.Rows[i]["IsActive"].ToString()),
                                EnteredDate = table.Rows[i]["EnteredDate"].ToString(),
                                CState = table.Rows[i]["CState"].ToString()
                            });
                        }
                        obj.Result = true;
                    }
                    obj.Data = emp;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
            finally
            {

            }
        }

        public async Task<ServiceResponse<string>> DeleteEmployee(int EmpId)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_DELETE_EMPLOYEE", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EMPID", EmpId);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Employee Deleted";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Deletion Failed.";
                    }
                }

            }
            catch (Exception ex)
            {
                sres.Message = ex.Message;
                return sres;
            }
            finally
            {

            }

            return sres;
        }

        public async Task<ServiceResponse<Employee>> GetEmployeeById(string EmpId)
        {
            ServiceResponse<Employee> obj = new ServiceResponse<Employee>();
            Employee emp = new Employee();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_GET_EmployeeInfoByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EID", EmpId);
                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            emp.EmpID = Convert.ToInt32(table.Rows[i]["EmpId"].ToString());
                            emp.FirstName = table.Rows[i]["FirstName"].ToString();
                            emp.LastName = table.Rows[i]["LastName"].ToString();
                            emp.MiddleName = table.Rows[i]["MiddleName"].ToString();
                            emp.CellPhone = table.Rows[i]["CellPhone"].ToString();
                            emp.Email = table.Rows[i]["Email"].ToString();
                            emp.DOB = table.Rows[i]["DOB"].ToString();
                            emp.HomePhone = table.Rows[i]["HomePhone"].ToString();
                            emp.ExtEmpId = table.Rows[i]["ExtEmpId"].ToString();
                            emp.Country = table.Rows[i]["Country"].ToString();
                            emp.City = table.Rows[i]["City"].ToString();
                            emp.CState = table.Rows[i]["CState"].ToString();
                            emp.ZipCode = table.Rows[i]["ZipCode"].ToString();
                            emp.Gender = table.Rows[i]["Gender"].ToString();
                        }
                        obj.Result = true;
                    }
                    obj.Data = emp;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
            finally
            {

            }
        }

    }
}
