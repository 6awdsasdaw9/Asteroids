using UnityEngine;

namespace Code.UI
{
    public class UIDisplay : MonoBehaviour
    {
        public FuelBar fuelBar;
        public HpBar hpBar;
        public SuperBulletCooldownBar superBulletCooldownBar;
        public AliensTimer aliensTimer;
        public GameObject LosePanel;

        public void ShowLosePanel()
        {
            LosePanel.SetActive(true);
        }

    }
}
