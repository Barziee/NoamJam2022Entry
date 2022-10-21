using System.Collections.Generic;
using UnityEngine;

public class InsectInvaders : Minigame
{
        private List<Insect> insectList;

        [SerializeField] private GameObject playerObject;

        public override void Init(int seconds)
        {
                base.Init(seconds);
                playerObject.SetActive(true);
        }

        public void EndGame()
        {
                playerObject.SetActive(false);
        }
}
