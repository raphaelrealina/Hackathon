using UnityEngine;
using System.Collections;

public class FireGrower : MonoBehaviour
{
    [SerializeField] private Transform fireTransform;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Light fireLight;

    [SerializeField] private float growMultiplier = 1.2f;
    [SerializeField] private float growDuration = 1.0f;
    [SerializeField] private float sustainDuration = 120f; // Fire stays bigger for 2 minutes

    private bool isGrowing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel") && !isGrowing)
        {
            Destroy(other.gameObject); // Destroy the thrown object, not the fire itself
            StartCoroutine(GrowAndSustainFire());
            Debug.Log("Fuel hit fire!");
        }
    }

    private IEnumerator GrowAndSustainFire()
    {
        isGrowing = true;
        Debug.Log("Growing fire...");

        // Store starting values
        Vector3 startScale = fireTransform.localScale;
        Vector3 targetScale = startScale * growMultiplier;

        var mainModule = fireParticles.main;
        float startSize = mainModule.startSizeMultiplier;
        float targetSize = startSize * growMultiplier;

        float startIntensity = fireLight.intensity;
        float targetIntensity = startIntensity * growMultiplier;

        float elapsed = 0f;

        // Grow the fire smoothly
        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growDuration;

            fireTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

            var main = fireParticles.main;
            main.startSizeMultiplier = Mathf.Lerp(startSize, targetSize, t);

            fireLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);

            yield return null;
        }

        // Make sure it fully reaches target values
        fireTransform.localScale = targetScale;

        var finalMain = fireParticles.main;
        finalMain.startSizeMultiplier = targetSize;

        fireLight.intensity = targetIntensity;

        Debug.Log("Fire fully grown. Sustaining fire...");

        // Sustain the bigger fire for 2 minutes
        yield return new WaitForSeconds(sustainDuration);

        Debug.Log("Fire sustain ended.");
        isGrowing = false;
    }
}
