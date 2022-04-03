using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public KeyCode upKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode downKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;

    public float speed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        if (Input.GetKey(upKey))
        {
            velocity += Vector3.forward;
        }
        if (Input.GetKey(leftKey))
        {
            velocity += Vector3.left;
        }
        if (Input.GetKey(downKey))
        {
            velocity += Vector3.back;
        }
        if (Input.GetKey(rightKey))
        {
            velocity += Vector3.right;
        }
        rb.velocity = new Vector3(velocity.x, 0, velocity.z).normalized * speed + Vector3.up * rb.velocity.y;
    }
}
