using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyPickUp : MonoBehaviour
{
    public enum Firefly { Denial, Anger, Bargaining, Acceptance };

    public GameObject Tooltip;
    public Firefly fireflyPickup;
    public GameObject cage;

    public ParticleSystem pickUpPS;
    public ParticleSystem indicatorPS;

    bool activated = false;
    Collider2D trigger;
    FireflyController _fireflyController;

    private void Start() {
        _fireflyController = GameObject.Find("Player").GetComponent<FireflyController>();
        trigger = GetComponent<CircleCollider2D>();
    }

    bool inside = false;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.name == "Player") { 
            Tooltip.SetActive(true);
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.transform.name == "Player") { 
            Tooltip.SetActive(false);
            inside = false;
        }
    }

    void StartPickUpSequence() {
        StartCoroutine(FadeInCage());
    }

    IEnumerator FadeInCage() {
        float fadeSpeed = 0.441f;
        FadeOutFireflyObject();
        SpriteRenderer sprite = cage.GetComponent<SpriteRenderer>();
        // fade from opaque to transparent
        sprite.color = new Color(0, 0, 0, 0);
        // loop over 1 second backwards
        for (float i = 0; i <= 1; i += Time.deltaTime * fadeSpeed) {
            // set color with i as alpha
            sprite.color = new Color(0, 0, 0, i);
            yield return null;
        }
        WaitXSeconds(2);
        FadeInFirefly();
    }

    IEnumerator WaitXSeconds(int x) {
        yield return new WaitForSeconds(x);
    }

    void FadeOutFireflyObject() {
        ParticleSystem.MainModule mainMod =  pickUpPS.main;
        mainMod.loop = false;
    }

    void FadeInFirefly() {
        string whichFirefly = null;
        switch (fireflyPickup) {
            case Firefly.Denial:
                whichFirefly = "Denial";
                break;
            case Firefly.Anger:
                whichFirefly = "Anger";
                break;
            case Firefly.Bargaining:
                whichFirefly = "Bargaining";
                break;
            case Firefly.Acceptance:
                whichFirefly = "Acceptance";
                break;
        }
        _fireflyController.ActivateFirefly(whichFirefly);
    }

    // Update is called once per frame
    void Update()
    {
        if (inside && Input.GetKeyDown(KeyCode.E) && !activated) {
            trigger.enabled = false;
            activated = true;
            StartPickUpSequence();
        }
    }
}
