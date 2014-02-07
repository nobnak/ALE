using System;
using System.IO;

namespace ALE.FileSystem
{
	internal class AsyncFileReadState
	{
		public readonly Action<Exception, byte[]> Callback;
		public readonly FileStream FileStream;
		public byte[] Buffer;

		public AsyncFileReadState(Action<Exception, byte[]> callback, FileStream fs, byte[] buffer)
		{
			Callback = callback;
			FileStream = fs;
			Buffer = buffer;
		}
	}
}