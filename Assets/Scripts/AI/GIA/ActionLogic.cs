using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI
{
    public abstract class ActionLogic : ScriptableObject
    {
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