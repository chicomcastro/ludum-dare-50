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

    void FixedUpdate()
    {
        Vector3 velocityDir = Vector3.zero;
        float velocityMag = speed;
        velocityDir = HandleDirection(velocityDir, upKey, Vector3.forward);
        velocityDir = HandleDirection(velocityDir, leftKey, Vector3.left);
        velocityDir = HandleDirection(velocityDir, downKey, Vector3.back);
        velocityDir = HandleDirection(velocityDir, rightKey, Vector3.right);
        if (Input.GetKey(sprintKey))
        {
            velocityMag = sprint;
        }
        rb.velocity = new Vector3(velocityDir.x, 0, velocityDir.z).normalized * velocityMag + Vector3.up * rb.velocity.y;
    }

    private Vector3 HandleDirection(Vector3 velocityDir, KeyCode keyCode, Vector3 referenceDirection)
    {
        if (Input.GetKey(keyCode))
        {
            velocityDir += referenceDirection;
        }
        return velocityDir;
    }
        {
            velocityMag = sprint;
        }
        rb.velocity = new Vector3(velocityDir.x, 0, velocityDir.z).normalized * velocityMag + Vector3.up * rb.velocity.y;
    }
}
