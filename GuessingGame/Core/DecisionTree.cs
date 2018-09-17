namespace GuessingGameReproduction.Core
{
    public class DecisionTree
    {
        private Node _tree;
        public int NodesCount { get; private set; }
        public Node Tree
        {
            get { return _tree; }
            set
            {
                NodesCount++;
                _tree = value;
            }
        }

        public DecisionTree(Node startingTree)
        {
            // root node, shark and monkey leaves
            _tree = startingTree;
            NodesCount = 3;
        }
    }
}
