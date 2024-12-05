using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch(transform.tag)
        {
            case "Ground":
                float diffY = playerPos.y - myPos.y;
                float diffX = playerPos.x - myPos.x;

                float dirX = diffY < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffY = Mathf.Abs(diffY);
                diffX = Mathf.Abs(diffX);
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if(diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            case "Enemy":
                if(coll.enabled)
                {
                    Vector3 distance = playerPos - myPos;
                    Vector3 random = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
                    transform.Translate(random + distance * 2);
                }
                break;
        }
    }
}
