using System;
using System.Collections.Generic;

namespace TestTask.Tree
{
    public class TreeNode<TKey, TValue> : ITreeNode<TKey, TValue>
    {
        public ICollection<TreeNode<TKey, TValue>> Children { get; set; }

        public TreeNode<TKey, TValue> Parent { get; set; }

        public int Priority { get; set; }

        public TKey Key { get; set; }        

        public TValue Value { get; set; }


        public TreeNode(TKey key, TValue value, int priority)
        {
            Value = value;
            Key = key;
            Priority = priority;
        }

        public TreeNode(TKey key, TValue value, int priority, ICollection<TreeNode<TKey, TValue>> children, TreeNode<TKey, TValue> parent) : this (key, value, priority)
        {
            Parent = parent;
            Children = children;
        }

        public void Traverse(Action<ITreeNode<TKey, TValue>> action)
        {
            action(this);
            foreach (var node in Children)
            {
                node.Traverse(action);
            }
        }
    }
}
