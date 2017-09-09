using System;
using System.Collections.Generic;
using System.IO;

namespace AltarBF {
	public class BrainfuckInstance {
		public const UInt16 MEMSIZE = 30000;

		private readonly string Code;
		private readonly TextReader Reader;
		private readonly TextWriter Writer;
		private readonly Stack<int> Loops;
		private readonly byte[] Memory;
		private UInt16 Ptr;
		private UInt16 InnerLoopsCounter;

		public BrainfuckInstance(string code, TextReader reader, TextWriter writer) {
			Code = code;
			Reader = reader;
			Writer = writer;
			Memory = new byte[MEMSIZE];
			Loops = new Stack<int>();
			Ptr = 0;
			Run();
		}

		private void Run() {
			for (int i = 0; i < Code.Length; i++) {
				switch (Code[i]) {
					case '+':
						Memory[Ptr]++;
						break;
					case '-':
						Memory[Ptr]--;
						break;
					case '>':
						if (++Ptr == MEMSIZE)
							Ptr = 0;
						break;
					case '<':
						if (Ptr == 0)
							Ptr = MEMSIZE - 1;
						else --Ptr;
						break;
					case '.':
						Writer.Write((char)Memory[Ptr]);
						Writer.Flush();
						break;
					case ',':
						Memory[Ptr] = (byte)(Reader.Peek() != -1 ? Reader.Read() : 0);
						break;
					case '[':
						if (Memory[Ptr] == 0) {
							while (++i < Code.Length) {
								if (Code[i] == '[')
									InnerLoopsCounter++;
								else if (Code[i] == ']') {
									if (InnerLoopsCounter == 0)
										break;
									InnerLoopsCounter--;
								}
							}
							if (InnerLoopsCounter != 0)
								throw new Exception("No matching ] found!");
						} else Loops.Push(i);
						break;
					case ']':
						if (Memory[Ptr] == 0)
							Loops.Pop();
						else i = Loops.Peek();
						break;
				}
			}
		}
	}
}
