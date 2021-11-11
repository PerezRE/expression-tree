namespace ExpressionTree
{
    public class BTNode<T>
    {
        public T Data { get; set; }
        public BTNode<T> Left { get; set; }
        public BTNode<T> Right { get; set; }

        public BTNode(T data, BTNode<T> left = null, BTNode<T> right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}