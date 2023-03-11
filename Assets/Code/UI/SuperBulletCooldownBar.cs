using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class SuperBulletCooldownBar : MonoBehaviour
    {
        [SerializeField] private Image bulletBar;

        public void SetValue(float currentTime, float maxTime)
        {
            bulletBar.fillAmount = currentTime / maxTime;
        }
    }
}