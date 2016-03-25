using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class InterpreterPattern
    {
        public static void ShowDemo()
        {
            string roman = "MCMXXVIII";
            Context context = new Context(roman);
            RomanToNumbericParser.Parse(context);
            Console.WriteLine("{0} = {1}",
            roman, context.Output);
            ArithmeticExpressionParser parser = new ArithmeticExpressionParser();

            string[] commands = new string[]
            {
            "+ 5 6",
            "- 6 5",
            "/ 10 5",
            "+ * 4 5 6",
            "+ 4 - 5 6",
            "+ - + - - 2 3 4 + - -5 6 + -7 8 9 10"
            };

            foreach (string command in commands)
            {
                ExpressionBase<int> expression = parser.Parse(command);
                Console.WriteLine("{0} = {1}", expression, expression.Evaluate());
            }
        }
    }

    class RomanToNumbericParser
    {
        public static void Parse(Context context)
        {
            // Build the 'parse tree'
            List<Expression> tree = new List<Expression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());

            // Interpret
            foreach (Expression exp in tree)
            {
                exp.Interpret(context);
            }
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    class Context
    {
        public string Input { get; set; }
        public int Output { get; set; }
        public Context(string input)
        {
            Input = input;
        }
    }

    interface IExpression
    {
        void Interpret(Context context);
    }

    abstract class Expression : IExpression
    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0)
                return;
            if (context.Input.StartsWith(Nine()))
            {
                context.Output += (9 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Four()))
            {
                context.Output += (4 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Five()))
            {
                context.Output += (5 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
            while (context.Input.StartsWith(One()))
            {
                context.Output += (1 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
        }

        protected abstract string One();
        protected abstract string Four();
        protected abstract string Five();
        protected abstract string Nine();
        protected abstract int Multiplier();
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// One checks for I, II, III, IV, V, VI, VI, VII, VIII, IX
    /// </remarks>
    /// </summary>
    class OneExpression : Expression
    {
        protected override string One() { return "I"; }
        protected override string Four() { return "IV"; }
        protected override string Five() { return "V"; }
        protected override string Nine() { return "IX"; }
        protected override int Multiplier() { return 1; }
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// Ten checks for X, XL, L and XC
    /// </remarks>
    /// </summary>
    class TenExpression : Expression
    {
        protected override string One() { return "X"; }
        protected override string Four() { return "XL"; }
        protected override string Five() { return "L"; }
        protected override string Nine() { return "XC"; }
        protected override int Multiplier() { return 10; }
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// Hundred checks C, CD, D or CM
    /// </remarks>
    /// </summary>
    class HundredExpression : Expression
    {
        protected override string One() { return "C"; }
        protected override string Four() { return "CD"; }
        protected override string Five() { return "D"; }
        protected override string Nine() { return "CM"; }
        protected override int Multiplier() { return 100; }
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// Thousand checks for the Roman Numeral M 
    /// </remarks>
    /// </summary>
    class ThousandExpression : Expression
    {
        protected override string One() { return "M"; }
        protected override string Four() { return " "; }
        protected override string Five() { return " "; }
        protected override string Nine() { return " "; }
        protected override int Multiplier() { return 1000; }
    }

    class ArithmeticExpressionParser
    {
        public ExpressionBase<int> Parse(string polish)
        {
            List<string> symbols = new List<string>(polish.Split(' '));
            return ParseNextExpression(symbols);
        }

        private ExpressionBase<int> ParseNextExpression(List<string> symbols)
        {
            int value;
            if (int.TryParse(symbols[0], out value))
            {
                symbols.RemoveAt(0);
                return new TerminalExpression<int>(value);
            }
            else
            {
                return ParseNonTerminalExpression(symbols);
            }
        }

        private ExpressionBase<int> ParseNonTerminalExpression(List<string> symbols)
        {
            string symbol = symbols[0];
            symbols.RemoveAt(0);
            ExpressionBase<int> expr1 = ParseNextExpression(symbols);
            ExpressionBase<int> expr2 = ParseNextExpression(symbols);

            switch (symbol)
            {
                case "+":
                    return new NonTerminalExpression<int>(expr1, expr2, (a, b) => a + b, "+");
                case "-":
                    return new NonTerminalExpression<int>(expr1, expr2, (a, b) => a - b, "-");
                case "*":
                    return new NonTerminalExpression<int>(expr1, expr2, (a, b) => a * b, "*");
                case "/":
                    return new NonTerminalExpression<int>(expr1, expr2, (a, b) => a / b, "/");
                default:
                    string message = string.Format("Invalid Symbol ({0})", symbol);
                    throw new InvalidOperationException(message);
            }
        }
    }

    abstract class ExpressionBase<T>
    {
        public abstract T Evaluate();
    }

    class TerminalExpression<T> : ExpressionBase<T>
    {
        private T _value;

        public TerminalExpression(T val)
        {
            _value = val;
        }

        public override T Evaluate()
        {
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }

    class NonTerminalExpression<T> : ExpressionBase<T>
    {
        private ExpressionBase<T> _exp1;
        private ExpressionBase<T> _exp2;
        private Func<T, T, T> _exp;
        private string _opSymbel;

        public NonTerminalExpression(ExpressionBase<T> exp1, ExpressionBase<T> exp2, Func<T, T, T> exp, string opSymbel)
        {
            _exp1 = exp1;
            _exp2 = exp2;
            _exp = exp;
            _opSymbel = opSymbel;
        }

        public override T Evaluate()
        {
            T val1 = _exp1.Evaluate();
            T val2 = _exp2.Evaluate();
            return _exp(val1, val2);
        }

        public override string ToString()
        {
            return string.Format("({0} {2} {1})", _exp1, _exp2, _opSymbel);
        }
    }
}
