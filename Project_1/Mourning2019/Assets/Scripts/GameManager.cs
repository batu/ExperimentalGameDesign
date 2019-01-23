using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public bool depression = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) { 
            Destroy(gameObject);
        }
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void TransitionToLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevelIndex++;
        StartCoroutine(FadeInFadeOut.TransitionLevel(false, currentLevelIndex));
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!depression)
            StartCoroutine(FadeInFadeOut.TransitionLevel(true));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
