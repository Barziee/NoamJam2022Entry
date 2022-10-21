using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMiniGame : Minigame
{
   [SerializeField] HitPositionScrollBar hitPositionScrollBar;
    public override void Init(int seconds,int score,int lives, Action onComplete = null)
    {
        base.Init(seconds,score,lives, onComplete);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init(3,0,3, () =>
        {
            hitPositionScrollBar.init();
        });
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
        float targetRandYPos = UnityEngine.Random.Range(0f, 1f);
        hitPositionScrollBar.init(freq: randFreq,targetSize: targetRandSize,targetYPos: targetRandYPos);
    }

    public override void EndGame()
    {
       hitPositionScrollBar.CloseSelf();
       hitPositionScrollBar.gameObject.SetActive(false);
    }
}
