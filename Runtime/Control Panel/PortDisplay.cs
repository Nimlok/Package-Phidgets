using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Phidgets
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PortDisplay: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI inputTypeText;
        [SerializeField] private TextMeshProUGUI portNumberText;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private GameObject inputValueGroup;
        [SerializeField] private TextMeshProUGUI inputValueText;
        
        
        [SerializeField] private Color initialisedColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color activeColor;
        
        private object currentState;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            inputValueGroup.SetActive(false);
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.enabled = false;
        }

        public void SetDetails(BasePhidget phidgetData)
        {
            inputTypeText.text = phidgetData.PhidgetInputType.ToString();
            portNumberText.text = phidgetData.port.ToString();
            SetColor(phidgetData.GetInitialised ? defaultColor: initialisedColor);

            if (!phidgetData.GetInitialised)
                return;
            
            inputValueGroup.SetActive(true);
            phidgetData.onStateChange += StateTriggered;
        }

        public void SetDetails(int portNumber)
        {
            portNumberText.text = portNumber.ToString();
            SetColor(initialisedColor);
            inputValueGroup.SetActive(false);
            canvasGroup.enabled = true;
        }

        private void SetColor(Color color)
        {
            indicatorImage.color = color;
        }
        
        private void StateTriggered(object state)
        {
            switch (state)
            {
                case bool b:
                    SetColor(b? activeColor: defaultColor);
                    inputValueText.text = b.ToString(CultureInfo.InvariantCulture);
                    break;
                case float f:
                    inputValueText.text = f.ToString("F2");
                    break;
            }
        }
    }
}