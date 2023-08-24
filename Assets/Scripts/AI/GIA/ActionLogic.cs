using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public abstract class ActionLogic : Data
    {
        protected static List<ActionLogic> actions = new();

        public static void Load(string folder = "")
        {
            actions.Clear();
            actions.AddRange(Resources.LoadAll<ActionLogic>(folder));
        }

        public static ActionLogic Get(string uid)
        {
            foreach (ActionLogic a in actions)
            {
                if (a.UID == uid)
                {
                    return a;
                }
            }

            Debug.LogError($"No GoapAction with Id {uid} exists.");
            return null;
        }

        public static List<string> GetAllIds()
        {
            HashSet<string> uids = new();
            foreach (ActionLogic a in actions)
            {
                uids.Add(a.UID);
            }
            return new List<string>(uids);
        }

        public static List<ActionLogic> GetAll()
        {
            return actions;
        }

        public abstract void Init(AIAgent agent);
        public abstract void StartAction(AIAgent agent);
        public abstract void StopAction(AIAgent agent);
        public virtual void UpdateAction(AIAgent agent)
        {

        }

        protected virtual void FaceTowards(Vector3 target, Transform unit)
        {
            Vector3 facing;
            facing = target - unit.position;
            facing.y = 0f;
            facing.Normalize();

            //Apply Rotation
            Quaternion targ_rot = Quaternion.LookRotation(facing, Vector3.up);
            Quaternion nrot = Quaternion.RotateTowards(unit.rotation, targ_rot, 360f);
            unit.rotation = nrot;
        }
    }
}