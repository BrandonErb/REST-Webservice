/*************
*Author         : Brandon Erb
*Instructor     : Ed Barsalou
*Date           : 11/13/2016
*Description    : Service contract for Customer Table DB Table 
*FILE           : ICustomer.cs
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
    public interface ICustomer
    {
        //POST operation -- Insert
        [OperationContract]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        Customer CheckInCustomer(Customer newCustomer);

        //Get Operation
        [OperationContract]
        [WebGet(UriTemplate = "{custid}")]
        Customer FindCustomer(string custId);

        //PUT Operation -- Update
        [OperationContract]
        [WebInvoke(UriTemplate = "{custid}", Method = "PUT")]
        Customer UpdateCustomerInfo(string custid, Customer whichCustomer);

        //DELETE Operation
        [OperationContract]
        [WebInvoke(UriTemplate = "{custid}", Method = "DELETE")]
        Customer CheckOutCustomer(string custid);

    }

    #endregion

    #region Customer Entity
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string CustID;
        [DataMember]
        public string FirstName;
        [DataMember]
        public string LastName;
        [DataMember]
        public string PhoneNumber;

    }
    #endregion
}