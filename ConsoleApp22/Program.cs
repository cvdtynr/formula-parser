using System;
using System.Threading.Tasks;

namespace ConsoleApp22
{

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Calc>");
                var parser = new Parser(Console.ReadLine());
                var node = parser.Result();
                Console.WriteLine(Calc(node as dynamic));
            }
        }

        static double Calc(NumberASTNode node) => node.Value();
        static double Calc(AdditionBinaryOperatorASTNode node) => Calc(node.LeftNode as dynamic) + Calc(node.RightNode as dynamic);
        static double Calc(SubtractionBinaryOperatorASTNode node) => Calc(node.LeftNode as dynamic) - Calc(node.RightNode as dynamic);
        static double Calc(MultiplicationBinaryOperatorASTNode node) => Calc(node.LeftNode as dynamic) * Calc(node.RightNode as dynamic);
        static double Calc(DivisionBinaryOperatorASTNode node) => Calc(node.LeftNode as dynamic) / Calc(node.RightNode as dynamic);

    }
}
