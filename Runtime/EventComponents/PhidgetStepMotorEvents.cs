using Phidgets;
using UnityEngine;

namespace a
{
    public class PhidgetStepMotorEvents: MonoBehaviour
    {
        [SerializeField] private float target;
        [SerializeField] private PhidgetStepMotorObject stepMotorObject;

        public void AddMotorTarget(float motorTarget)
        {
            if (stepMotorObject == null)
            {
                Debug.LogWarning($"Missing StepMotorObject: {gameObject.name}");
                return;
            }
            
            stepMotorObject.AddCurrentPosition(target);
        }

        public void AddMotorTarget()
        {
            AddMotorTarget(target);
        }

        public void SetMotorTarget(float target)
        {
            if (stepMotorObject == null)
            {
                Debug.LogWarning($"Missing StepMotorObject: {gameObject.name}");
                return;
            }
            
            stepMotorObject.MoveToTarget(target);
        }
    }
}