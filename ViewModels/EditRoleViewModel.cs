﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagment.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new  List<string>();
        }
        public string? Id { get; set; }
        [Required] 
        public string? RoleName { get; set; }
       public List<string>? Users { get; set; }
    }
}
