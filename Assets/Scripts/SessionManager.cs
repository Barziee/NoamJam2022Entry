using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    [SerializeField] AudioClip openMiniGame;

    const int TIMER_COUNTDOWN_SECONDS=3;
    const int PLAYER_MINIGAME_LIVES = 3;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.MasterVolume = 1;
        SoundManager.Instance.MusicVolume = 1;
        SoundManager.Instance.EffectsVolume = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MiniGameClicked(Minigame miniGame)
    {
        //barzie anim
        SoundManager.Instance.PlayAudioEffectOnce(openMiniGame);
        miniGame.gameObject.SetActive(true);
        miniGame.Init(TIMER_COUNTDOWN_SECONDS,0, PLAYER_MINIGAME_LIVES,OnCountdownTimerEnded, OnMiniGameEnded);
    }

    void OnCountdownTimerEnded() { }

     void OnMiniGameEnded(Minigame miniGame)
    {
        miniGame.CloseSelf();
    }


}
