using Microsoft.AspNetCore.Http;
using Restaurant.Domain.Common.Files;

namespace Restaurant.Application.Common.Abstractions.BlobService;

public interface IBlobService
{
    void Create(IFormFile file, string alias);

    IFile? GetByAlias(string alias);

    void Update(IFormFile file, string alias);
}