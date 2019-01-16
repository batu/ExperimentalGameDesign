using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void TransitionToLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevelIndex++;
        StartCoroutine(FadeInFadeOut.TransitionLevel(false, currentLevelIndex));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInFadeOut.TransitionLevel(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
