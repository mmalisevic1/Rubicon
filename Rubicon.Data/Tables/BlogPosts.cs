﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rubicon.Data.Tables
{
    public class BlogPosts
    {
        [MaxLength(300)]
        [NonUnicode]
        [Required]
        public string Slug { get; set; }

        [MaxLength(300)]
        [Required]
        public string Title { get; set; }

        [MaxLength(1000)]
        [Required]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Tags> Tags { get; set; }
    }
}
