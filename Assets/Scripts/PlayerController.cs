using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float moveX;
    private float moveY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue inputValue){
        Vector2 moveV = inputValue.Get<Vector2>();
        moveX = moveV.x;
        moveY = moveV.y;
    }

    void FixedUpdate(){
        Vector3 mv3 = new Vector3(moveX,0.0f,moveY);
        rb.AddForce(mv3 * speed);
    }
}
