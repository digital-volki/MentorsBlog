﻿using System;

namespace MentorsBlog.Core.DataAccess.Models
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}