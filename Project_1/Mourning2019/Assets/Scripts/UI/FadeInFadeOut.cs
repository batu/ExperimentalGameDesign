using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class FadeInFadeOut
{
    public static IEnumerator TransitionLevel(bool fadeAway, int levelIndex=0) {
        float fadeSpeed = 0.441f;

        GameObject obstructionPlane = GameObject.FindGameObjectWithTag("Obstruction");
        SpriteRenderer sprite = obstructionPlane.GetComponent<SpriteRenderer>();
        // fade from opaque to transparent
        if (fadeAway) {
            sprite.color = new Color(0, 0, 0, 1);
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * fadeSpeed) {
                // set color with i as alpha
                sprite.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else {
            sprite.color = new Color(0, 0, 0, 0);
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * fadeSpeed) {
                // set color with i as alpha
                sprite.color = new Color(0, 0, 0, i);
                yield return null;
            }
            SceneManager.LoadScene(levelIndex);
        }
    }
}
 
