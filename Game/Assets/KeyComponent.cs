using UnityEngine;

public class KeyController : MonoBehaviour
{
    [Header("Effects")]
    public AudioClip pickupSound;
    public ParticleSystem pickupEffect;
    
    [Header("Objects to Disable")]
    public bool useTags = true; // Toggle between tag or manual assignment
    public GameObject[] specificDoors; // Manually assign if not using tags
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("first");
        /*if (collision.gameObject.CompareTag("Lock"))
        {*/
            Debug.Log("sec");
            if (useTags)
            {
                Debug.Log("3");
                // Disable all objects with Door tag
                GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                }
                
                // Disable all objects with Lock tag
                GameObject[] locks = GameObject.FindGameObjectsWithTag("Lock");
                foreach (GameObject lockObj in locks)
                {
                    lockObj.SetActive(false);
                }

                GameObject[] handles = GameObject.FindGameObjectsWithTag("Handle");
                foreach (GameObject handle in handles)
                {
                    handle.SetActive(false);
                }
            }
            else
            {
                // Disable manually assigned objects
                foreach (GameObject obj in specificDoors)
                {
                    obj.SetActive(false);
                }
            }
            
            // Play effects
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
                
            if (pickupEffect != null)
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            
            // Remove key
            Destroy(gameObject);
        }
   // }
}