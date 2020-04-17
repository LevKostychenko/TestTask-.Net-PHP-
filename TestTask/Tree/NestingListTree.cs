using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.Tree
{
    public sealed class NestingListTree : ITreeNodeCollection<string, string>
    {
        private ICollection<TreeNode<string, string>> _treeNodes;

        private ITreeNode<string, string> _rootNode;

        public NestingListTree(Dictionary<string, string> dictionarySet)
        {
            List<TreeNode<string, string>> priorityQueue = CreatePriorityQueue(dictionarySet).ToList();
            CreateTreeFromPriorityQueue(priorityQueue);
        }

        public IEnumerable<TreeNode<string, string>> CreatePriorityQueue(Dictionary<string, string> dictionarySet)
        {
            var priorityQueue = new List<TreeNode<string, string>>();

            foreach (var pair in dictionarySet)
            {
                List<string> nestingLevels = pair.Key.Trim().Split('.').ToList();
                var parsedNestingLevels = ParseToNumericCollection(nestingLevels);

                if (!parsedNestingLevels.Any())
                {
                    throw new Exception("Cannot conver dictionary to tree.");
                }

                int priority = parsedNestingLevels.Count;

                priorityQueue.Add(new TreeNode<string, string>(pair.Key, pair.Value, priority));
            }

            return priorityQueue.OrderBy(x => x.Priority).ThenBy(x => String.IsNullOrEmpty(x.Key.Split('.').Last())? 
                Int32.Parse(x.Key.Trim().Split('.')[0]) : 
                Int16.Parse(x.Key.Trim().Split('.').Last())).ToList();
        }

        public void CreateTreeFromPriorityQueue(List<TreeNode<string, string>> priorityQueue)
        {
            _treeNodes = new List<TreeNode<string, string>>();
            var rootNodeChildren = priorityQueue.Where(x => x.Priority == 1).ToList();
            _rootNode = new TreeNode<string, string>("0", "0", 0, rootNodeChildren, null);
            var parent = _rootNode;
            foreach (var node in priorityQueue)
            {                
                var children = priorityQueue.Where(x => x.Priority == node.Priority + 1 && x.Key.StartsWith(node.Key)).ToList();

                var newNode = node;
                newNode.Parent = (TreeNode<string, string>)parent;
                newNode.Children = children;
                _treeNodes.Add(newNode);

                parent = _treeNodes.FirstOrDefault(x => x.Children.Contains(node));
            }
        }

        public void Traverse(Action<ITreeNode<string, string>> action)
        {
            foreach (var node in _rootNode.Children)
            {
                node.Traverse(action);
            }
        }

        private List<int> ParseToNumericCollection(IEnumerable<string> nestingLevels)
        {
            var parsedNestingLevels = new List<int>();

            foreach (string nestingLevel in nestingLevels)
            {
                int parsedLevel;
                bool isParsed = Int32.TryParse(nestingLevel, out parsedLevel);

                if (isParsed)
                {
                    parsedNestingLevels.Add(parsedLevel);
                }
            }

            return parsedNestingLevels;
        }
    }
}
