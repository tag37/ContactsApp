﻿using System.ComponentModel.DataAnnotations;

namespace ContactsApp.ModelDto
{
    public class ContactResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}