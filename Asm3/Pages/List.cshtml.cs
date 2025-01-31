﻿using Asm3.Hubs;
using Asm3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Asm3.Pages
{
    public class ListModel : PageModel
    {
        private MVVMContext context;
        private readonly IHubContext<CustomerHub> hubContext;


        public ListModel(MVVMContext _context, IHubContext<CustomerHub> hubContext)
        {
            this.context = _context;
            this.hubContext = hubContext;
        }

    public List<Customer> customers = new List<Customer>();
        public int DiscountSearch { get; set; }

        public void OnGet([FromQuery] int discount, String? deleteId)
        {
            if (!string.IsNullOrEmpty(deleteId))
            {
                var cus = context.Customers.SingleOrDefault(x => x.CustomerId.Equals(deleteId));
                if (cus != null)
                {
                    context.Remove(cus);
                    context.SaveChanges();
                }
                customers = context.Customers.ToList();
            }

            DiscountSearch = discount;
            if (discount > 0 && discount <= 60)
            {
            
       
                customers = context.Customers
                .Where(x => x.DiscountRate <= discount)
                .ToList();
            }
            else
            {
                TempData["Message"] = "Invalid value. Please enter a value from 0 to 60.";
                customers = context.Customers.ToList();
            }
        }

        public void OnPost(IFormFile inputfile)
        {

            string submitType = Request.Form["submitType"];
            string fileContent = "";
            using (var reader = new StreamReader(inputfile.OpenReadStream()))
            {
                fileContent = reader.ReadToEnd();
            }

            if (submitType == "json")
            {
           
                if (!string.IsNullOrEmpty(fileContent))
                {
                    var listCustomer = JsonConvert.DeserializeObject<List<Customer>>(fileContent);
                    if (listCustomer.Count > 0)
                    {
                        context.Customers.AddRange(listCustomer);
                        context.SaveChanges();
                    }
                }
                customers = context.Customers.ToList();
            }
            else if (submitType == "xml")
            {

                    if (!string.IsNullOrEmpty(fileContent))
                    {
                        XDocument xmlDoc = XDocument.Parse(fileContent);
                        var customers = xmlDoc.Descendants("Customer").Select(c =>
                        {
                            return new Customer
                            {
                                CustomerId = (string)c.Element("CustomerId"),
                                CustomerName = (string)c.Element("CustomerName"),
                                Address = (string)c.Element("Address"),
                                Phone = (string)c.Element("Phone"),
                                DiscountRate = (int)c.Element("DiscountRate")
                            };
                        }).ToList();
                    if (customers.Count > 0)
                    {
                        context.Customers.AddRange(customers);
                        context.SaveChanges();
                    }
                }
                customers = context.Customers.ToList();
            }
            hubContext.Clients.All.SendAsync("Customer");

        }
    }
}