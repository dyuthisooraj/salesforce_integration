﻿using HalcyonApparelsApplication.DTO;
using HalcyonApparelsApplication.Interfaces;
using HalcyonApparelsDomain.Entities;
using HalcyonApparelsInfrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalcyonApparelsInfrastructure.Implementation
{
    public class SalesforceCrud: ISalesforceCrud
    {
        private readonly AppDBContext _appDBContext;

        

        public SalesforceCrud(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public bool SalesforcePost(List<CustomerDTO> customerDTO)
        {
            try
            {
                foreach (CustomerDTO i in customerDTO)
                {
                    var custmodel = new CustomerDetails()
                    {
                        ContactId = i.ContactId,
                        Fname = i.Fname,
                        Lname = i.Lname,
                        Email = i.Email
                    };


                    //var custid = _appDBContext.CustomerDetails.Where(model => model.ContactId == custmodel.ContactId).FirstOrDefault();


                    //if (custid != null)
                    //{

                      

                        _appDBContext.CustomerDetails.Add(custmodel);
                        _appDBContext.SaveChanges();

                        foreach (OrderDetails j in i.orderList)
                        {
                            var ordermodel = new OrderDetails()
                            {
                                Id = j.Id,
                                Contact__c = i.ContactId,
                                Parent_Order_Id__c = j.Parent_Order_Id__c,
                                ordered_Date__c = j.ordered_Date__c,
                                Product_Type__c = j.Product_Type__c,

                            };

                            _appDBContext.OrderDetails.Add(ordermodel);
                            _appDBContext.SaveChanges();
                        }

                    }
                }
            

            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
