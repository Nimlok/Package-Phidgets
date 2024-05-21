using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Phidget22;
using Phidgets;

namespace PhidgetControls
{
    public class PhidgetController : MonoBehaviour
    {
        [SerializeField]private List<PhidgetBaseObject> IndividualPhidgets = new List<PhidgetBaseObject>();
        [SerializeField] private List<PhidgetHubObject> hubs = new List<PhidgetHubObject>();
        
        private const string resourcesPath = "Phidgets";
        
        #region  UNITY functions
        private void OnApplicationQuit()
        {
            foreach (var hub in hubs)
            {
                hub.Close();
            }

            foreach (var phidget in IndividualPhidgets)
            {
                phidget.ClosePhidget();
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
            InitialiseHubs();
        }
        #endregion

        private void LoadHubs()
        {
            if (hubs.Count > 0)
                return;
            hubs = Resources.LoadAll<PhidgetHubObject>(resourcesPath).ToList();
            Debug.Log($"Loaded Hubs: {hubs.Count}");
        }
        
        private void InitialiseHubs()
        {
            if (hubs.Count <= 0)
                return;
            
            foreach (var hub in hubs)
            {
                hub.Initialise();
            }
        }

        private void InitialisePhidgets()
        {
            if (IndividualPhidgets.Count <= 0)
                return;

            foreach (var phidget in IndividualPhidgets)
            {
                phidget.InitialisePhidget();
            }
        }
    }
}