using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp22
{
    public class Lexer
    {
        const char PLUS = '+';
        const char MINUS = '-';
        const char MULTIPLICATION = '*';
        const char DIVISION = '/';
        const char DECIMAL_SEPERATOR = ',';
        const char OPEN_PAR = '(';
        public const char CLOSE_PAR = ')';

        static readonly char[] E_NOTATION = new char[] { 'e', 'E' };
        static readonly char[] SIGN_OPERATORS = new char[] { PLUS, MINUS };


        readonly static Dictionary<char, Func<int, char, Token>> _operators =
         new Dictionary<char, Func<int, char, Token>>
         {
                { PLUS,(int x,char y)=>new Token(TokenType.ADDITION,y.ToString(),x) },
                { MINUS,(int x,char y)=>new Token(TokenType.SUBTRACTION,y.ToString(),x) },
                { MULTIPLICATION,(int x,char y)=>new Token(TokenType.MULTIPLICATION,y.ToString(),x) },
                { DIVISION,(int x,char y)=>new Token(TokenType.DIVISION,y.ToString(),x) },
                { OPEN_PAR,(int x,char y)=>new Token(TokenType.OPEN_PAR,y.ToString(),x) },
                { CLOSE_PAR,(int x,char y)=>new Token(TokenType.CLOSE_PAR,y.ToString(),x) },
         };

        private readonly SourceScanner _scanner;

        private Token CurrentToken { get; set; }

        public Lexer(string text)
        {
            _scanner = new SourceScanner(text);
        }

        public Token Peek()
        {
            _scanner.Push();
            var token = ReadNext();
            _scanner.Pop();
            return token;
        }

        public char Accept() => _scanner.Read().Value;

        public void Expect(Predicate<char> match)
        {
            if (!IsNext(match))
                throw new Exception($"Unexpected value at position {Position}");
        }

        private bool IsNext(params char[] possibleValues)
            => IsNext(x => possibleValues.Any(k => k == x));

        private bool IsNext(Predicate<char> match)
        {
            var lookahead = _scanner.Peek();
            return lookahead.HasValue && match(lookahead.Value);
        }

        public int Position => _scanner.Position;

        public Token ReadNext()
        {
            if (_scanner.EndOfSource)
                return new Token(TokenType.EOF, null, _scanner.Position);

            ConsumeWhiteSpace();

            if (TryTorkenizeOperator(out Token token))
                return token;

            if (TryTorkenizeNumber(out token))
                return token;

            throw new Exception($"Unexpected character '{_scanner.Peek()}' found at position : {_scanner.Position}");
        }

        private void ConsumeWhiteSpace()
        {
            while (IsNext(char.IsWhiteSpace))
                Accept();
        }

        private bool TryTorkenizeOperator(out Token token)
        {
            token = null;

            if (IsNext(_operators.ContainsKey))
            {
                var position = Position;
                var op = Accept();
                token = _operators[op](position, op);
            }

            return token != null;
        }

        private bool TryTorkenizeNumber(out Token token)
        {
            token = null;
            var sb = new StringBuilder();
            var position = Position;

            sb.Append(ReadDigits());

            if (IsNext(DECIMAL_SEPERATOR))
                sb.Append(Accept());

            sb.Append(ReadDigits());


            if (sb.Length > 0)
                token = new Token(TokenType.NUMBER, sb.ToString(), position);

            return token != null;
        }


        private string ReadDigits()
        {
            StringBuilder builder = new StringBuilder();

            while (IsNext(char.IsDigit))
                builder.Append(Accept());

            return builder.ToString();

        }

    }
}
