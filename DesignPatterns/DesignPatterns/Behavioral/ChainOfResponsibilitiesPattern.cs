using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class ChainOfResponsibilitiesPattern
    {
        public static void ShowDemo()
        {
            IParticipent p3 = new Participent(null) { Name = "Raja" };
            IParticipent p2 = new Participent(p3) { Name = "Sree" };
            IParticipent p1 = new Participent(p2) { Name = "Sumesh" };
            List<IParticipent> participents = new List<IParticipent>() { p1, p2, p3 };
            IQuestion q1 = new Question("Is CPU a part of computer?", "Yes");
            IQuestion q2 = new Question("Is IAS is computer course?", "No");
            IQuestion q3 = new Question("Is HCL is indian compeny?", "Yes");
            List<IQuestion> questions = new List<IQuestion>() { q1, q2, q3 };
            QuizSystem myQuiz = new QuizSystem(questions, participents);
            myQuiz.StartEvent();
        }
    }

    class QuizSystem
    {
        private List<IQuestion> listOfQuestions;
        private List<IParticipent> listOfParticipents;

        public QuizSystem(List<IQuestion> questions, List<IParticipent> participents)
        {
            listOfQuestions = questions;
            listOfParticipents = participents;
        }

        public void StartEvent()
        {
            IParticipent nextParticipent = listOfParticipents.First();

            foreach (var q in listOfQuestions)
            {
                nextParticipent = nextParticipent.DoAnswer(q);

                if (nextParticipent == null)
                    nextParticipent = listOfParticipents.First();
            }

            Console.WriteLine("\nEnd of quiz. Score card.");
            foreach (var p in listOfParticipents)
            {
                Console.WriteLine(p.Name + " : " + p.Score);
            }

            Console.WriteLine("\nThanks for participating.");
        }
    }

    interface IQuestion
    {
        string Question1 { get; set; }
        bool CheckAnswer(string ans);
    }

    class Question : IQuestion
    {
        public string Question1 { get; set; }
        private string Answer;

        public Question(string q, string a)
        {
            Question1 = q;
            Answer = a;
        }

        public bool CheckAnswer(string ans)
        {
            return ans.Equals(Answer, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    interface IParticipent
    {
        string Name { get; set; }
        int Score { get; set; }
        IParticipent DoAnswer(IQuestion question);
    }

    class Participent : IParticipent
    {
        private IParticipent oNextParticipent;
        public string Name { get; set; }
        public int Score { get; set; }

        public Participent(IParticipent nextParticipent)
        {
            oNextParticipent = nextParticipent;
            Score = 0;
        }

        public IParticipent DoAnswer(IQuestion question)
        {
            Console.WriteLine("Hi " + Name + ", Please answer for the question " + question.Question1);
            string ans = Console.ReadLine();

            if (question.CheckAnswer(ans))
            {
                Console.WriteLine("Correct Answer");
                this.Score += 1;
                return this.oNextParticipent;
            }
            else if (oNextParticipent != null)
            {
                Console.WriteLine("Wrong Answer. Pass on to next participent.");
                return oNextParticipent.DoAnswer(question);
            }
            else
            {
                Console.WriteLine("No one answered");
                return null;
            }
        }
    }
}
