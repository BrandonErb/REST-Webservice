/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : Service Contract for Cart DB Table 
*FILE           : ICart.cs
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
    #region ICart Interface

    [ServiceContract]
    [XmlSerializerFormat]
    public interface ICart
    {
        //POST operation -- Insert
        [OperationContract]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        Cart CheckInCart(Cart incomingCart);

        //Get Operation
        [OperationContract]
        [WebGet(UriTemplate = "{orderid}")]
        Cart FindCart(string orderid);

        //PUT Operation -- Update
        [OperationContract]
        [WebInvoke(UriTemplate = "{orderid}", Method = "PUT")]
        Cart UpdateCartInfo(string orderid, Cart whichCart);

        //DELETE Operation
        [OperationContract]
        [WebInvoke(UriTemplate = "{orderid}", Method = "DELETE")]
        Cart CheckOutCart(string orderid);

    }

    #endregion

    #region Cart Entity
    [DataContract]
    public class Cart
    {
        [DataMember]
        public int OrderID;
        [DataMember]
        public string ProdID;
        [DataMember]
        public string Quantity;

    }
    #endregion
}