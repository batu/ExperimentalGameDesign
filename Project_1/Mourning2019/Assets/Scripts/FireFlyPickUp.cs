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

    bool activated = false;
    Collider2D trigger;
    FireflyController _fireflyController;

    public bool inDepression = false;
    public SpriteRenderer wall;
    public GameObject other1 = null;
    public GameObject other2 = null;

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
        print("firefly activated.");
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

    IEnumerator StartDepressionPickUpSequence() {
        StartCoroutine(FadeInCage());
        yield return new WaitForSeconds(0.5f);
        other1.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        other2.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        other1.GetComponent<Collider2D>().enabled = false;
        other2.GetComponent<Collider2D>().enabled = false;
        if (fireflyPickup == Firefly.Denial) {
            for (float i = 1; i >= 0; i -= Time.deltaTime / 2) {
                // set color with i as alpha
                wall.color = new Color(i, i, 1, 1);
                yield return null;
            }
        }
        if (fireflyPickup == Firefly.Anger) {
            for (float i = 1; i >= 0; i -= Time.deltaTime / 2) {
                // set color with i as alpha
                wall.color = new Color(1, i, i, 1);
                yield return null;
            }
        }
        if (fireflyPickup == Firefly.Bargaining) {
            for (float i = 1; i >= 0; i -= Time.deltaTime / 2) {
                // set color with i as alpha
                wall.color = new Color(i, 1, i, 1);
                yield return null;
            }
        }
        FadeInFirefly();

    }

    // Update is called once per frame
    void Update()
    {
        if (inDepression && inside && Input.GetKeyDown(KeyCode.E) && !activated) {
            trigger.enabled = false;
            activated = true;
            StartCoroutine(StartDepressionPickUpSequence());
        }

        if (!inDepression && inside && Input.GetKeyDown(KeyCode.E) && !activated) {
            trigger.enabled = false;
            activated = true;
            StartPickUpSequence();
        }
    }
}
