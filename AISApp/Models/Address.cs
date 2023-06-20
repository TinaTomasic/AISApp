﻿using System.ComponentModel.DataAnnotations;

namespace AISApp.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string? Street { get; set; }
        public string? City { get; set; }    

    }
}
