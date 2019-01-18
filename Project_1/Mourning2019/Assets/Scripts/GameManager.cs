using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public string animationName;
    Animator anim;

    public void CallThisFromButton() {
        anim.Play(animationName);
    }

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) { 
            Destroy(gameObject);
        }
        Cursor.lockState = CursorLockMode.Confined;
        anim = GetComponent<Animator>();
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
