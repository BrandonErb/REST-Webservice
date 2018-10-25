/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : REST methods for the Customer DB Table 
*FILE           : Customer.cs
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
    public class EmporiumCustomer : ICustomer
    {

        //POST
        public Customer CheckInCustomer(Customer newCustomer)
        {
            try
            {
                Customer response = new Customer();
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "INSERT INTO Customer (firstName, lastName, phoneNumber) VALUES (" + newCustomer.FirstName + "', '" + newCustomer.LastName + "', '" + newCustomer.PhoneNumber + "')";
                cmd.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception)
            {
                Customer error = new Customer();
                error.CustID = "";
                error.FirstName = "Error";
                error.LastName = "";
                error.PhoneNumber = "";
                return error;
            }
            return newCustomer;
        }

        //GET
        public Customer FindCustomer(string findCustomer)
        {
            Customer response = new Customer();
            try
            {

                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.Connection = connection;

                cmd.CommandText = "SELECT * FROM Customer WHERE custID = " + findCustomer + "";
                
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    response.CustID = reader["custID"].ToString();
                    response.FirstName = reader["firstName"].ToString();
                    response.LastName = reader["lastName"].ToString();
                    response.PhoneNumber = reader["PhoneNumber"].ToString();
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
                Customer error = new Customer();
                error.CustID = "";
                error.FirstName = "Error";
                error.LastName = "";
                error.PhoneNumber = "";
                return error;
            }
        }


        //PUT
        public Customer UpdateCustomerInfo(string custid, Customer whichCustomer)
        {
            Customer response = new Customer();
            // we need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "IF EXISTS (SELECT * FROM Customer WHERE custID = " + custid  + ") UPDATE Customer SET custID = '" + whichCustomer.CustID + "', firstName = '" + whichCustomer.FirstName + "', lastName = '" + whichCustomer.LastName + "', phoneNumber = '" + whichCustomer.PhoneNumber + "' WHERE custID =" + whichCustomer.CustID + "; ELSE INSERT INTO Customer(custID,firstName,lastName,phoneNumber) Values(" + whichCustomer.CustID + ", '" + whichCustomer.FirstName + "', '" + whichCustomer.LastName + "', '" + whichCustomer.PhoneNumber + "')";

                cmd.ExecuteNonQuery();

                connection.Close();

                return response;
                
            }
            catch (Exception Ex) 
            {
                Customer error = new Customer();
                error.CustID = "";
                error.FirstName =  Ex.Message;
                error.LastName = "";
                error.PhoneNumber = "";
                return error;
            }
        }


        //DELETE
        public Customer CheckOutCustomer(string custid)
        {
            Customer response = new Customer();
            // we need to check if the ID supplied is actually in the system and is valid
            try
            {
                string cs = "user id=sa;password=Conestoga1;server=127.0.0.1;Trusted_Connection=yes;database=soa_assignment4;connection timeout=30";
                SqlConnection connection = new SqlConnection(cs);
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                cmd.CommandText = "DELETE FROM Customer WHERE custID='" + custid + "'";

                cmd.ExecuteNonQuery();
                return response;
            }
            catch (Exception Ex)
            {
                Customer error = new Customer();
                error.CustID = "";
                error.FirstName = Ex.Message;
                error.LastName = "";
                error.PhoneNumber = "";
                return error;
            }
        }

    }
}