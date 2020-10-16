using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatMovement : MonoBehaviour
{
    public CharacterController2D controller;
    // Start is called before the first frame update

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    Rigidbody2D _rb;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
