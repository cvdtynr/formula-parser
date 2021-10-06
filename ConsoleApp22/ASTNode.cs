using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    public abstract class ASTNode
    {
        public Token Token { get; private set; }

        protected ASTNode(Token token)
        {
            Token = token;
        }
    }

    public class NumberASTNode : ASTNode
    {
        public double Value() => Token.Value.ToDouble();

        public NumberASTNode(Token token) : base(token) { }
    }

    public abstract class OperatorASTNode : ASTNode
    {
        protected OperatorASTNode(Token token) : base(token) { }
    }

    public abstract class BinaryOperatorASTNode : OperatorASTNode
    {
        public ASTNode LeftNode { get; private set; }
        public ASTNode RightNode { get; private set; }

        protected BinaryOperatorASTNode(Token token, ASTNode leftNode, ASTNode rightNode)
            : base(token)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
        }
    }

    public class AdditionBinaryOperatorASTNode : BinaryOperatorASTNode
    {
        public AdditionBinaryOperatorASTNode(Token token, ASTNode leftNode, ASTNode rightNode):
            base(token, leftNode, rightNode)
        {
        }
    }

    public class SubtractionBinaryOperatorASTNode : BinaryOperatorASTNode
    {
        public SubtractionBinaryOperatorASTNode(Token token, ASTNode leftNode, ASTNode rightNode) :
            base(token, leftNode, rightNode)
        {
        }
    }

    public class MultiplicationBinaryOperatorASTNode : BinaryOperatorASTNode
    {
        public MultiplicationBinaryOperatorASTNode(Token token, ASTNode leftNode, ASTNode rightNode) :
            base(token, leftNode, rightNode)
        {
        }
    }

    public class DivisionBinaryOperatorASTNode : BinaryOperatorASTNode
    {
        public DivisionBinaryOperatorASTNode(Token token, ASTNode leftNode, ASTNode rightNode) :
            base(token, leftNode, rightNode)
        {
        }
    }
}
