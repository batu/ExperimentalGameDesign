using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowerMainMenu : MonoBehaviour
{
    GameObject player;
    Transform moveTreshold;

    Vector3 initialPosition;
    Vector3 initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moveTreshold = GameObject.Find("CameraMoveTreshold").transform;
        initialPosition = transform.position;
        initialOffset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera() {
       transform.position = new Vector3((player.transform.position - initialOffset).x, transform.position.y, transform.position.z);
    }
}
