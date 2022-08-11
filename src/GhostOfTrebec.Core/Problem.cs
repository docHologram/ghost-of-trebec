using GhostOfTrebec.Core.InnerCore;

namespace GhostOfTrebec.Core
{
    public class Problem : Entity<string>
    {

        [Obsolete("Should only be used by EF")]
        private Problem(): base(default!)
        {
            Question = default!;
            Answers = default!;
        }

        public Problem(string identifier, string question, IReadOnlyList<Answer> answers): base(identifier)
        {
            Question = question;
            Answers = answers;
        }
       
        public string? Question { get; set; }
        public IReadOnlyList<Answer>? Answers { get; set; }
    }
}