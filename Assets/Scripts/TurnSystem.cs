using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TurnPlayer
{
    myTurn,
    oppnentTurn,
};
public class TurnSystem : MonoBehaviour
{
    
    public int turn = 1;
    public TurnPlayer[] players;
    public TurnPlayer turnPlayer;
    private TurnPlayer firstPlayer;
    
    private void Awake()
    {
        int order = Random.Range(0, 1);
        turnPlayer = players[order];
        firstPlayer = players[order];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            turn++;
        }
    }

    private void LateUpdate()
    {
        if (IsOdd(turn) == "even")
        {
            turnPlayer = (TurnPlayer)(turn % 2);
        }
        else
        {
            turnPlayer = (TurnPlayer)(turn % 2);
        }
    }

    string IsOdd(int n)
    {
        return (n & 1) == 0 ? "even" : "odd";
    }

}
