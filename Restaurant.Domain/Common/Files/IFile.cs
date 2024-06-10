namespace Restaurant.Domain.Common.Files;

public interface IFile
{
    string Name { get; }

    string ContentType { get; }

    FileContent Content { get; }
}