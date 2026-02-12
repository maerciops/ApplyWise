using ApplyWise.Application.DTOs;
using FluentValidation;

namespace ApplyWise.Application.Validators;

public class CreateJobApplicationValidator: AbstractValidator<JobApplicationRequest>
{
    public CreateJobApplicationValidator()
    {
        RuleFor(field => field.Company).NotEmpty().MaximumLength(100);
        RuleFor(field => field.JobTitle).NotEmpty().MaximumLength(100);
        RuleFor(field => field.Description).NotEmpty();
        RuleFor(field => field.SalaryRange).GreaterThan(0);
        RuleFor(field => field.URL).Must(LinkValido);
    }

    private bool LinkValido(string? url)
    {
        if (string.IsNullOrEmpty(url)) return true; // Deixe o NotEmpty lidar com isso se for obrigatório

        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
