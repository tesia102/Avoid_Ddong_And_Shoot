using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    GameObject obj;

    private void Start()
    {
        obj = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            if(obj != null)
            {
                obj.GetComponent<Player>().AvoidCount++;
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Death();
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
