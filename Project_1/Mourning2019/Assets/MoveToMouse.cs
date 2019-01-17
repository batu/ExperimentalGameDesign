using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMouse : MonoBehaviour
{
    GameObject renderCamera;
    Camera mainCam;
    private void Start() {
        mainCam = Camera.main;
        renderCamera = GameObject.FindGameObjectWithTag("RendererCamera");
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Input.mousePosition;
        Vector2 worldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        renderCamera.transform.position = new Vector3(worldPos.x, worldPos.y, -10);
    }
}
