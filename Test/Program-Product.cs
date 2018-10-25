//Test Harness for Product

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
            WebServiceHost host = new WebServiceHost(typeof(EmporiumProduct), new Uri("http://localhost:9579/"));
            ServiceEndpoint ep = host.AddServiceEndpoint(typeof(IProduct), new WebHttpBinding(), "");
            host.Open();
            try
            {         
                Product ret = new Product();
                ChannelFactory<IProduct> cf = new ChannelFactory<IProduct>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IProduct channel = cf.CreateChannel();
                ret = channel.FindProduct("1");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //POST TEST
            try
            {
                Product ret = new Product();
                ret.ProdID = "143";
                ret.ProdName = "Boxes";
                ret.Price = 120;
                ret.ProdWeight = 50;
                ret.InStock = 1;
                ChannelFactory<IProduct> cf = new ChannelFactory<IProduct>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IProduct channel = cf.CreateChannel();
                ret = channel.CheckInProduct(ret);

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //PUT
            try
            {
                Product ret = new Product();
                ret.ProdID = "145";
                ret.ProdName = "Barrels";
                ret.Price = 1000;
                ret.ProdWeight = 1;
                ret.InStock = 6;
                ChannelFactory<IProduct> cf = new ChannelFactory<IProduct>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IProduct channel = cf.CreateChannel();
                ret = channel.UpdateProductInfo(ret.ProdID, ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //DELETE
            try
            {
                Product ret = new Product();
                ChannelFactory<IProduct> cf = new ChannelFactory<IProduct>(new WebHttpBinding(), "http://localhost:9579");
                cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
                IProduct channel = cf.CreateChannel();
                ret = channel.CheckOutProduct("2");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            host.Close();

        }
    }
}