using System;
using System.Collections.Generic;

namespace TestTask.Tree
{
    public interface ITreeNode<TKey, TValue>
    {
        ICollection<TreeNode<TKey, TValue>> Children { get; set; }

        int Priority { get; set; }

        TKey Key { get; set; }

        TValue Value { get; set; }

        TreeNode<TKey, TValue> Parent { get; set; }

        void Traverse(Action<ITreeNode<TKey, TValue>> action);
    }
}
