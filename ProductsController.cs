using DotNetEndExamMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetEndExamMVC.Controllers
{
    public class ProductsController : Controller
    {

        [ChildActionOnly]
        public ActionResult PartialView1()
        {
            return View();
        }


        // GET: Products
        public ActionResult Index()
        {
            List<Product> listProd = new List<Product>();


            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cn.Open();

            SqlCommand cmdIndex = new SqlCommand();
            cmdIndex.Connection = cn;
            cmdIndex.CommandType = System.Data.CommandType.StoredProcedure;
            cmdIndex.CommandText = "IndexProcedure";



        try
        {
                SqlDataReader dr=  cmdIndex.ExecuteReader();
                while (dr.Read())
                {
                    listProd.Add(new Product { ProductId = (int)dr["ProductId"], ProductName = (string)dr["ProductName"], Rate = (decimal)dr["Rate"], Description = (string)dr["Description"], CategoryName = (string)dr["CategoryName"] });
                } dr.Close();

        } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        finally { cn.Close();  }

            return View(listProd);
        }

    

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {

        
                SqlConnection cn = new SqlConnection();
                cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                cn.Open();

                SqlCommand cmdIndex = new SqlCommand();
                cmdIndex.Connection = cn;
                cmdIndex.CommandType = System.Data.CommandType.Text;
                cmdIndex.CommandText = "Select * from Products where ProductId= @id";
                cmdIndex.Parameters.AddWithValue("@id", id);

                Product obj = null;
            try
            {
                SqlDataReader dr= cmdIndex.ExecuteReader();
                if (dr.Read())
                {
                    obj = new Product() { ProductId = (int)dr["ProductId"], ProductName = (string)dr["ProductName"], Rate = (decimal)dr["Rate"], Description = (string)dr["Description"], CategoryName = (string)dr["CategoryName"] };
                } 
                dr.Close();

                return View(obj);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction("Index");
            }
            finally { cn.Close();  }
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product obj)
        {


            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DotNetExam;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            cn.Open();

            SqlCommand cmdEdit = new SqlCommand();
            cmdEdit.Connection = cn;
            cmdEdit.CommandType = System.Data.CommandType.StoredProcedure;
            cmdEdit.CommandText = "EditProcedure";

            cmdEdit.Parameters.AddWithValue("@ProductId", id);
            cmdEdit.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmdEdit.Parameters.AddWithValue("@Rate", obj.Rate);
            cmdEdit.Parameters.AddWithValue("@Description", obj.Description);
            cmdEdit.Parameters.AddWithValue("@CategoryName", obj.CategoryName);

            try
            {
                cmdEdit.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
            finally { cn.Close(); }
        }

      
    }
}
