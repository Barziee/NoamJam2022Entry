using System;
using UnityEngine;

public class Spritzer : MonoBehaviour
{
    [SerializeField] 
    private float speed = 500f;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // move left
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // move right
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // shoot water drop
        }
    }
}
