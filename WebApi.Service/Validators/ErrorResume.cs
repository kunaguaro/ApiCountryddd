using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Service.Validators
{
    public class ErrorResume
    {
        public class ApiError
        {
            public string ErrorSource { get; set; }

            public int ErrorCount { get; set; }

            public List<ApiErrores> Errores { get; set; }
        }

        public class ApiErrores
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
        }


        public ApiError ErrorReturn(ValidationException ex)
        {
            // return BadRequest(ex.Message);
            ApiError apiError = new ApiError();
            apiError.ErrorCount = ex.Errors.Count();
            apiError.ErrorSource = ex.Source;
            List<ValidationFailure> errores = new List<ValidationFailure>();
            foreach (var error in ex.Errors)
            {
                errores.Add(new ValidationFailure(error.PropertyName, error.ErrorMessage));
            }
            var lqError = errores.Select(x => new ApiErrores() { PropertyName = x.PropertyName, ErrorMessage = x.ErrorMessage });
            apiError.Errores = lqError.ToList();
            return apiError;
        }
    }
}
