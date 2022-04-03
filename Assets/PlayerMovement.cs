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

    public bool enableDelay = true;
    public float delay = 1f;
    public float chargeSpeed = 1f;

    private Dictionary<string, float> delayLevel = new Dictionary<string, float>();

    public float speed = 5f;
    public float sprint = 15f;

    private List<KeyCode> directionKeyCodes;
    private List<Vector3> directionVectors;

    private void Start()
    {
        directionKeyCodes = new List<KeyCode>();
        directionKeyCodes.Add(upKey);
        directionKeyCodes.Add(leftKey);
        directionKeyCodes.Add(downKey);
        directionKeyCodes.Add(rightKey);

        directionVectors = new List<Vector3>();
        directionVectors.Add(Vector3.forward);
        directionVectors.Add(Vector3.left);
        directionVectors.Add(Vector3.back);
        directionVectors.Add(Vector3.right);
    }

    void FixedUpdate()
    {
        Vector3 velocityDir = Vector3.zero;
        float velocityMag = speed;
        for (int i = 0; i < directionKeyCodes.Count; i++)
        {
            velocityDir = HandleDirection(velocityDir, directionKeyCodes[i], directionVectors[i]);
        }
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

    private void Update()
    {
        HandleDelay();
    }

    private void HandleDelay()
    {
        if (!enableDelay)
        {
            return;
        }
        directionKeyCodes.ForEach(keyCode => ChargeDirection(keyCode));
    }

    private void ChargeDirection(KeyCode keyCode)
    {
        string directionLabel = keyCode.ToString();
        if (!delayLevel.ContainsKey(directionLabel))
        {
            delayLevel.Add(directionLabel, 0f);
        }
        if (delayLevel[directionLabel] > delay)
        {
            return;
        }
        delayLevel[directionLabel] = Mathf.Min(1f, delayLevel[directionLabel] + Time.deltaTime * chargeSpeed);
        print(directionLabel + delayLevel[directionLabel]);
    }
}
