using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenialSkill : MonoBehaviour
{

    enum SkillStatus { None, ButtonDown, ButtonUp };

    public GameObject barrierPrefab;
    public float minimumBarrierLenght = 0.75f;
    //    public float maximumBarrierLength;
    public int maximumBarierCount = 3;

    Queue<GameObject> barriers= new Queue<GameObject>();

    Camera mainCamera;

    private LineRenderer line;                           // Line Renderer
    // Start is called before the first frame update
    void Start() {
        mainCamera = Camera.main;
        // Add a Line Renderer to the GameObject
        InitializeLineRenderer();
    }

    private void InitializeLineRenderer() {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.05F;
        line.endWidth = 0.05F;
        line.positionCount = 2;
        line.material = new Material(Shader.Find("Unlit/Color")) {
            color = Color.white
        };
    }

    Vector3 denialStartPosition = Vector3.zero;
    Vector3 denialEndPosition = Vector3.zero;
    Vector3 worldClickPoint;

    // Check if the 
    SkillStatus currentMouseStatus = SkillStatus.None;
    void CastDenial() {
        if (Input.GetButton("Fire2")) {
            currentMouseStatus = SkillStatus.None;
        }

        // Get casting starting point.
        if (Input.GetButtonDown("Fire1") && currentMouseStatus == SkillStatus.None) {
            worldClickPoint = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                                          mainCamera.ScreenToWorldPoint(Input.mousePosition).y,
                                          0);

            denialStartPosition = worldClickPoint;
            line.SetPosition(0, denialStartPosition);
            currentMouseStatus = SkillStatus.ButtonDown;
        }

        if(currentMouseStatus == SkillStatus.ButtonDown) {
            worldClickPoint = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                              mainCamera.ScreenToWorldPoint(Input.mousePosition).y,
                              0);
            line.SetPosition(1, worldClickPoint);
            if(CheckBarrierObstructionAvailablity(denialStartPosition, worldClickPoint)) {
                line.material.color = Color.blue;
            } else {
                line.material.color = Color.red;
            }

        }

        // Get casting ending point.
        if (Input.GetButtonUp("Fire1") && currentMouseStatus == SkillStatus.ButtonDown) {
            worldClickPoint = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                                          mainCamera.ScreenToWorldPoint(Input.mousePosition).y,
                                          0);

            denialEndPosition = worldClickPoint;
            currentMouseStatus = SkillStatus.ButtonUp;
        }

        
        if (currentMouseStatus == SkillStatus.ButtonUp) {
            if (CheckBarrierDistanceAvailablity(denialStartPosition, denialEndPosition) && CheckBarrierObstructionAvailablity(denialStartPosition, denialEndPosition)) {
                SpawnBarrier(denialStartPosition, denialEndPosition);
            }
            currentMouseStatus = SkillStatus.None;
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);

        }
    }

    bool CheckBarrierDistanceAvailablity(Vector3 startPosition, Vector3 endPosition) {

        float distance = Vector3.Distance(endPosition, startPosition);
        bool distanceAcceptable = distance > minimumBarrierLenght;
        Vector3 direction = (endPosition - startPosition).normalized;

        return distanceAcceptable;
    }

    bool CheckBarrierObstructionAvailablity(Vector3 startPosition, Vector3 endPosition) {
        float distance = Vector3.Distance(endPosition, startPosition);
        Vector3 direction = (endPosition - startPosition).normalized;
    
        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, distance);
        bool obstructionAcceptable = hit.collider == null;

        return obstructionAcceptable;
    }


    void SpawnBarrier(Vector3 startPosition, Vector3 endPosition) {
        Vector3 midPoint = (startPosition + endPosition) / 2;
        GameObject barrier = Instantiate(barrierPrefab, midPoint, Quaternion.identity) as GameObject;

        Vector3 direction = (endPosition - startPosition).normalized;
        float z_Value = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        barrier.transform.rotation = Quaternion.Euler(new Vector3(0, 0, z_Value));

        float distance = Vector3.Distance(endPosition, startPosition);
        barrier.transform.localScale = new Vector3(distance, barrier.transform.localScale.y, barrier.transform.localScale.z);
        barriers.Enqueue(barrier);

        if(barriers.Count > maximumBarierCount) {
            Destroy(barriers.Dequeue());
        }
    }


    private void OnDisable() {
        foreach (GameObject node in barriers) {
            Destroy(node);
        }
        barriers.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        CastDenial();
    }
}
