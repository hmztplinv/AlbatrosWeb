using FluentValidation;

public class UpdateSliderDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
}

public class UpdateSliderDtoValidator : AbstractValidator<UpdateSliderDto>
{
    public UpdateSliderDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(500).WithMessage("ImageUrl cannot exceed 500 characters.");
        
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.").MinimumLength(10).WithMessage("Description must be at least 10 characters.");
    }
}