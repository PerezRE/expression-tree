using System;

namespace ExpressionTree
{
    public interface IIterableTree<T>
    {
        public void PreOrder(Action<T> fn);

        public void InOrder(Action<T> fn);

        public void PostOrder(Action<T> fn);
    }
}
