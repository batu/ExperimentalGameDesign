using UnityEngine;

public class BargainingSkill: MonoBehaviour {

    public LayerMask moveableLayer;
    public float smoothSpeed = 0.5f;

    Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
    }

    bool skillActive = false;
    Rigidbody2D targetRigidbody = null;
    RaycastHit2D raycastHit;

    GameObject targetObject;
    Vector2 MousePosition;
    Vector2 objPosition;


    void Update() {
        GetTarget();
        MoveTarget();
        StopMovingTarget();
    }

    private void MoveTarget() {
        if (skillActive) {
            MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            objPosition = mainCamera.ScreenToWorldPoint(MousePosition);
            targetRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
            if (objPosition != Vector2.zero) {
                Vector3 desiredPosition = objPosition;
                Vector3 smoothedPosition = Vector3.Lerp(targetObject.transform.position, desiredPosition, smoothSpeed);
                targetObject.transform.position = smoothedPosition;
            }
        }
    }

    private void StopMovingTarget() {
        if (Input.GetMouseButtonUp(0)) {
            skillActive = false;
            targetRigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void GetTarget() {
        if (Input.GetMouseButton(0)) {
            raycastHit = Physics2D.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition).origin,
                                    mainCamera.ScreenPointToRay(Input.mousePosition).direction, 1000, moveableLayer);
            if (raycastHit) {
                skillActive = true;
                targetObject = raycastHit.transform.gameObject;
                if (!targetRigidbody)
                    targetRigidbody = raycastHit.transform.gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }
}