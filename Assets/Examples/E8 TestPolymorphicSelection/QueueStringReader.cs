using System.Collections.Concurrent;
using System.IO;
namespace Examples.E8_TestPolymorphicSelection
{
public class QueueStringReader : TextReader
{
	ConcurrentQueue<string> StringQueue = new();
	StringReader Reader;
	public override int Read()
	{
		int c = Reader.Read();
		if (c == -1 && StringQueue.TryDequeue(out string s))
		{
			Reader = new(s);
			return Reader.Read();
		}
		return c;
	}
	public override int Peek() => Reader.Peek();
	public void Enqueue(string s) => StringQueue.Enqueue(s);
}
}