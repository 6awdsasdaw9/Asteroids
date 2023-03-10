using System.Collections.Generic;
using Code.Data;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class HpBar : MonoBehaviour
    {
        private readonly List<GameObject> _hpIcons = new();
    
        [Inject]
        private void Construct(GamePrefabs prefabs, GameConfig config)
        {
            AddHpIcon(config.playerMaxHP, prefabs.hpIcon);
        }

        public void SetValue(int currentHP,int maxHP)
        {
            for (int i = 0; i < maxHP; i++)
            {
                _hpIcons[i].SetActive(i < currentHP);
            }
        }
        private void AddHpIcon(int maxHp, GameObject hpIcon)
        {
            for (int i = 0; i < maxHp; i++)
            {
                GameObject hp = Instantiate(hpIcon);
                hp.transform.SetParent(transform, false);
                _hpIcons.Add(hp);
            }
        }
    }
}