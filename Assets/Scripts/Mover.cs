using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float inicialSpeed = 2000f;
    public float MoveSpeed;

    private CharacterController controller;
    private Rigidbody rb;
    private Vector2 horizontalInput;  // Only for left (A) and right (D) movement

    private void Awake()
    {
        MoveSpeed = inicialSpeed;
        rb = GetComponent<Rigidbody>();
    }

    public void SetInput(Vector2 input)
    {
        horizontalInput = input;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(0, horizontalInput.x, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= MoveSpeed;
        rb.AddForce(moveDirection);
    }
}
