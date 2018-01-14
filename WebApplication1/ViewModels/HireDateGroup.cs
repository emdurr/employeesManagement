using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class HireDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        public int EmployeeCount { get; set; }

        public int Week1 { get; set; }
    }
}
