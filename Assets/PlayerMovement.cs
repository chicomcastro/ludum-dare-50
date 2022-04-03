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
    public KeyCode sprintKey = KeyCode.LeftShift;

    public float speed = 5f;
    public float sprint = 15f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocityDir = Vector3.zero;
        float velocityMag = speed;
        if (Input.GetKey(upKey))
        {
            velocityDir += Vector3.forward;
        }
        if (Input.GetKey(leftKey))
        {
            velocityDir += Vector3.left;
        }
        if (Input.GetKey(downKey))
        {
            velocityDir += Vector3.back;
        }
        if (Input.GetKey(rightKey))
        {
            velocityDir += Vector3.right;
        }
        if (Input.GetKey(sprintKey))
        {
            velocityMag = sprint;
        }
        rb.velocity = new Vector3(velocityDir.x, 0, velocityDir.z).normalized * velocityMag + Vector3.up * rb.velocity.y;
    }
}
