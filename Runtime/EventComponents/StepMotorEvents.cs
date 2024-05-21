using UnityEngine;

namespace Phidgets.Events
{
    public class StepMotorEvents: MonoBehaviour
    {
        [SerializeField] private float target;
        [SerializeField] private StepMotorObject stepMotorObject;

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