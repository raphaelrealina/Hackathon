using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableCoal : MonoBehaviour
{
    public int hitsToBreak = 3;
    private int currentHits = 0;
    // Start is called before the first frame update

    private void onCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickaxe"))
        {
            currentHits++;

            Debug.Log("Coat hit! Hits: " + currentHits);

            if (currentHits >= hitsToBreak)
            {
                Destroy(gameObject);
            }
        }
    }
}
