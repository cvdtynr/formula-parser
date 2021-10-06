using System.Collections.Generic;

namespace ConsoleApp22
{
    internal class SourceScanner
    {
        private readonly Stack<int> _positionStack = new Stack<int>();

        private readonly string _buffer;
        public int Position { get; private set; }
        public bool EndOfSource => Position >= _buffer.Length;

        public SourceScanner(string buffer)
        {
            _buffer = buffer;
            Position = 0;
        }

        public char? Read() => EndOfSource ? (char?)null : _buffer[Position++];

        public char? Peek()
        {
            Push();
            var result = Read();
            Pop();
            return result;
        }

        public void Push() => _positionStack.Push(Position);
        public void Pop() => Position = _positionStack.Pop();
    }
}
