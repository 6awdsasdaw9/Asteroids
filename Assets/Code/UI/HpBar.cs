using System.Collections;
using System.Collections.Generic;
using Code.Data;
using UnityEngine;
using Zenject;

public class HpBar : MonoBehaviour
{
    
    private List<GameObject> hpIcons = new();
    

    [Inject]
    private void Construct(GamePrefabs prefabs, GameConfig config)
    {
     AddHpIcon(config.playerMaxHP,prefabs.hpIcon);
        
    }
    
    private void AddHpIcon(byte maxHp,GameObject hpIcon)
    {
        for (byte i = 0; i < maxHp; i++)
        {
            GameObject hp = Instantiate(hpIcon);
            hp.transform.SetParent(transform,false);
            hpIcons.Add(hp);
        }
    }

}
