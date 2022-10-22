using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBranch : Minigame
{
    [SerializeField] StayOnPositionScrollBar stayOnPositionScrollBar;
    [SerializeField] int amountOfRounds = 3;

    public override void Init(int seconds, int score, int lives, Action onTimerDone = null, Action<Minigame, int> OnGameEnded = null)
    {
        base.Init(seconds, score, lives,
            () =>
            {
                stayOnPositionScrollBar.Init();
                onTimerDone?.Invoke();

            }, OnGameEnded);
    }


    private void OnEnable()
    {
        stayOnPositionScrollBar.OnRoundEnd += GameActionHappened;
    }
    private void OnDisable()
    {
        stayOnPositionScrollBar.OnRoundEnd -= GameActionHappened;
    }

    //if the player lost a round or won a round
    void GameActionHappened(bool didWin)
    {
    
        if (!didWin)
        {
            ReduceLife();
        }
        else
        {
            IncreaseScore(5);
        }
        amountOfRounds--;

        if (amountOfRounds == 0)
        {
            EndGame();
            return;
        }

        ResetOnPositionBar();
    }

    void ResetOnPositionBar()
    {
        stayOnPositionScrollBar.ResetSelf();
    }

    public override void EndGame()
    {
        amountOfRounds = 3;
        base.EndGame();
        stayOnPositionScrollBar.CloseSelf();
    }

    public override void CloseSelf()
    {
        base.CloseSelf();
    }
}
