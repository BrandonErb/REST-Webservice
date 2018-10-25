//Test Harness for Order

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

            //GET TEST
            try
            {
                Order ret = new Order();
                //Second Test
                WebServiceHost host = new WebServiceHost(typeof(EmporiumOrder), new Uri("http://localhost:9579/"));
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IOrder), new WebHttpBinding(), "");
                host.Open();
                ChannelFactory<IOrder> cf = new ChannelFactory<IOrder>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IOrder channel = cf.CreateChannel();
                ret = channel.FindOrder("1");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                resp.Close();           
                Console.WriteLine(resp);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //POST TEST
            try
            {
                Order ret = new Order();
                ret.OrderID = 143;
                ret.CustID = 1;
                ret.OrderDate = "2016-11-11";
                ret.PoNumber = "GRAP-2016-11-11";
                ChannelFactory <IOrder> cf = new ChannelFactory<IOrder>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IOrder channel = cf.CreateChannel();
                ret = channel.CheckInOrder(ret);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //PUT TEST
            try
            {
                Order ret = new Order();
                ret.OrderID = 102;
                ret.CustID = 60;
                ret.OrderDate = "2016-19-11";
                ret.PoNumber = "GRAP-2016-19-11";
                ChannelFactory<IOrder> cf = new ChannelFactory<IOrder>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IOrder channel = cf.CreateChannel();
                ret = channel.UpdateOrderInfo(ret.CustID.ToString(), ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //PUT DELETE
            try
            {
                Order ret = new Order();
                ChannelFactory<IOrder> cf = new ChannelFactory<IOrder>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IOrder channel = cf.CreateChannel();
                ret = channel.CheckOutOrder("999");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}