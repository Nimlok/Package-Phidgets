using Phidget22;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Outputs/StepMotor")]
    public class PhidgetStepMotorObject: PhidgetBaseObject
    {
        [SerializeField] private int acceleration = 7500;
        [SerializeField] private int velocityLimit = 10000;

        private Stepper stepper => (Stepper)Phidget; 
        
        public override void InitialisePhidget()
        {
            Phidget = new Stepper();
            stepper.Attach += (o, e) =>
            {
                stepper.ControlMode = StepperControlMode.Step;
                stepper.Acceleration = acceleration;
                stepper.VelocityLimit = velocityLimit;
                stepper.HoldingCurrentLimit = 0;
                stepper.CurrentLimit = 1;
                stepper.Engaged = true;
            };
            base.InitialisePhidget();
        }
        
        public void AddCurrentPosition(double target)
        {
            var targetPosition = target + stepper.Position;
            stepper.BeginSetTargetPosition(targetPosition, result =>
            {
                try {
                    stepper.EndSetTargetPosition(result);
                } catch (PhidgetException ex) 
                {
                    Debug.LogError("Async Failure: " + ex.Message);
                }
            }, null);
        }
        
        public void MoveToTarget(double target)
        {
            stepper.BeginSetTargetPosition(target, result =>
            {
                try {
                    stepper.EndSetTargetPosition(result);
                } catch (PhidgetException ex) 
                {
                    Debug.LogError("Async Failure: " + ex.Message);
                }
            }, null);
        }
    }
}