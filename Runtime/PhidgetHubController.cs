using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Phidget22;
using Phidget22.Events;
using Sirenix.OdinInspector;

namespace Phidgets
{
    public class PhidgetHubController : MonoBehaviour
    {
        private List<Hub> hubs = new List<Hub>();
        
        public static Action OnHubInitialised;
        
        #region EDITOR

        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        private void AddHub(int serialNumber)
        {
            var newHub = new GameObject();
            var hub = newHub.AddComponent<Hub>();
            hub.name = $"Hub: {serialNumber}";
            hub.serialNumber = serialNumber;
            newHub.transform.SetParent(transform);
        }
        
        [PropertySpace]
        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        private void RemoveHub(int serialNumber)
        {
            var hub = hubs.Find(x => x.serialNumber == serialNumber);
            if (hub == null)
            {
                Debug.Log($"Couldn't find hub: {serialNumber}");
                return;
            }
            
            DestroyImmediate(hub.gameObject);
        }
        #endregion
        
        #region  UNITY
        void Start()
        {
           Initialise();
           OnHubInitialised?.Invoke();
        }
        
        private void OnApplicationQuit()
        {
            CloseHubs();
        }
        #endregion

        #region TESTING
        public void ChangeHubSerialNumber(int deviceNumber, int newSerialNumber)
        {
            var hub = FindHub(deviceNumber);
            if (hub == null)
                return;
            
            CloseHubs();
            hub.serialNumber = newSerialNumber;
            InitialiseHubs();
        }

        public void ReinitialiseHubs()
        {
            CloseHubs();
            Initialise();
        }
        #endregion

        public BasePhidget FindBasePhidget(int port, int serialNumber = -1)
        {
            if (serialNumber != -1)
            {
                var hub = FindHub(serialNumber);
                return hub == null ? null : hub.FindPhidget(port);
            }

            foreach (var hub in hubs)
            {
                var phidget = hub.FindPhidget(port);
                if (phidget == null)
                    continue;

                return phidget;
            }

            return null;
        }
        
        public void TriggerPhidget(object value,int phidgetNumber, int hubSerialNumber)
        {
            if (hubs.Count <= 0)
                return;

            var hub = FindHub(hubSerialNumber);
            if (hub == null)
                return;
            
            hub.TriggerPhidget(value ,phidgetNumber);
        }
        
        private void Initialise()
        {
            InitialiseHubs();
            InitialisePhidgets();
        }
        
        //TODO: DS 20/06/24 Separate to smaller functions
        private void InitialiseHubs()
        {
            if (hubs.Count == 0)
            {
                hubs = GetComponentsInChildren<Hub>().ToList();
            }
            
            if (hubs.Count <= 0)
                return;
            
            var phidgetHubs = CreateHubs();
            
            //Assign Specific hubs
            var unassignedHubs = new List<Hub>(hubs);
            foreach (var hub in hubs)
            {
                if (hub.serialNumber == 0)
                    continue;

                var phidgetHub = phidgetHubs.Find(x => x.DeviceSerialNumber == hub.serialNumber);
                if (phidgetHub == null)
                    continue;
                
                hub.Initialise(phidgetHub);
                unassignedHubs.Remove(hub);
                phidgetHubs.Remove(phidgetHub);
            }
            
            //Assign unassigned Hubs
            for (int i = 0; i < unassignedHubs.Count; i++)
            {
                if (phidgetHubs.Count <= 0)
                    return;
                
                unassignedHubs[i].Initialise(phidgetHubs[i]);
            }
        }
        
        private List<Phidget22.Hub> CreateHubs()
        {
            var phidgetHubs = new List<Phidget22.Hub>();
            for (int i = 0; i < hubs.Count; i++)
            {
                var phidgetHub = InitialiseHub();
                if (phidgetHub == null)
                    continue;
                phidgetHubs.Add(phidgetHub);
            }

            return phidgetHubs;
        }

        private Phidget22.Hub InitialiseHub()
        {
            try
            {
                var hub = new Phidget22.Hub();
                hub.Attach += OnAttachHandler;
                hub.Open(500);
                return hub;
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to add hub: {e}");
            }

            return null;
        }
        
        private void InitialisePhidgets()
        {
            foreach (var hub in hubs)
            {
                hub.InitialiseInputs();
            }
        }
        
        private Hub FindHub(int serialNumber)
        {
            var hub = hubs.Find(x => x.serialNumber == serialNumber);
            if (hub == null)
            {
                Debug.Log($"Could not find Hub: {serialNumber}");
            }

            return hub;
        }
        
        private void OnAttachHandler(object sender, AttachEventArgs e)
        {
            Debug.Log($"<color=green>Hub:</color> attached {sender}");
        }

        private void CloseHubs()
        {
            foreach (var hub in hubs)
            {
                hub.Close();
            }
        
            if (Application.isEditor)
            {
                Phidget.ResetLibrary();
            }
            else
            {
                Phidget.FinalizeLibrary(0);
            }
        }
    }
}