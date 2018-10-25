/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : REST Methods for Order DB Table 
*FILE           : Order.cs
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
    public class EmporiumOrder : IOrder
    {
        //POST
        public Order CheckInOrder(Order incomingOrder)
        {
            Order response = new Order();
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Order (orderID, custID, poNumber,orderDate) VALUES (" + incomingOrder.OrderID + ", " + incomingOrder.CustID + ", '" + incomingOrder.OrderDate + "', '" + incomingOrder.PoNumber + "')";
                cmd.ExecuteNonQuery();

                conn.Close();
                return incomingOrder;
            }
            catch (Exception)
            {
                Order error = new Order();
                error.OrderID = 0;
                error.CustID = 0;
                error.OrderDate = "";
                error.PoNumber = "";
                return error;
            }
        }

    //GET
    public Order FindOrder(string whatToFind)
        {
            Order response = new Order();
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=5";
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM Order WHERE orderID = " + whatToFind + "";
                
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    response.OrderID = (int)reader["orderID"];
                    response.CustID = (int)reader["custID"];
                    response.OrderDate = reader["orderDate"].ToString();
                    response.PoNumber = reader["poNumber"].ToString();
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
                Order error = new Order();
                error.OrderID = 0;
                error.CustID = 0;
                error.OrderDate = "";
                error.PoNumber = "";
                return error;
            }
        }


        //PUT
        public Order UpdateOrderInfo(string orderID, Order whichOrder)
        {
            Order response = new Order();
            // we need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=5";
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "IF EXISTS (SELECT * FROM Order WHERE orderID = " + orderID  + ") UPDATE Order SET orderID = '" + whichOrder.OrderID + "', custID = '" + whichOrder.CustID + "', orderDate = '" + whichOrder.OrderDate + "', poNumber = '" + whichOrder.PoNumber + "' WHERE orderID =" + whichOrder.OrderID + "; ELSE INSERT INTO Order(orderID,custID,orderDate,poNumber) Values(" + whichOrder.OrderID + ", '" + whichOrder.CustID + "', '" + whichOrder.OrderDate + "', '" + whichOrder.PoNumber + "')";

                cmd.ExecuteNonQuery();

                conn.Close();

                return response;
                
            }
            catch (Exception Ex) 
            {
                Order error = new Order();
                error.OrderID = 0;
                error.CustID =  0;
                error.OrderDate = Ex.Message;
                error.PoNumber = "";
                return error;
            }
        }


        //DELETE
        public Order CheckOutOrder(string orderID)
        {
            Order response = new Order();
            // we need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=5";
                SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "DELETE FROM Order WHERE orderID='" + orderID + "'";

                cmd.ExecuteNonQuery();
                return response;
            }
            catch (Exception Ex)
            {
                Order error = new Order();
                error.OrderID = 0;
                error.CustID = 0;
                error.OrderDate = Ex.Message;
                error.PoNumber = "";
                return error;
            }
        }

    }
}