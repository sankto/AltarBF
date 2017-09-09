using System;
using System.IO;
using System.Text;

namespace AltarBF {
	class Program {
		public const string HelloWorldWithNewLine = "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++.";
		public const string ReadAllInput = ",[.,]";
		public const string InnerLoopTest = "[.+[-]+]+.";

		static void Main(string[] args) {
			using (var reader = GenerateStreamFromString("Test")) {
				new BrainfuckInstance(InnerLoopTest, reader, Console.Out);
				new BrainfuckInstance(HelloWorldWithNewLine, reader, Console.Out);
				new BrainfuckInstance(ReadAllInput, reader, Console.Out);
			}
			Console.WriteLine();
			Console.WriteLine("-------------------");
			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		public static StreamReader GenerateStreamFromString(string s) {
			var stream = new MemoryStream();
			using (var writer = new StreamWriter(stream, Encoding.ASCII, s.Length, true)) {
				writer.Write(s);
				writer.Flush();
				stream.Position = 0;
			}
			return new StreamReader(stream);
		}
	}
}
