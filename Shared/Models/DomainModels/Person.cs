﻿using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.DomainModels
{
    public class Person
    {
        // relationships
        public int Id { get; set; }
        public Settings Settings { get; set; }

        // fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Registered { get; set; }
        public bool Verified { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Birthdate { get; set; }
        public PersonType PersonType { get; set; }
    }
}