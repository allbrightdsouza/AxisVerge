using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCollision : MonoBehaviour
{
    bool triggered;
    PlayerMove playerMove;
    
    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("projectile") && !triggered)
        {
            triggered = true;
            HighScore.Instance.GameOver();
            //Do Somthing On Trigger
        }

        if(other.CompareTag("pickup"))
        {
            SpeedUpSpawner.Instance.count -= 1;
            Destroy(other.gameObject);
            if (playerMove.lerpTimeInterval <= 0.2f)
                playerMove.lerpTimeInterval = 0.2f;
            else if (playerMove.lerpTimeInterval <= 1.0f)
                playerMove.lerpTimeInterval -= 0.1f;
            else if (playerMove.lerpTimeInterval <= 1.6f)
                playerMove.lerpTimeInterval -= 0.2f;
            else
                playerMove.lerpTimeInterval -= 0.3f;
            HighScore.Instance.AddScore(20);
        }



    }
}
