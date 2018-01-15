using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public enum Type
    {
        Manager, TeamLead, Director, HR, EmailAccess, Key
    }
    public class Permissions
    {
        [Key]
        [ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public Type? Type { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Removed")]
        public DateTime DateRemoved { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
