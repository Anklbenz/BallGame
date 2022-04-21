using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BallDestroyer : MonoBehaviour
{
    public delegate void CoinsHandler(Ball obj);
    public event CoinsHandler OnBallDestroyEvent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Ball obj = hit.collider.GetComponent<Ball>();
                if (obj != null)
                {
                    OnBallDestroyEvent?.Invoke(obj);
                    obj.Deactivate();
                }
            }
        }
    }
}
