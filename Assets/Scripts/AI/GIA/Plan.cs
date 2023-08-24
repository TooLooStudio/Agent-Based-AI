using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TooLoo.AI
{
    public class Plan : IEquatable<Plan>
    {
        private readonly List<ActionLogic> actions = new();

        public List<ActionLogic> Actions => actions;

        public Plan(ICollection<ActionLogic> actions)
        {
            this.actions.AddRange(actions);
        }

        public Plan(ActionLogic a)
        {
            this.actions.Add(a);
        }

        public Queue<ActionLogic> ActionQueue()
        {
            return new Queue<ActionLogic>(actions);
        }

        public string ActionSequence()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ActionLogic a in actions)
            {
                sb.Append($" --> {a.name}");
            }

            return sb.ToString();
        }

        public bool Contains(ActionLogic a)
        {
            return actions.Contains(a);
        }

        public bool Equals(Plan other)
        {
            if (other == null) return false;

            if (actions.Count != other.actions.Count) return false;

            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i] != other.Actions[i]) return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Plan);
        }

        public override int GetHashCode()
        {
            return actions.Select(s => s.GetHashCode()).Aggregate(0, (acc, val) => acc ^ val);
        }

        public static bool operator ==(Plan left, Plan right)
        {
            return EqualityComparer<Plan>.Default.Equals(left, right);
        }

        public static bool operator !=(Plan left, Plan right)
        {
            return !(left == right);
        }
    }
}