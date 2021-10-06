using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    public class Parser
    {
        private readonly Lexer _lexer;

        public Parser(string text)
        {
            _lexer = new Lexer(text);
        }

        public ASTNode Result() => Expression();

        private ASTNode Expression()
        {
            var left = Term();

            var peekToken = _lexer.Peek();

            while (peekToken.TokenType == TokenType.ADDITION
                || peekToken.TokenType == TokenType.SUBTRACTION)
            {
                peekToken = _lexer.ReadNext();
                switch (peekToken.TokenType)
                {
                    case TokenType.ADDITION:
                        var right = Term();
                        left = new AdditionBinaryOperatorASTNode(peekToken, left, right);
                        break;
                    case TokenType.SUBTRACTION:
                        right = Term();
                        left = new SubtractionBinaryOperatorASTNode(peekToken, left, right);
                        break;
                }

                peekToken = _lexer.Peek();
            }

            return left;
        }

        private ASTNode Term()
        {
            ASTNode left = Factor();

            var peekToken = _lexer.Peek();

            while (peekToken.TokenType == TokenType.MULTIPLICATION
                || peekToken.TokenType == TokenType.DIVISION)
            {
                peekToken = _lexer.ReadNext();
                switch (peekToken.TokenType)
                {
                    case TokenType.MULTIPLICATION:
                        var right = Factor();
                        left = new MultiplicationBinaryOperatorASTNode(peekToken, left, right);
                        break;
                    case TokenType.DIVISION:
                        right = Factor();
                        left = new DivisionBinaryOperatorASTNode(peekToken, left, right);
                        break;
                }

                peekToken = _lexer.Peek();
            }

            return left;
        }

        private ASTNode Factor()
        {
            ASTNode node;

            if (IsNext(TokenType.OPEN_PAR))
            {
                Accept();
                node = Expression();
                Expect(TokenType.CLOSE_PAR);
                Accept();
                return node;
            }
            else
            {
                node = ParseNumber();
            }

            return node;
        }

        private ASTNode ParseNumber()
        {
            Expect(TokenType.NUMBER);
            return new NumberASTNode(Accept());
        }

        private Token Accept() => _lexer.ReadNext();

        private void Expect(TokenType tokenType)
        {
            if (!IsNext(tokenType))
                throw new Exception($"Unexpected token {_lexer.Peek()} at position {_lexer.Position}");
        }

        private bool IsNext(params TokenType[] possibleTokens)
            => IsNext(x => possibleTokens.Any(k => k == x));

        private bool IsNext(Predicate<TokenType> match)
            => match(_lexer.Peek().TokenType);

    }
}
