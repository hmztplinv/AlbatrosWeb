using FluentValidation;

public class CreateSliderDto
{
    public string Title { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string? Description { get; set; }
}

public class CreateSliderDtoValidator : AbstractValidator<CreateSliderDto>
{
    public CreateSliderDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl is required.")
            .MaximumLength(500).WithMessage("ImageUrl cannot exceed 500 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.").MinimumLength(10).WithMessage("Description must be at least 10 characters.");

    }
}