using System;
using Code.Player;
using Code.UI;
using UnityEngine;
using Zenject;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerHp _hp;
    [SerializeField] private PlayerMove _move;
    [SerializeField] private PlayerAttack _attack;
    private Action OnDeath;

    [Inject]
    private void Construct(UIDisplay uiDisplay)
    {
        _hp.OnStatChanged += Death;
        OnDeath += uiDisplay.ShowLosePanel;
    }

    private void Death()
    {
        if (_hp.Current > 0) 
            return;
        
        _hp.enabled = false;
        _move.enabled = false;
        _attack.enabled = false;
        OnDeath?.Invoke();
    }
}