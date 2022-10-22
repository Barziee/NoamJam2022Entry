using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMiniGame : Minigame
{
   [SerializeField] HitPositionScrollBar hitPositionScrollBar;
    public override void Init(int seconds,int score,int lives, Action onTimerDone = null,Action<Minigame> OnGameEnded=null)
    {
        base.Init(seconds,score,lives,
            () =>
            {
                hitPositionScrollBar.init();
                onTimerDone?.Invoke();

            }, OnGameEnded);
    }

    private void OnEnable()
    {
        hitPositionScrollBar.playerPressedHit += PlayerTriedToScore;
    }
    private void OnDisable()
    {
        hitPositionScrollBar.playerPressedHit -= PlayerTriedToScore;
    }

    void PlayerTriedToScore(bool didHit)
    {
        Debug.Log($"player tried to hit and got {didHit}");
        if (!didHit)
        {
            ReduceLife();
        }
        else
        {
            IncreaseScore(5);
        }
        setNextLevelHitBar();
    }

    private void setNextLevelHitBar()
    {
      float randFreq =  UnityEngine.Random.Range(1, 5);
     
      float targetRandSize = UnityEngine.Random.Range(0.05f, 0.5f);
        float halfSize = targetRandSize/2;
        float targetRandYPos = UnityEngine.Random.Range(halfSize, 1- halfSize);
        hitPositionScrollBar.CreateNewBar(freq: randFreq,targetSize: targetRandSize,targetYPos: targetRandYPos);
    }

    public override void EndGame()
    {
        base.EndGame();
       hitPositionScrollBar.CloseSelf();
    }


    public override void CloseSelf()
    {
        base.CloseSelf();
    }
}
