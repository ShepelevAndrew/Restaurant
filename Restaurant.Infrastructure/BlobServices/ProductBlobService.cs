using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Restaurant.Application.Common.Abstractions.BlobService;
using Restaurant.Domain.Common.Files;

namespace Restaurant.Infrastructure.BlobServices;

public class ProductBlobService : IBlobService
{
    private readonly IWebHostEnvironment _host;
    private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

    public ProductBlobService(IWebHostEnvironment host)
    {
        _host = host;
    }

    public void Create(IFormFile file, string alias)
    {
        using var blob = file.OpenReadStream();
        using var fileStream = File.Create(Path.Combine(_path, alias + Path.GetExtension(file.FileName)));
        blob.Seek(0, SeekOrigin.Begin);
        blob.CopyTo(fileStream);
    }

    public IFile? GetByAlias(string alias)
    {
        var filePath = Directory.GetFiles(_host.WebRootPath, "*", SearchOption.AllDirectories)
            .FirstOrDefault(path => path.Contains(alias));
        if (filePath is null)
            return null;

        using var file = new FileStream(filePath, FileMode.Open);

        return new FileResult(
            Path.GetFileName(filePath),
            $"image/{Path.GetExtension(filePath).TrimStart('.')}",
            file);
    }

    public void Update(IFormFile file, string alias)
    {
        var filePath = Directory.GetFiles(_host.WebRootPath, "*", SearchOption.AllDirectories)
            .FirstOrDefault(path => path.Contains(alias));
        if (filePath is null)
            return;

        File.Delete(filePath);

        using var blob = file.OpenReadStream();
        using var fileStream = File.Create(Path.Combine(_path, alias + Path.GetExtension(file.FileName)));
        blob.Seek(0, SeekOrigin.Begin);
        blob.CopyTo(fileStream);
    }
}