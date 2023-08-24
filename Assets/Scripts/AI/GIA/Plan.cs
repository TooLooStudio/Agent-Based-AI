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
        private readonly List<string> actionIds = new();

        public List<string> ActionIds => actionIds;

        public Plan(ICollection<string> actionIds)
        {
            this.actionIds.AddRange(actionIds);
        }

        public Plan(string actionId)
        {
            this.actionIds.Add(actionId);
        }

        public Queue<string> ActionQueue()
        {
            return new Queue<string>(actionIds);
        }

        public string ActionSequence()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string id in actionIds)
            {
                sb.Append($" --> {ActionLogic.Get(id).name}");
            }

            return sb.ToString();
        }

        public bool Contains(string id)
        {
            return actionIds.Contains(id);
        }

        public bool Equals(Plan other)
        {
            if (other == null) return false;

            if (actionIds.Count != other.actionIds.Count) return false;

            for (int i = 0; i < actionIds.Count; i++)
            {
                if (actionIds[i] != other.ActionIds[i]) return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Plan);
        }

        public override int GetHashCode()
        {
            return actionIds.Select(s => s.GetHashCode()).Aggregate(0, (acc, val) => acc ^ val);
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