//Test Harness for Cart

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
            const string uri = "http://localhost:9579/FindCart?cartid=1";

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;

            req.KeepAlive = false;
            req.Method = "GET";
            req.ContentType = "text/plain;charset=utf-8";
            try
            {
                Cart ret = new Cart();
                //Second Test
                WebServiceHost host = new WebServiceHost(typeof(EmporiumCart), new Uri("http://localhost:9579/"));
                ServiceEndpoint ep = host.AddServiceEndpoint(typeof(ICart), new WebHttpBinding(), "");
                host.Open();
                ChannelFactory<ICart> cf = new ChannelFactory<ICart>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICart channel = cf.CreateChannel();
                ret = channel.FindCart("1");

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
                Cart ret = new Cart();
                ret.OrderID = 143;
                ret.ProdID = "1";
                ret.Quantity = "130";
                ChannelFactory <ICart> cf = new ChannelFactory<ICart>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICart channel = cf.CreateChannel();
                ret = channel.CheckInCart(ret);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //PUT TEST
            try
            {
                Cart ret = new Cart();
                ret.OrderID = 16;
                ret.ProdID = "1";
                ret.Quantity = "130";
                ChannelFactory<ICart> cf = new ChannelFactory<ICart>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICart channel = cf.CreateChannel();
                ret = channel.UpdateCartInfo(ret.OrderID.ToString(), ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            PUT DELETE
            try
            {
                Cart ret = new Cart();
                ChannelFactory<ICart> cf = new ChannelFactory<ICart>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                ICart channel = cf.CreateChannel();
                ret = channel.CheckOutCart("3");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}