using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    PlayerCharacterController2D controller;

    [Header("Player movement values.")]
    public float runSpeed = 40f;

    [HideInInspector]
    public bool frozen = false;
    // Movement values
    float horizontalMovement = 0f;

    Rigidbody2D rb;
    bool hasJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        AssignComponents();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen) { 
            horizontalMovement  = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        if (Input.GetButtonDown("Jump")) {
            hasJumped = true;
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontalMovement * Time.deltaTime, false, hasJumped);
        hasJumped = false;
        if(rb.velocity.sqrMagnitude > 20 * 20) {
            rb.velocity *= .95f;
        }
    }

    void AssignComponents() {
        controller = GetComponent<PlayerCharacterController2D>();
    }
}
