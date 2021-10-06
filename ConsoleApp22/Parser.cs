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
            if (_lexer.Peek().TokenType == TokenType.OPEN_PAR)
            {
                _lexer.Accept();
                var node = Expression();
                _lexer.Expect(x => x == Lexer.CLOSE_PAR);
                _lexer.Accept();
                return node;
            }

            if (_lexer.Peek().TokenType == TokenType.NUMBER)
                return new NumberASTNode(_lexer.ReadNext());

            throw new Exception($"Unexpected token {_lexer.Peek().Value} at position : {_lexer.Position} ");
        }

    }
}
