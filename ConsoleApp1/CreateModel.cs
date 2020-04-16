using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConsoleApp1
{
    public class CreateModel
    {
        [Display(Name ="First Name")]
        public string MyProperty { get; set; }
        public CreateModel(string name)
        {
            MyProperty = name;
        }
    }
}
