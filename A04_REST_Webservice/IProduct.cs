/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : Service contract for Product DB Table 
*FILE           : IProduct.cs
**************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

namespace A04_REST_Webservice
{
    #region ICustomer Interface

    [ServiceContract]
    [XmlSerializerFormat]
    public interface IProduct
    {
        //POST operation -- Insert
        [OperationContract]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        Product CheckInProduct(Product incomingProduct);

        //Get Operation
        [OperationContract]
        [WebGet(UriTemplate = "{prodid}")]
        Product FindProduct(string prodid);

        //PUT Operation -- Update
        [OperationContract]
        [WebInvoke(UriTemplate = "{prodid}", Method = "PUT")]
        Product UpdateProductInfo(string prodid, Product whichProduct);

        //DELETE Operation
        [OperationContract]
        [WebInvoke(UriTemplate = "{prodid}", Method = "DELETE")]
        Product CheckOutProduct(string prodid);

    }

    #endregion

    #region Product Entity
    [DataContract]
    public class Product
    {
        [DataMember]
        public string ProdID;
        [DataMember]
        public string ProdName;
        [DataMember]
        public float Price;
        [DataMember]
        public float ProdWeight;
        [DataMember]
        public int InStock; //Used as a bool

    }
    #endregion
}