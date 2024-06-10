namespace Restaurant.Domain.Common.Files;

public class FileContent
{
    public FileContent(byte[] bytes)
    {
        Bytes = bytes;
        Stream = new MemoryStream(bytes);
    }

    public FileContent(Stream stream)
    {
        stream.CopyTo(Stream);
        Stream.Seek(0, SeekOrigin.Begin);

        using var ms = new MemoryStream();
        Stream.CopyTo(ms);
        Bytes = ms.ToArray();
        Stream.Seek(0, SeekOrigin.Begin);
    }

    public Stream Stream { get; set; } = new MemoryStream();

    public byte[] Bytes { get; set; }
}