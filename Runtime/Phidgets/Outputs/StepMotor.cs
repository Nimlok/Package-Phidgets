using Phidget22;
using UnityEngine;

namespace Phidgets
{
    public class StepMotor: BasePhidget
    {
        private double acceleration = 10000;
        private double velocityLimit = 10000;
        
        private Stepper GetStepper => (Stepper)Phidget;

        public double GetPosition => GetStepper.Position;
        
        public override void InitialisePhidget()
        {
            Phidget = new Stepper();
            GetStepper.Attach += (o, e) =>
            {
                GetStepper.ControlMode = StepperControlMode.Step;
                GetStepper.Acceleration = acceleration;
                GetStepper.VelocityLimit = velocityLimit;
                GetStepper.HoldingCurrentLimit = 0;
                GetStepper.CurrentLimit = 1;
                GetStepper.Engaged = true;
            };
            base.InitialisePhidget();
        }

        public override void TriggerPhidget(object target)
        {
            if (target == null)
            {
                Debug.Log($"Target Missing");
                return;
            }
                
            
            MoveToTarget((float)target);
            base.TriggerPhidget(target);
        }

        public void AddCurrentPosition(double target)
        {
            var targetPosition = target + GetStepper.Position;
            GetStepper.BeginSetTargetPosition(targetPosition, result =>
            {
                try {
                    GetStepper.EndSetTargetPosition(result);
                } catch (PhidgetException ex) 
                {
                    Debug.LogError("Async Failure: " + ex.Message);
                }
            }, null);
        }
        
        public void MoveToTarget(double target)
        {
            GetStepper.BeginSetTargetPosition(target, result =>
            {
                try {
                    GetStepper.EndSetTargetPosition(result);
                } catch (PhidgetException ex) 
                {
                    Debug.LogError("Async Failure: " + ex.Message);
                }
            }, null);
        }
    }
}