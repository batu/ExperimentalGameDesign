using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TriggerNextLevel : MonoBehaviour
{

    public int specificLevel = -1;
    public bool keyMoment = false;

    FireflyController fireflyController;

    public void TransitionToLevel() {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevelIndex++;
        StartCoroutine(FadeInFadeOut.TransitionLevel(false, currentLevelIndex));
    }

    List<int> levels = new List<int>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (keyMoment && collision.gameObject.name == "Player") {
            if (fireflyController.denialGained) {
                StartCoroutine(FadeInFadeOut.TransitionLevel(false, 6));
            }else if (fireflyController.angerGained) {
                StartCoroutine(FadeInFadeOut.TransitionLevel(false, 7));
            }else if (fireflyController.bargainingGained) {
                StartCoroutine(FadeInFadeOut.TransitionLevel(false, 8));
            }else if (!fireflyController.denialGained && !fireflyController.angerGained && !fireflyController.bargainingGained) {
                StartCoroutine(FadeInFadeOut.TransitionLevel(false, 5));
            }
        }
        if(specificLevel != -1 && collision.gameObject.name == "Player") {
            StartCoroutine(FadeInFadeOut.TransitionLevel(false, specificLevel));
        }else if (!keyMoment && collision.gameObject.name == "Player") {
            TransitionToLevel();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        fireflyController = GameObject.Find("Player").GetComponent<FireflyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
