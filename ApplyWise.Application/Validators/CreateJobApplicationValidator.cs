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
        url = url?.Trim();

        if (string.IsNullOrEmpty(url)) return true;

        bool temProtocolo = url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                           url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);

        if (!temProtocolo)
        {
            url = "http://" + url;
        }

        if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
        {
            return (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                   && uriResult.Host.Contains(".");
        }

        return false;
    }
}
