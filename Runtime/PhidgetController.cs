using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Phidget22;
using Phidgets;

namespace PhidgetControls
{
    public class PhidgetController : MonoBehaviour
    {
        private List<PhidgetHubObject> hubs = new List<PhidgetHubObject>();
        private const string resourcesPath = "Phidgets";
        
        #region  UNITY functions
        private void OnApplicationQuit()
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
        
        void Awake()
        {
            LoadHubs();
            Initialise();
        }
        #endregion

        private void LoadHubs()
        {
            if (hubs.Count > 0)
                return;
            hubs = Resources.LoadAll<PhidgetHubObject>(resourcesPath).ToList();
            Debug.Log($"Loaded Hubs: {hubs.Count}");
        }
        
        private void Initialise()
        {
            if (hubs.Count <= 0)
                return;
            
            foreach (var hub in hubs)
            {
                hub.Initialise();
            }
        }
    }
}