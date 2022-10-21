using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{

    const int TIMER_COUNTDOWN_SECONDS=3;
    const int PLAYER_MINIGAME_LIVES = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MiniGameClicked(Minigame miniGame)
    {
        miniGame.Init(TIMER_COUNTDOWN_SECONDS,0, PLAYER_MINIGAME_LIVES,OnCountdownTimerEnded, OnMiniGameEnded);
    }

    void OnCountdownTimerEnded() { }

     void OnMiniGameEnded(Minigame miniGame)
    {
        miniGame.Init(TIMER_COUNTDOWN_SECONDS,0, PLAYER_MINIGAME_LIVES,OnCountdownTimerEnded, OnMiniGameEnded);

    }


}
