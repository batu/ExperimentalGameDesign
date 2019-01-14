using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    enum FireflyState { None, Denial, Anger, Bargaining, Depression, Acceptance };

    Color denialColor = Color.blue;
    Color angerColor = Color.red;
    Color bargainingColor = Color.green;

    DenialSkill denialSkill;
    AngerSkill angerSkill;
    BargainingSkill bargainingSkill;

    GameObject fireflyParticlesGO;
    ParticleSystem firelyParticles;

    Camera mainCamera;

    FireflyState activeFirefly = FireflyState.Denial;
    // Start is called before the first frame update
    void Start()
    {
        fireflyParticlesGO = GameObject.FindGameObjectWithTag("FireFlyParticles");
        firelyParticles = fireflyParticlesGO.GetComponent<ParticleSystem>();

        denialSkill = GetComponent<DenialSkill>();
        angerSkill = GetComponent<AngerSkill>();
        bargainingSkill = GetComponent<BargainingSkill>();

        mainCamera = Camera.main;

        ChangeFirefly(FireflyState.Denial);
    }


    void MoveFireflyParticles() {
        
        Vector3 worldMousePoint = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                                              mainCamera.ScreenToWorldPoint(Input.mousePosition).y,
                                              0);

        fireflyParticlesGO.transform.position = worldMousePoint;
    }

    void ChangeFireflyInputDetector() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeFirefly(FireflyState.Denial);
            print("Changed the state to Denial.");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeFirefly(FireflyState.Anger);
            print("Changed the state to Anger.");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeFirefly(FireflyState.Bargaining);
            print("Changed the state to Bargaining.");
        }
    }
    void ChangeFirefly(FireflyState targetState) {
        var main = firelyParticles.main;
        switch (targetState) {
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
            case FireflyState.None:
                DisableAllSkills();
                break;
        }
    }

    void DisableAllSkills() {
        denialSkill.enabled = false;
        angerSkill.enabled = false;
        bargainingSkill.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveFireflyParticles();
        ChangeFireflyInputDetector();
    }
}
