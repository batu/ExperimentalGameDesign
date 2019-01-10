using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerCharacterController2D controller;

    [Header("Player movement values.")]
    public float runSpeed = 40f;

    // Movement values
    float horizontalMovement = 0f;


    bool hasJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        AssignComponents();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement  = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump")) {
            hasJumped = true;
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontalMovement * Time.deltaTime, false, hasJumped);
        hasJumped = false;
    }

    void AssignComponents() {
        controller = GetComponent<PlayerCharacterController2D>();
    }
}
