using MediatR;
using SocialMediaBackend.Application.RequestParameters;

namespace SocialMediaBackend.Application.Features.Users.Queries.SearchUser;

public class SearchUserQueryRequest : IRequest<SearchUserQueryResponse>
{
    public string SearchTerm { get; set; }
    public Pagination Pagination { get; set; } = new Pagination();
}
