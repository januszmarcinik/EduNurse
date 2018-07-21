﻿using System;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams.Shared.Results
{
    public class ExamResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ExamType Type { get; set; }
        public string Category { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
