using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using aspPrac0102019.Models;

namespace aspPrac0102019.Controllers
{
    public class DashboardController : Controller
    {

        string connectionString = @"Data Source=DESKTOP-VHGKKQ6;Initial Catalog = Proj011919;Integrated Security=True";
        [HttpGet]
        // GET: Product
        //View
        public ActionResult Dashboard()
        {
            DataTable accountLists = new DataTable();
            using (SqlConnection sqlConnect = new SqlConnection(connectionString))
            {
                sqlConnect.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("Select * From Accounts ", sqlConnect);
                sqlDa.Fill(accountLists);

            }
            return View(accountLists);
        }

        //Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Account());
        }

        [HttpPost]
        public ActionResult Create(Account acc)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "INSERT INTO Accounts VALUES(@username, @password, @email)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@username", acc.username);
                sqlCmd.Parameters.AddWithValue("@password", acc.password);
                sqlCmd.Parameters.AddWithValue("@email", acc.email);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Dashboard");
        }


        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Account accModel = new Account();
            DataTable dtblProduct = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Select * From Accounts where id = @id";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@id", id);
                sqlDa.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                accModel.id =Int32.Parse(dtblProduct.Rows[0][0].ToString());
                accModel.username = dtblProduct.Rows[0][1].ToString();
                accModel.password = dtblProduct.Rows[0][2].ToString();
                return View(accModel);
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
            //return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Account accModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE Accounts Set username = @username, password = @password where id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@id", accModel.id);
                sqlCmd.Parameters.AddWithValue("@username", accModel.username);
                sqlCmd.Parameters.AddWithValue("@password", accModel.password);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Dashboard");

        }

        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "Delete from Accounts where id = @id";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@id", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Dashboard");
        }


    }
}