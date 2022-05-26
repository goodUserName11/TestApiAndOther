﻿using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class CretitionModel
    {
        public CretitionModel()
        {

        }

        public CretitionModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
