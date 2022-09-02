﻿using System.ComponentModel.DataAnnotations;

namespace LeaveManagenet.Web.Models
{
    public class LeaveAllocationVM
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Number Of Days")]
        [Required]
        [Range(1,50, ErrorMessage = "Enter value between 1 - 50.")]
        public int NumberOfDays { get; set; }

        [Required]
        public int Period { get; set; }
        public LeaveTypeVM? LeaveType { get; set; }
    }
}
