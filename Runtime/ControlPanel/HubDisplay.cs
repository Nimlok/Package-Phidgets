using System.Linq;
using TMPro;
using UnityEngine;

namespace Phidgets
{
    public class HubDisplay: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI serialTextMesh;
        [SerializeField] private PortDisplay[] ports = new PortDisplay[6];
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.enabled = false;
        }

        public void AssignHubData(Hub hub)
        {
            var hubPorts = hub.GetPorts.ToList();
            serialTextMesh.text = hub.serialNumber.ToString();

            for (int i = 0; i < hubPorts.Count; i++)
            {
                var phidget = hubPorts[i];
                if (phidget == null)
                {
                    ports[i].SetDetails(i);
                    continue;
                }
           
                ports[i].SetDetails(phidget);
            }

            if (!hub.Initialised) 
                canvasGroup.enabled = true;
        }
    }
}