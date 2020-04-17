using System;
using System.Collections.Generic;

namespace TestTask.Tree
{
    public interface ITreeNodeCollection<TKey, TValue>
    {
        IEnumerable<TreeNode<TKey, TValue>> CreatePriorityQueue(Dictionary<TKey, TValue> dictionarySet);

        void CreateTreeFromPriorityQueue(List<TreeNode<TKey, TValue>> priorityQueue);

        void Traverse(Action<ITreeNode<TKey, TValue>> action);
    }
}
