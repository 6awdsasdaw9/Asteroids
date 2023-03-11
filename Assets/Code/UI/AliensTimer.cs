using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class AliensTimer: MonoBehaviour
    {
        [SerializeField] private Text textTimer;

        public void UpdateTimerText(float cooldown) => 
            textTimer.text = Mathf.Round(cooldown).ToString();
    }
}