﻿using FlowaStudy.Common.Validation;

namespace FlowaStudy.WebApi.Commom
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<ValidationErrorDetail> Errors { get; set; } = [];
    }
}
