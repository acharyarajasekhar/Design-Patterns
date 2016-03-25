using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class StatePattern
    {
        public static void ShowDemo()
        {
            TableLamp lamp = new TableLamp();
            lamp.PressSwitch();
            lamp.PressSwitch();
            lamp.PressSwitch();
            lamp.GotError();
            lamp.PressSwitch();
        }
    }

    class TableLamp : StateMachine
    {
        [Trigger]
        public readonly Action PressSwitch;

        [Trigger]
        public readonly Action GotError;

        /// <summary>
        /// A lamp state machine. Typical structure:
        /// <code>
        /// protected override IEnumerable WalkStates()
        /// {
        /// [state name]:
        /// // State entry actions
        /// yield return null; // (wait for a trigger to be called)
        /// // State exit actions
        /// 
        /// // Transitions from this state, by checking which trigger has been called
        /// if (Trigger == PressSwitch) goto [some other state];
        /// 
        /// // Throw an exception if the trigger was invalid
        /// InvalidState();
        /// 
        /// [some other state]:
        /// ...
        /// }
        /// </code> 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable WalkStates()
        {
        off:
            Console.WriteLine("off.");
            yield return null;
            if (Trigger == PressSwitch) goto on;
            InvalidTrigger();
        on:
            Console.WriteLine("*shiiine!*");
            yield return null;
            if (Trigger == GotError) goto error;
            if (Trigger == PressSwitch) goto off;
            InvalidTrigger();
        error:
            Console.WriteLine("-err-");
            yield return null;
            if (Trigger == PressSwitch) goto off;
            InvalidTrigger();
        }
    }

    abstract class StateMachine
    {
        private IEnumerator currentState;

        /// <summary>
        /// Is set to the trigger that was called most recently. Always contains a reference
        /// to a child class property/field decorated with the [Trigger] attribute.
        /// </summary>
        protected Action Trigger { get; private set; }

        public StateMachine()
        {
            SetupTriggers();
            currentState = WalkStates().GetEnumerator();
            currentState.MoveNext();
        }

        protected abstract IEnumerable WalkStates();

        protected void InvalidTrigger()
        {
            throw new InvalidTriggerException("Invalid trigger!");
        }

        /// <summary>
        /// Gets all members returned by the `getMembers` method that have the TriggerAttribute set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getMembers"></param>
        /// <returns></returns>
        private IEnumerable<T> TriggerMembers<T>(Func<BindingFlags, T[]> getMembers) where T : MemberInfo
        {
            return getMembers(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetCustomAttributes(typeof(TriggerAttribute), false).Any());
        }

        private void VerifyMemberType(Type type)
        {
            if (type != typeof(Action))
            {
                throw new InvalidStateMachineException("Fields/properties decorated with [Trigger] must be of type Action");
            }
        }

        /// <summary>
        /// Finds all fields and properties that have the [Trigger] attribute, and assigns a trigger action to them.
        /// </summary>
        private void SetupTriggers()
        {
            var type = GetType();

            foreach (var field in TriggerMembers(type.GetFields))
            {
                VerifyMemberType(field.FieldType);
                field.SetValue(this, MakeTrigger());
            }

            foreach (var prop in TriggerMembers(type.GetProperties))
            {
                VerifyMemberType(prop.PropertyType);
                prop.SetValue(this, MakeTrigger(), null);
            }
        }

        private Action MakeTrigger()
        {
            Action action = null;

            action = () =>
            {
                Trigger = action;
                currentState.MoveNext();
            };

            return action;
        }
    }

    class TriggerAttribute : Attribute { }

    public class InvalidStateMachineException : Exception
    {
        public InvalidStateMachineException(string msg) : base(msg) { }
    }

    public class InvalidTriggerException : Exception
    {
        public InvalidTriggerException(string msg) : base(msg) { }
    }
}
