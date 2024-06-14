#if UNITY_EDITOR
using System;
using PhidgetControls;
using UnityEngine;

namespace Phidgets
{
    [Serializable]
    public struct TestKeys
    {
        public KeyCode keyCode;
        public int port;
    }
    
    [RequireComponent(typeof(PhidgetController))]
    public class PhidgetEditorInputs : MonoBehaviour
    {
        [SerializeField] private TestKeys[] keyCodes;
    
        private PhidgetController phidgetController;
        private int hubNumber;

        private void Awake()
        {
            phidgetController = GetComponent<PhidgetController>();
        }

        private void Update()
        {

            foreach (var key in keyCodes)
            {
                if (Input.GetKeyDown(key.keyCode))
                {
                    phidgetController.TriggerPhidget(hubNumber, key.port);
                }
            }

            if (Input.GetKeyDown(KeyCode.Plus))
            {
                AdjustHubNumber();
                Debug.Log($"Phidget Editor Test: Hub {hubNumber}");
            }
        }

        private void AdjustHubNumber()
        {
            hubNumber++;
            if (hubNumber > phidgetController.GetHubs.Count)
            {
                hubNumber = 0;
            }
        }
    }
}
#endif