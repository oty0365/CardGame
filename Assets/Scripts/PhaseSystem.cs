using System;
using Unity.VisualScripting;
using UnityEngine;

public enum Phase
{
    TurnPhase,
    MainPhase,
    BattlePhase,
    MainPhase2,
    EndPhase,
}


public class PhaseSystem : MonoBehaviour
{
    Phase _phase = Phase.TurnPhase;
    Hand _hand;
    private bool _isDraw;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _phase++;
        }
        
        _phase = Phase.MainPhase;

        if (_phase == Phase.TurnPhase && _isDraw == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                for (int i = 0; i < 5; i++)
                {
                    _hand.OnCardDraw();
                    _isDraw = true;
                }
            }
        } 
    }
}
