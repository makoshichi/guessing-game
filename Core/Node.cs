namespace GuessingGameReproduction.Core
{
    public class Node
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Node AnswerYes { get; set; }
        public Node AnswerNo { get; set; }

        public Node() { }

        public Node(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }
    }
}
