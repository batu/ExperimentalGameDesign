using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptanceSkill : MonoBehaviour
{
    enum SkillStatus { None, ButtonDown, ButtonUp };

    List<GameObject> nodesList = new List<GameObject>();
    public GameObject acceptanceNode;

    public float nodeSwitchDistance = 0.1f;
    public float nodeEscapeDistance = 1f;
    public float nextNodeDistance = 0.75f; 
    public float tripTime = 2f;
    public int maximumNodes;

    Camera mainCamera;
    GameObject activeNode = null;
    int activeNodeIndex = -5;
    float initialGravityScale = 3f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialGravityScale = rb.gravityScale;
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Node" && activeNode == null) {
            ActivateNode(collision.gameObject);
            print(nodesList.IndexOf(collision.gameObject) + " Printing the index of collision");
        }
    }

    float startTime;
    void ActivateNode(GameObject node) {
        activeNodeIndex = nodesList.IndexOf(node);
        activeNode = node;
        rb.gravityScale = 0;
        rb.velocity = rb.velocity / 10;
        startTime = Time.time;
    }

    void NodePull() {
        if(activeNode != null) { 
            Vector3 nextNodePosition = activeNode.transform.position;
            float currentPercentage = (Time.time - startTime) / (tripTime * 100 * ((activeNodeIndex+1)/3f));
            if ((transform.position - nextNodePosition).sqrMagnitude > nodeSwitchDistance * nodeSwitchDistance) {
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, nextNodePosition, currentPercentage);
                transform.position = smoothedPosition;
            } else {
                SwitchToNextNode();
            }
        }
    }

    void SwitchToNextNode() {
        if (activeNodeIndex < nodesList.Count && activeNodeIndex != -1) {
            activeNode = nodesList[activeNodeIndex];
            activeNodeIndex++;
        }
    }

    void DetachFromAcceptance() {
        if (activeNode != null && (transform.position - activeNode.transform.position).sqrMagnitude > nodeEscapeDistance * nodeEscapeDistance) {
            Detach();
        }
    }

    SkillStatus currentMouseStatus = SkillStatus.None;
    Vector3 worldClickPoint;
    Vector3 lastNodePlacement;

    void AddNode(Vector3 location) {
        GameObject node = Instantiate(acceptanceNode, location, Quaternion.identity);
        nodesList.Add(node);
    }

    void DeleteAllNodes() {
        foreach(GameObject node in nodesList) {
            Destroy(node);
        }
        nodesList.Clear();
    }

    void CastAcceptance() {
        if (Input.GetButton("Fire2")) {
            currentMouseStatus = SkillStatus.None;
        }

        // Get casting starting point.
        if (Input.GetButtonDown("Fire1") && currentMouseStatus == SkillStatus.None) {
            DeleteAllNodes();
            Detach();
            lastNodePlacement = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                                          mainCamera.ScreenToWorldPoint(Input.mousePosition).y,
                                          0);
            AddNode(lastNodePlacement);
            currentMouseStatus = SkillStatus.ButtonDown;
        }

        if (currentMouseStatus == SkillStatus.ButtonDown) {
            worldClickPoint = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                              mainCamera.ScreenToWorldPoint(Input.mousePosition).y,
                              0);

            if ((worldClickPoint - lastNodePlacement).sqrMagnitude > nextNodeDistance * nextNodeDistance) {
                AddNode(worldClickPoint);
                lastNodePlacement = worldClickPoint;
            }
        }

        if (Input.GetButtonUp("Fire1")){
            currentMouseStatus = SkillStatus.None;

        }
    }

    private void OnDisable() {
        Detach();
        DeleteAllNodes();
    }

    private void Detach() {
        activeNodeIndex = -1;
        activeNode = null;
        rb.gravityScale = initialGravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        NodePull();
        DetachFromAcceptance();
        CastAcceptance();
    }
}
