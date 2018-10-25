//Test Harness for custumer

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceModel;
using A04_REST_Webservice;

namespace Test
{
    public class Program
    {
        static void Main(string[] args)
        {

            WebServiceHost host = new WebServiceHost(typeof(EmporiumCustomer), new Uri("http://localhost:9579/"));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(ICustomer), new WebHttpBinding(), "");
            host.Open();
            //GET
            try
            {
                Customer ret = new Customer();
                ChannelFactory<ICustomer> cf = new ChannelFactory<ICustomer>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICustomer channel = cf.CreateChannel();
                ret = channel.FindCustomer("1");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //POST
            try
            {
                Customer ret = new Customer();
                ret.CustID = "102";
                ret.FirstName = "Steve";
                ret.LastName = "LaDouche";
                ret.PhoneNumber = "647-666-8764";
                ChannelFactory<ICustomer> cf = new ChannelFactory<ICustomer>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICustomer channel = cf.CreateChannel();
                ret = channel.CheckInCustomer(ret);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //PUT
            try
            {
                Customer ret = new Customer();
                ret.CustID = "101";
                ret.FirstName = "Joe";
                ret.LastName = "Jack";
                ret.PhoneNumber = "416-555-1234";
                ChannelFactory<ICustomer> cf = new ChannelFactory<ICustomer>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICustomer channel = cf.CreateChannel();
                ret = channel.UpdateCustomerInfo(ret.CustID.ToString(), ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //DELETE
            try
            {
                Customer ret = new Customer();
                ChannelFactory<ICustomer> cf = new ChannelFactory<ICustomer>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICustomer channel = cf.CreateChannel();
                ret = channel.CheckOutCustomer("101");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            host.Close();

        }
    }
}