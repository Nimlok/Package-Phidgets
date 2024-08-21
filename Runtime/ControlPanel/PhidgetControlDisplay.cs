using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Nimlok.Phidgets
{
    public class PhidgetControlDisplay : MonoBehaviour
    {
        [SerializeField] private HubDisplay hubDisplayPrefab;
        [SerializeField] private Transform hubParent;
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        
        private PhidgetHubManager phidgetHubManager;
        private List<HubDisplay> hubDisplays = new List<HubDisplay>();
        
        private PhidgetData phidgetData;

        private void OnEnable()
        {
            PhidgetHubManager.OnHubInitialised += CreateHubDisplays;
        }

        private void CreateHubDisplays()
        {
            FindHub();
            
            var hubs = phidgetHubManager.GetHubs;
            if (hubs.Count <= 0)
            {
                InactiveDisplay("No Hubs Attached");
                return;
            }
            
            foreach (var hub in hubs)
            {
                var hubDisplay = Instantiate(hubDisplayPrefab, hubParent);
                hubDisplay.AssignHubData(hub);
                hubDisplays.Add(hubDisplay);
            }
        }

        private void FindHub()
        {
            if (phidgetHubManager != null) return;
            phidgetHubManager = FindObjectOfType<PhidgetHubManager>();

            if (phidgetHubManager != null) 
                return;
            
            Debug.LogWarning("No Phidget Hub Manager Found");
            InactiveDisplay("No Phidget Hub Manager Found");
        }
        
        private void InactiveDisplay(string displayText)
        {
            textMeshProUGUI.text = displayText;
        }
    }
}
