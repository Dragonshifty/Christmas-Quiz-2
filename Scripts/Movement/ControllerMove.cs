using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMove : MonoBehaviour
{
    
    [SerializeField] float moveSpeed = 200f;

    Vector2 moveInput;
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        rigid.velocity = moveInput * moveSpeed * Time.deltaTime;
    }
}
