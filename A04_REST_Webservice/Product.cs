/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : REST Methods for Product DB Table 
*FILE           : Product.cs
**************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Data.SqlClient;
using System.Web.Services;
using System.Data.SqlTypes;
namespace A04_REST_Webservice
{
    /// <summary>
    /// Basically this code is developed for HTTP GET, PUT, POST & DELETE operation.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EmporiumProduct : IProduct
    {
        //POST
        public Product CheckInProduct(Product incomingProduct)
        {
            Product response = new Product();
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "INSERT INTO Product (prodID, prodName, price, prodWeight, inStock) VALUES ('" + incomingProduct.ProdID + "', '" + incomingProduct.ProdName + "', " + incomingProduct.Price + ", " + incomingProduct.ProdWeight + "," + incomingProduct.InStock + ")";
                cmd.ExecuteNonQuery();

                connection.Close();
                return response;
            }
            catch (Exception Ex)
            {
                Product error = new Product();
                response.ProdID = "";
                response.ProdName = Ex.Message;
                response.Price = 0;
                response.ProdWeight = 0;
                response.InStock = 0;
                return error;
            }
        }

        //GET
        public Product FindProduct(string prodid)
        {
            Product response = new Product();
            try
            {

                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.Connection = connection;

                cmd.CommandText = "SELECT * FROM Product WHERE prodID = " + prodid + "";

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    response.ProdID = reader["prodID"].ToString();
                    response.ProdName = (reader["prodName"].ToString());
                    string price = reader["price"].ToString();
                    string weight = reader["prodWeight"].ToString();
                    response.Price = float.Parse(price);
                    response.ProdWeight = float.Parse(weight);
                    response.InStock = (int)reader["inStock"];
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();

                return response;

            }
            catch (Exception Ex)
            {
                Product error = new Product();
                response.ProdID = "";
                response.ProdName = Ex.Message;
                response.Price = 0;
                response.ProdWeight = 0;
                response.InStock = 0;
                return error;
            }
        }


        //PUT
        public Product UpdateProductInfo(string prodid, Product whichProduct)
        {
            Product response = new Product();
            // we need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "IF EXISTS (SELECT * FROM Product WHERE prodID = " + prodid + ") UPDATE Product SET prodID = '" + whichProduct.ProdID + "', prodName = '" + whichProduct.ProdName + "', price = " + whichProduct.Price + ", prodWeight = " + whichProduct.ProdWeight + ", inStock = " + whichProduct.InStock + " WHERE prodID =" + whichProduct.ProdID + "; ELSE INSERT INTO Product(prodID,prodName,price,prodWeight,inStock) Values(" + whichProduct.ProdID + ", '" + whichProduct.ProdName + "', " + whichProduct.Price + ", " + whichProduct.ProdWeight + ", " + whichProduct.InStock +")";
                cmd.ExecuteNonQuery();

                connection.Close();

                return response;

            }
            catch (Exception Ex)
            {
                Product error = new Product();
                response.ProdID = "";
                response.ProdName = Ex.Message;
                response.Price = 0;
                response.ProdWeight = 0;
                response.InStock = 0;
                return error;
            }
        }


        //DELETE
        public Product CheckOutProduct(string prodid)
        {
            Product response = new Product();
            // we need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "DELETE FROM Product WHERE prodID='" + prodid + "'";

                cmd.ExecuteNonQuery();
                return response;
            }
            catch (Exception Ex)
            {
                Product error = new Product();
                response.ProdID = "";
                response.ProdName = Ex.Message;
                response.Price = 0;
                response.ProdWeight = 0;
                response.InStock = 0;
                return error;
            }
        }
    }
}