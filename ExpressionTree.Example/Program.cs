using ExpressionTree;

namespace ExpressionTree.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string? expression;

            if (args is not null && args.Length > 0)
                expression = args[0];
            else
            {
                Console.WriteLine("Type an expression: ");
                expression = Console.ReadLine()?.Trim();
            }

            static void print(BTNode<Data> node) => Console.Write($"({node.Data})");

            try
            {
                IIterableTree<BTNode<Data>>? tree = new ExpressionTree(expression) as IIterableTree<BTNode<Data>>;

                tree?.InOrder(print);
                Console.WriteLine();

                tree?.PreOrder(LogicInference);
                Console.WriteLine();

                tree?.PostOrder(ModusTollens);
                Console.WriteLine();

                tree?.InOrder(ModusPonens);
                Console.WriteLine();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"There was an error: \nError message: \n{ex.Message}");
            }
        }

        private static void LogicInference(BTNode<Data> node)
        {
            string[] operators = { "and", "or", "=>" };
            if (operators.Contains(node.Data.Value))
                // Do stuff e.g: changing the expression as conjuntion or disjuntion through modus ponens or modus tollens
                /* if (node.Data.Value == "or")
                 *      ModusTollens(node);
                 */
                return;
        }

        private static void ModusPonens(BTNode<Data> node)
        {
            // Do stuff
        }

        private static void ModusTollens(BTNode<Data> node)
        {
            // Do stuff
        }
    }
}