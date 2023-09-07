using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using SqlDataReader = System.Data.SqlClient.SqlDataReader;
using SqlConnection = System.Data.SqlClient.SqlConnection;
using SqlCommand = System.Data.SqlClient.SqlCommand;

namespace webapi.Controllers
{
    public class EmployeeController : Controller
    {
        private IConfiguration _configuration; 

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetEmployee")]

        public JsonResult GetEmployee()
        {
            string query = "select * from dbo.Notes";
            DataTable dt = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;

            using(SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                //myReader = myCon.Open();

                using (SqlCommand myComm = new SqlCommand(query, myCon)) 
                {
                    myReader = myComm.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Got Employees Successfully");
        }

        [HttpPost]
        [Route("AddEmployee")]

        public JsonResult AddEmployee(string empFName, string empLName, string description)
        {
            string query = "insert into dbo.Notes values(@empFName, @empLName, @description)";
            DataTable dt = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("todoAppDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                // myReader = myCon.Open()

                using (SqlCommand myComm = new SqlCommand(query, myCon))
                {
                    myComm.Parameters.AddWithValue("@empFName", empFName);
                    myComm.Parameters.AddWithValue("@empLName", empLName);
                    myComm.Parameters.AddWithValue("@description", description);
                    myReader = myComm.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Employee Successfully");
        }
    }
}
