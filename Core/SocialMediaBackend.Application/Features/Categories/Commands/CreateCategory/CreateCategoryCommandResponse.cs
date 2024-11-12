namespace SocialMediaBackend.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse
    {
        public string Message { get; set; }
    }

    public class CreateCategoryCommandSuccessResponse : CreateCategoryCommandResponse
    {
        public bool Succeeded { get; set; } = true;
    }

    public class CreateCategoryCommandErrorResponse : CreateCategoryCommandResponse
    {
        public bool Succeeded { get; set; } = false;
    }
}
