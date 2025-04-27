using UnityEngine;
using System.Collections;

public class FireGrower : MonoBehaviour
{
    [SerializeField] private Transform fireTransform;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Light fireLight;

    [SerializeField] private float growMultiplier = 1.2f;
    [SerializeField] private float growDuration = .10f;
    [SerializeField] private float sustainDuration = .15f; // Fire stays bigger for 15 seconds
    [SerializeField] private float shrinkDuration = .10f; // How long it takes to shrink back

    private bool isGrowing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fuel") && !isGrowing)
        {
            Destroy(other.gameObject);
            StartCoroutine(GrowSustainShrinkFire());
            Debug.Log("Fuel hit fire!");
        }
    }

    private IEnumerator GrowSustainShrinkFire()
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

        // Grow phase
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

        // Snap to target to make sure no drift
        fireTransform.localScale = targetScale;
        var finalMain = fireParticles.main;
        finalMain.startSizeMultiplier = targetSize;
        fireLight.intensity = targetIntensity;

        Debug.Log("Fire fully grown. Sustaining fire...");

        // Sustain phase
        yield return new WaitForSeconds(sustainDuration);

        Debug.Log("Fire sustain ended. Shrinking fire...");

        // Shrink phase
        elapsed = 0f;
        while (elapsed < shrinkDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / shrinkDuration;

            fireTransform.localScale = Vector3.Lerp(targetScale, startScale, t);

            var main = fireParticles.main;
            main.startSizeMultiplier = Mathf.Lerp(targetSize, startSize, t);

            fireLight.intensity = Mathf.Lerp(targetIntensity, startIntensity, t);

            yield return null;
        }

        // Final snap back to original values
        fireTransform.localScale = startScale;
        var originalMain = fireParticles.main;
        originalMain.startSizeMultiplier = startSize;
        fireLight.intensity = startIntensity;

        isGrowing = false;
        Debug.Log("Fire returned to normal size.");
    }
}
