using System;
using System.Collections;
using Game.Core;
using UnityEngine;

public static class AnimatorUtility
{
    
    public static void Blink(SpriteRenderer spriteRenderer, float blinkDuration, float blinkInterval)
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is null. Cannot apply blink effect.");
            return;
        }
        
        GameManager.Instance.StartCoroutine(BlinkCoroutine(spriteRenderer, blinkDuration, blinkInterval));
    }

    private static IEnumerator BlinkCoroutine(SpriteRenderer spriteRenderer, float duration, float interval)
    {
        float elapsedTime = 0f;
        bool isDim = false; 

        Color originalColor = Color.white; 
        Color dimColor = originalColor; 
        dimColor.a = 0.2f; 

        while (elapsedTime < duration)
        {
            isDim = !isDim;
            spriteRenderer.color = isDim ? dimColor : originalColor;
            
            yield return new WaitForSeconds(interval);
            elapsedTime += interval;
        }
        
        spriteRenderer.color = originalColor;
        
    }
}