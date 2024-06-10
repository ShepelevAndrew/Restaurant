namespace Restaurant.Domain.Common.Files;

public class FileResult : IFile
{
    public FileResult(string name, string contentType, Stream content)
    {
        Name = name;
        ContentType = contentType;
        Content = new FileContent(content);
    }

    public FileResult(string name, string contentType, byte[] content)
    {
        Name = name;
        ContentType = contentType;
        Content = new FileContent(content);
    }

    public string Name { get; }

    public string ContentType { get; }

    public FileContent Content { get; private set; }
}