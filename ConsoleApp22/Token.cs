namespace ConsoleApp22
{
    public enum TokenType
    {
        NUMBER,
        ADDITION,
        SUBTRACTION,
        MULTIPLICATION,
        DIVISION,
        OPEN_PAR,
        CLOSE_PAR,
        EOF
    }

    public class Token
    {
        public Token(TokenType tokenType, string value, int position)
        {
            TokenType = tokenType;
            Value = value;
            Position = position;
        }

        public TokenType TokenType { get; }
        public string Value { get; }
        public int Position { get; }
    }
}
