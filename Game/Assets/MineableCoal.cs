using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableCoal : MonoBehaviour
{
    public int hitsToBreak = 3;
    private int currentHits = 0;
    private void onTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickaxe"))
        {
            currentHits++;

            Debug.Log("Coat Hit! hits: " + currentHits);

            if (currentHits >= hitsToBreak)
            {
                Destroy(gameObject);
            }
        }
    }
}
