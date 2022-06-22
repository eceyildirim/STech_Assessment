using FluentValidation;
using Report.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Business.Validators
{
    public class ReportValidator : AbstractValidator<ReportModel>
    {
        public ReportValidator()
        {
        }
    }
}
