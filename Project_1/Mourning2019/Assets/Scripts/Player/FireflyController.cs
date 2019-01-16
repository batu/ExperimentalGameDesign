using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    enum FireflyState { None, Denial, Anger, Bargaining, Depression, Acceptance };

    Color denialColor = Color.blue;
    Color angerColor = Color.red;
    Color bargainingColor = Color.green;
    Color acceptanceColor = Color.white;

    bool denialGained = false;
    bool angerGained = false;
    bool bargainingGained = false;
    bool acceptanceGained = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.transform.name) {
            case "DenialFirefly":
                denialGained = true;
                ChangeFirefly(FireflyState.Denial);
                Destroy(collision.transform.gameObject);
                break;
            case "AngerFirefly":
                angerGained = true;
                ChangeFirefly(FireflyState.Anger);
                Destroy(collision.transform.gameObject);
                break;
            case "BargainingFirefly":
                bargainingGained = true;
                ChangeFirefly(FireflyState.Bargaining);
                Destroy(collision.transform.gameObject);
                break;
            case "AcceptanceFirefly":
                denialGained = true;
                ChangeFirefly(FireflyState.Acceptance);
                Destroy(collision.transform.gameObject);
                break;

        }
    }

    DenialSkill denialSkill;
    AngerSkill angerSkill;
    BargainingSkill bargainingSkill;
    AcceptanceSkill acceptanceSkill;

    GameObject fireflyParticlesGO;
    ParticleSystem firelyParticles;

    Camera mainCamera;

    FireflyState activeFirefly = FireflyState.None;
    // Start is called before the first frame update
    void Start()
    {
        fireflyParticlesGO = GameObject.FindGameObjectWithTag("FireFlyParticles");
        firelyParticles = fireflyParticlesGO.GetComponent<ParticleSystem>();

        denialSkill = GetComponent<DenialSkill>();
        angerSkill = GetComponent<AngerSkill>();
        bargainingSkill = GetComponent<BargainingSkill>();
        acceptanceSkill = GetComponent<AcceptanceSkill>();

        mainCamera = Camera.main;

        ChangeFirefly(FireflyState.None);
    }


    void MoveFireflyParticles() {
        
        Vector3 worldMousePoint = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                                              mainCamera.ScreenToWorldPoint(Input.mousePosition).y,
                                              0);

        fireflyParticlesGO.transform.position = worldMousePoint;
    }

    void ChangeFireflyInputDetector() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && denialGained) {
            ChangeFirefly(FireflyState.Denial);
            print("Changed the state to Denial.");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && angerGained) {
            ChangeFirefly(FireflyState.Anger);
            print("Changed the state to Anger.");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && bargainingGained) {
            ChangeFirefly(FireflyState.Bargaining);
            print("Changed the state to Bargaining.");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && acceptanceGained) {
            ChangeFirefly(FireflyState.Acceptance);
            print("Changed the state to Bargaining.");
        }
    }
    void ChangeFirefly(FireflyState targetState) {
        var main = firelyParticles.main;
        switch (targetState) {
            case FireflyState.None:
                DisableAllSkills();
                main.startColor = new Color(0,0,0,0);
                break;
            case FireflyState.Denial:
                DisableAllSkills();
                main.startColor = denialColor;
                denialSkill.enabled = true;
                break;
            case FireflyState.Anger:
                DisableAllSkills();
                main.startColor = angerColor;
                angerSkill.enabled = true;
                break;
            case FireflyState.Bargaining:
                DisableAllSkills();
                main.startColor = bargainingColor;
                bargainingSkill.enabled = true;
                break;
            case FireflyState.Acceptance:
                DisableAllSkills();
                main.startColor = acceptanceColor;
                acceptanceSkill.enabled = true;
                break;
        }
    }

    void DisableAllSkills() {
        denialSkill.enabled = false;
        angerSkill.enabled = false;
        bargainingSkill.enabled = false;
        acceptanceSkill.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveFireflyParticles();
        ChangeFireflyInputDetector();
    }
}
