using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadline : MonoBehaviour
{
    public delegate void OnBallHitDeadline(Ball obj);
    public event OnBallHitDeadline OnBallHitDedlineEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball)
            OnBallHitDedlineEvent?.Invoke(ball);

    }
    
}
