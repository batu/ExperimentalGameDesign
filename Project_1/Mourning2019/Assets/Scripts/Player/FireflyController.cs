using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    public enum FireflyState { None, Denial, Anger, Bargaining, Depression, Acceptance };

    Color denialColor = new Color(0.1741573f, 0.8314608f, 1f, 1f);
    Color angerColor = Color.red;
    Color bargainingColor = Color.green;
    Color acceptanceColor = Color.white;

    GameObject denialCam;
    GameObject angerCam;
    GameObject bargainingCam;
    GameObject nullCam;

    public FireflyState startFireflyState = FireflyState.None; 

    [HideInInspector]
    public bool denialGained = false;
    [HideInInspector]
    public bool angerGained = false;
    [HideInInspector]
    public bool bargainingGained = false;
    bool acceptanceGained = false;

    public void ActivateFirefly(string firefly) {
        print("In this shit!");

        switch (firefly) {
            case "Denial":
                print("Deninal activated");
                denialGained = true;
                ChangeFirefly(FireflyState.Denial);
                break;
            case "Anger":
                angerGained = true;
                ChangeFirefly(FireflyState.Anger);
                break;
            case "Bargaining":
                bargainingGained = true;
                ChangeFirefly(FireflyState.Bargaining);
                break;
            case "Acceptance":
                acceptanceGained = true;
                ChangeFirefly(FireflyState.Acceptance);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
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

        denialCam = GameObject.FindGameObjectWithTag("DenialCamera");
        angerCam = GameObject.FindGameObjectWithTag("AngerCamera");
        bargainingCam = GameObject.FindGameObjectWithTag("BargainingCamera");
        nullCam = GameObject.Find("Null Cam");
        mainCamera = Camera.main;

        switch (startFireflyState) {
            case FireflyState.None:
                ChangeFirefly(FireflyState.None);
                break;
            case FireflyState.Denial:
                ChangeFirefly(FireflyState.Denial);
                break;
            case FireflyState.Anger:
                ChangeFirefly(FireflyState.Anger);
                break;
            case FireflyState.Bargaining:
                ChangeFirefly(FireflyState.Bargaining);
                break;
            case FireflyState.Acceptance:
                ChangeFirefly(FireflyState.Acceptance);
                break;
        }
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
        DisableAllSkills();
        DisableAllCameras();
        switch (targetState) {
            case FireflyState.None:
                nullCam.SetActive(true);
                main.startColor = new Color(0,0,0,0);
                break;
            case FireflyState.Denial:
                denialCam.SetActive(true);
                main.startColor = denialColor;
                denialSkill.enabled = true;
                break;
            case FireflyState.Anger:
                angerCam.SetActive(true);
                main.startColor = angerColor;
                angerSkill.enabled = true;
                break;
            case FireflyState.Bargaining:
                bargainingCam.SetActive(true);
                main.startColor = bargainingColor;
                bargainingSkill.enabled = true;
                break;
            case FireflyState.Acceptance:
                main.startColor = acceptanceColor;
                acceptanceSkill.enabled = true;
                break;
        }
        firelyParticles.Play();
    }

    void DisableAllSkills() {
        denialSkill.enabled = false;
        angerSkill.enabled = false;
        bargainingSkill.enabled = false;
        acceptanceSkill.enabled = false;
    }

    void DisableAllCameras() {
        if(nullCam)
            nullCam.SetActive(false);
        if (denialCam)
            denialCam.SetActive(false);
        if (angerCam)
            angerCam.SetActive(false);
        if (bargainingCam)
            bargainingCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoveFireflyParticles();
        ChangeFireflyInputDetector();
    }
}
