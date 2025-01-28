using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using practiceMVCApplication3.Models;
using Newtonsoft.Json;

namespace practiceMVCApplication3.Controllers
{
    public class PracticeController : Controller
    {

        SqlConnection con = new SqlConnection ( "data source=LAPTOP-84MRHNGB; initial catalog=dbprac_1; integrated security=true");
        public ActionResult Index()
        {
            return View();
        }
         
        public void userInsert( userModel obj)
        {
           if(obj.UserID == 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert_user", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", obj.Name);
                cmd.Parameters.AddWithValue("@userCity", obj.City);
                cmd.Parameters.AddWithValue("@userAge", obj.Age);
                cmd.Parameters.AddWithValue("@country", obj.Country);
                cmd.Parameters.AddWithValue("@state", obj.State);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update tbluser set userName='" + obj.Name + "',userCity='" + obj.City + "' ,userAge='" + obj.Age + "', country='"+obj.Country+"', state='"+obj.State+"' where userId='" + obj.UserID + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
       
        }
        public JsonResult userDisplay(userModel obj) { 
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tbluser join tblcountry on country=cid join tblstate on state=sid", con);
            SqlDataAdapter sda=new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            sda.Fill(dt);
            con.Close();
            string data=JsonConvert.SerializeObject(dt);
            return Json(data,JsonRequestBehavior.AllowGet);

        }
        public JsonResult countryDisplay()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from tblcountry", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult stateDisplay(userModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from tblstate where cid ='"+obj.Country+"'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); 
            sda.Fill(dt);
            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public void userDelete(userModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("user_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userId", obj.UserID);
            cmd.ExecuteNonQuery ();
            con.Close();
        }
       
        public JsonResult userEdit(userModel obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("user_edit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userId", obj.UserID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            string data = JsonConvert.SerializeObject(dt);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}