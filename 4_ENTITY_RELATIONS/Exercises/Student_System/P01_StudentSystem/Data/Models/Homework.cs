﻿using System;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    public class Homework
    {
        [Key]
        public int HomeworkId { get; set; }

        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public int? StudentId { get; set; }

        public Student Student { get; set; }

        public int? CourseId { get; set; }

        public Course Course { get; set; }

        public DateTime SubmissionTime { get; set; }
    }
}
