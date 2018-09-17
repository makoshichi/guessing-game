namespace GuessingGameReproduction.Core
{
    public class DecisionTree
    {
        private Node _tree;
        public int LeafCount { get; private set; }
        public Node Tree
        {
            get { return _tree; }
            set
            {
                LeafCount++;
                _tree = value;
            }
        }

        public DecisionTree(Node startingTree)
        {
            // shark and monkey;
            _tree = startingTree;
            LeafCount = 2;
        }
    }
}
