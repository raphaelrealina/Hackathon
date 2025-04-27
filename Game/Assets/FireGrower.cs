using UnityEngine;

public class FireGrower : MonoBehaviour
{
    [SerializeField] private Transform fireTransform;
    [SerializeField] private ParticleSystem fireParticles;
    [SerializeField] private Light fireLight;

    [SerializeField] private float growMultiplier = 1.2f;
    [SerializeField] private float growDuration = 1.0f;

    private bool isGrowing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coal") && !isGrowing)
        {
            Destroy(other.gameObject);
            StartCoroutine(GrowFire());
        }
    }

    private System.Collections.IEnumerator GrowFire()
    {
        isGrowing = true;

        // Store starting values
        Vector3 startScale = fireTransform.localScale;
        Vector3 targetScale = startScale * growMultiplier;

        var mainModule = fireParticles.main;
        float startSize = mainModule.startSizeMultiplier;
        float targetSize = startSize * growMultiplier;

        float startIntensity = fireLight.intensity;
        float targetIntensity = startIntensity * growMultiplier;

        float elapsed = 0f;

        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / growDuration;

            // Smooth interpolation
            fireTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

            var main = fireParticles.main;
            main.startSizeMultiplier = Mathf.Lerp(startSize, targetSize, t);

            fireLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);

            yield return null;
        }

        // Just to be sure we reach the exact target values
        fireTransform.localScale = targetScale;

        var finalMain = fireParticles.main;
        finalMain.startSizeMultiplier = targetSize;

        fireLight.intensity = targetIntensity;

        isGrowing = false;
    }
}
