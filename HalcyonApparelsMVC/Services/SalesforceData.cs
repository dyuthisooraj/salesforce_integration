﻿using HalcyonApparelsMVC.Interfaces;
using HalcyonApparelsMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStack.Web;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace HalcyonApparelsMVC.Services
{
    public class SalesforceData: ISalesforceData
    {
        public List<CustomerDetailsMVC> SalesforceCustomerDetails(string access_token)
        {


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var url = "https://team4-step-dev-ed.develop.my.salesforce.com/services/apexrest/GetUserDetails";
            List<CustomerDetailsMVC> customerdata = new List<CustomerDetailsMVC>();
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "GET";

            httpRequest.Accept = "application/json";
            httpRequest.Headers["Authorization"] = "Bearer " + access_token;
            httpRequest.ContentType = "application/json";



            //using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            //{
            //    streamWriter.Write(data);
            //}

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var response = streamReader.ReadToEnd();
                customerdata = JsonConvert.DeserializeObject<List<CustomerDetailsMVC>>(response);
                //foreach(CustomerDetailsMVC cdata in customerdata)
                //{
                //    cdata.OrderDetails= SalesforceOrderDetails(access_token, cdata.ContactId);
                //}
            }
            return customerdata;
        }


        //public List<OrderDetailsMVC> SalesforceOrderDetails(string access_token, string id)
        ////public List<OrderDetailsMVC> SalesforceOrderDetails(string access_token)
        //{

            
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //    var url = "https://team4-step-dev-ed.develop.my.salesforce.com/services/apexrest/GetUserDetails?userId=";
        //    url = url + id;
        //    List<OrderDetailsMVC> orderdata = new List<OrderDetailsMVC>();
        //    var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        //    httpRequest.Method = "GET";

        //    httpRequest.Accept = "application/json";
        //    httpRequest.Headers["Authorization"] = "Bearer " + access_token;
        //    httpRequest.ContentType = "application/json";



        //    //using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        //    //{
        //    //    streamWriter.Write(data);
        //    //}

        //    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //    {
        //        var response = streamReader.ReadToEnd();
        //        orderdata = JsonConvert.DeserializeObject<List<OrderDetailsMVC>>(response);

        //    }
        //    return orderdata;
        //}


        public bool Post(List<CustomerDetailsMVC> custdet)
        {

            HttpClientHandler clienthandler = new HttpClientHandler();
            clienthandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslpolicyerrors) => { return true; };
            
            HttpClient client = new HttpClient(clienthandler);
            client.BaseAddress = new Uri("https://localhost:7200");

            var postTask = client.PostAsJsonAsync<List<CustomerDetailsMVC>>("api/SalesforceData/Post", custdet);
           
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return true;
            }
             return false;  
        }

    }
}
