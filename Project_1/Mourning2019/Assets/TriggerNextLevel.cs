using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerNextLevel : MonoBehaviour
{

    public void TransitionToLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevelIndex++;
        StartCoroutine(FadeInFadeOut.TransitionLevel(false, currentLevelIndex));
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            TransitionToLevel();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
