namespace Events.Application.Extensions;

public static class StreamExtensions
{
	public static byte[] ToByteArray(this Stream input)
	{
		using (MemoryStream ms = new MemoryStream())
		{
			input.CopyTo(ms);
			return ms.ToArray();
		}
	}
}
