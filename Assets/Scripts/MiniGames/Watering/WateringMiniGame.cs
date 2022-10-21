using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMiniGame : Minigame
{
   [SerializeField] HitPositionScrollBar hitPositionScrollBar;
    public override void Init(int seconds,Action onComplete = null)
    {
        hitPositionScrollBar.playerPressedHit += PlayerTriedToScore;
        base.Init(seconds, onComplete);

    }
    // Start is called before the first frame update
    void Start()
    {
        Init(3, () =>
        {
            Debug.Log("done");
            hitPositionScrollBar.init();
        });
    }

   void PlayerTriedToScore(bool didHit)
    {
        IncreaseScore(5);
        if (!didHit)
        {
            ReduceLife();
        }
    }


    public override void EndGame()
    {
       
       hitPositionScrollBar.CloseSelf();
       hitPositionScrollBar.gameObject.SetActive(false);
    }
}
