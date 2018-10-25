/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : REST methods for the Cart DB Table 
*FILE           : Cart.cs
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
    /// This code is developed for HTTP GET, PUT, POST & DELETE operation.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EmporiumCart : ICart
    {

        //Cart Cart = new Cart();
        //POST
        public Cart CheckInCart(Cart incomingCart)
        {
            try
            { 
            Cart response = new Cart();
            string cs = "user id=sa;password=Conestoga1;server=127.0.01;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
            SqlConnection connection = new SqlConnection(cs);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "INSERT INTO Cart (orderID, prodID, quantity) VALUES (" + incomingCart.OrderID + ", " + incomingCart.ProdID + ", " + incomingCart.Quantity + ")";
            cmd.ExecuteNonQuery();

            connection.Close();
            return incomingCart;
            }
            catch (Exception)
            {
                Cart error = new Cart();
                error.OrderID = 0;
                error.ProdID = "Error";
                error.Quantity = "";
                return error;
            }
        }

        //GET
        public Cart FindCart(string whatToFind)
        {
            Cart response = new Cart();
            try
            {

                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.Connection = connection;

                cmd.CommandText = "SELECT * FROM Cart WHERE orderID = " + whatToFind + "";
                
                //balance = (decimal)cmd.ExecuteScalar();
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    response.OrderID = (int)reader["orderID"];
                    response.ProdID = reader["prodID"].ToString();
                    response.Quantity = reader["quantity"].ToString();
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();

                return response;

            }
            catch (Exception)
            {
                Cart error = new Cart();
                error.OrderID = 0;
                error.ProdID = "Error";
                error.Quantity = "";
                return error;
            }
        }


        //PUT
        public Cart UpdateCartInfo(string orderID, Cart whichCart)
        {
            Cart response = new Cart();
            // need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "IF EXISTS (SELECT * FROM Cart WHERE orderID = " + orderID  + ") UPDATE Cart SET orderID = " + whichCart.OrderID + ", prodID = " + whichCart.ProdID + ", quantity = " + whichCart.Quantity + " WHERE orderID =" + whichCart.OrderID + "; ELSE INSERT INTO Cart(orderID,prodID,quantity ) Values(" + whichCart.OrderID + ", " + whichCart.ProdID + ", " + whichCart.Quantity + ")";

                cmd.ExecuteNonQuery();

                connection.Close();

                return response;
                
            }
            catch (Exception Ex) 
            {
                Cart error = new Cart();
                error.OrderID = 0;
                error.ProdID =  Ex.Message;
                error.Quantity = "";
                return error;
            }
        }


        //DELETE
        public Cart CheckOutCart(string orderID)
        {
            Cart response = new Cart();
            // we need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=5";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "DELETE FROM Cart WHERE orderID=" + orderID + "";

                cmd.ExecuteNonQuery();
                return response;
            }
            catch (Exception Ex)
            {
                Cart error = new Cart();
                error.OrderID = 0;
                error.ProdID = Ex.Message;
                error.Quantity = "";
                return error;
            }
        }

    }
}