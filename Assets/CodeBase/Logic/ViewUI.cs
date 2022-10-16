using UnityEngine;
using TMPro;

namespace Assets.CodeBase.Data
{
    public class ViewUI : MonoBehaviour
    {
        public TextMeshProUGUI DistanceText;
        public TextMeshProUGUI SpeedText;
        public TextMeshProUGUI TimeText;

        public void UpdateText(SettingsType settingsType, string newValueText)
        {
            switch(settingsType)
            {
                case SettingsType.TIME:
                    TimeText.text = newValueText;
                break;

                case SettingsType.SPEED:
                    SpeedText.text = newValueText;
                    break;

                case SettingsType.DISTANCE:
                    DistanceText.text = newValueText;
                    break;

                default:
                    throw new System.ArgumentException("Type of setting isn't processed");
            }
        }
    }
}
