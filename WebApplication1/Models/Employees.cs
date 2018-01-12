﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeStatus { get; set; }
        public string Department { get; set; }
        public string Shift { get; set; }
        public string Manager { get; set; }
        public string ImageFileName { get; set; }
        public string FavoriteColor { get; set; }
        public string ReturnStartDateForDisplay
        {
            get
            {
                return this.StartDate.ToString("d");
            }
        }
        public string ReturnEndDateForDisplay
        {
            get
            {
                return this.EndDate.ToString("d");
            }
        }
    }
}