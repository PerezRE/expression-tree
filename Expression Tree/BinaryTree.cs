namespace ExpressionTree
{
    public class BinaryTree<T>
    {
        public BTNode<T> Root { get; set; }
        public BinaryTree<T> LeftTree { get; set; }
        public BinaryTree<T> RightTree { get; set; }

        protected BinaryTree(BTNode<T> root)
        {
            Root = root;
        }
        
        public BinaryTree(T data, BinaryTree<T> left = null, BinaryTree<T> right = null)
        {
            Root = new BTNode<T>(data);
            LeftTree = left;
            RightTree = right;
        }

        public bool IsLeaf() => Root != null && Root.Left == null && Root.Right == null;
    }
}
