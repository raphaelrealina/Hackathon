using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableCoal : MonoBehaviour
{
    public int hitsToBreak = 3;
    private int currentHits = 0;
    private void onCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickaxe"))
        {
            currentHits++;

            Debug.Log("Coal Hit! hits: " + currentHits);

            if (currentHits >= hitsToBreak)
            {
                Destroy(gameObject);
            }
        }
        Debug.Log("entered function");
    }
}
