using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBranch : Minigame
{
    [SerializeField] StayOnPositionScrollBar stayOnPositionScrollBar;

    public override void Init(int seconds, int score, int lives, Action onTimerDone = null, Action<Minigame, int> OnGameEnded = null)
    {
        base.Init(seconds, score, lives,
            () =>
            {
                stayOnPositionScrollBar.Init();
                onTimerDone?.Invoke();

            }, OnGameEnded);
    }

    public override int GetMiniGameType()
    {
        return 1;
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
        ResetOnPositionBar();
    }

    void ResetOnPositionBar()
    {
        stayOnPositionScrollBar.ResetSelf();
    }

    public override void EndGame()
    {
        base.EndGame();
        stayOnPositionScrollBar.CloseSelf();
    }

    public override void CloseSelf()
    {
        base.CloseSelf();
    }
}
