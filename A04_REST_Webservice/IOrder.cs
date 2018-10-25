/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : Service contract for Order DB Table 
*FILE           : IOrder.cs
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
    #region IOrder Interface

    [ServiceContract]
    [XmlSerializerFormat]
    public interface IOrder
    {
        //POST operation -- Insert
        [OperationContract]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        Order CheckInOrder(Order incomingOrder);

        //Get Operation
        [OperationContract]
        [WebGet(UriTemplate = "{orderid}")]
        Order FindOrder(string orderId);

        //PUT Operation -- Update
        [OperationContract]
        [WebInvoke(UriTemplate = "{orderid}", Method = "PUT")]
        Order UpdateOrderInfo(string orderid, Order whichOrder);

        //DELETE Operation
        [OperationContract]
        [WebInvoke(UriTemplate = "{orderid}", Method = "DELETE")]
        Order CheckOutOrder(string orderid);

    }

    #endregion

    #region Order Entity
    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderID;
        [DataMember]
        public int CustID;
        [DataMember]
        public string OrderDate;
        [DataMember]
        public string PoNumber;

    }
    #endregion
}