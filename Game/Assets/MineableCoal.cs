using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableCoal : MonoBehaviour
{
    public int hitsToBreak = 3;
    private int currentHits = 0;

    public GameObject coalPickupPrefab; // 👈 Assign your Coal Pickup Prefab in the Inspector
    public int numberOfDrops = 1;        // 👈 How many pickups to spawn when broken

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("entered function");
        if (collision.gameObject.CompareTag("Pickaxe"))
        {
            currentHits++;

            Debug.Log("Coal Hit! hits: " + currentHits);

            if (currentHits >= hitsToBreak)
            {
                // ✨ Spawn pickups before destroying
                SpawnPickups();
                
                Destroy(gameObject);
            }
        }
    }

    private void SpawnPickups()
    {
        if (coalPickupPrefab == null)
        {
            Debug.LogWarning("No coalPickupPrefab assigned!");
            return;
        }

        for (int i = 0; i < numberOfDrops; i++)
        {
            // Slightly random position around broken coal
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 0.5f;
            spawnPosition.y = transform.position.y; // Keep pickup on ground level

            GameObject pickup = Instantiate(coalPickupPrefab, spawnPosition, Quaternion.identity);

            // Optional: Add a little pop force so pickups bounce a bit
            Rigidbody rb = pickup.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
            }
        }
    }
}
