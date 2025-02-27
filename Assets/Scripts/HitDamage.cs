using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
   [Header("Fade Settings")]
    public Color fadeColor = Color.red;         // The color to fade to
    public float fadeDuration = 1f;             // Duration of the fade effect
    public float waitBeforeFadeOut = 0.5f;      // Time to wait before fading back to the original color

    private Color originalColor;                // The original color of the object
    private SpriteRenderer spriteRenderer;

    private float fadeTimer = 0f;                // Timer to control the fade duration
    private bool isFadingIn = false;            // Flag to check if fading in
    private bool isFadingOut = false;           // Flag to check if fading out

    private void Start()
    {
        // Get the SpriteRenderer component and store the original color
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Start fading in when collision occurs
        if (!isFadingIn && !isFadingOut)
        {
            isFadingIn = true;
            fadeTimer = 0f;  // Reset fade timer
        }
    }

    private void Update()
    {
        if (isFadingIn)
        {
            FadeIn();
        }
        else if (isFadingOut)
        {
            FadeOut();
        }
    }

    private void FadeIn()
    {
        fadeTimer += Time.deltaTime;  // Increase fade timer based on the frame time

        // Interpolate between the original color and the target fade color
        float lerpFactor = Mathf.Clamp01(fadeTimer / fadeDuration);
        spriteRenderer.color = Color.Lerp(originalColor, fadeColor, lerpFactor);

        // Once the fade-in is complete, start the waiting period
        if (fadeTimer >= fadeDuration)
        {
            isFadingIn = false;  // Stop fading in
            fadeTimer = 0f;      // Reset timer for waiting period
            Invoke("StartFadeOut", waitBeforeFadeOut); // Wait before fading out
        }
    }

    private void StartFadeOut()
    {
        isFadingOut = true;  // Start fading out
    }

    private void FadeOut()
    {
        fadeTimer += Time.deltaTime;  // Increase fade timer for fade out

        // Interpolate between the fade color and the original color
        float lerpFactor = Mathf.Clamp01(fadeTimer / fadeDuration);
        spriteRenderer.color = Color.Lerp(fadeColor, originalColor, lerpFactor);

        // Once the fade-out is complete, stop the effect
        if (fadeTimer >= fadeDuration)
        {
            isFadingOut = false;  // Stop fading out
        }
    }
}