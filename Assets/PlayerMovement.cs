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
    private Dictionary<string, KeyCode> directionKeyCodesMap = new Dictionary<string, KeyCode>();
    private Dictionary<string, Vector3> keyCode2VectorMap = new Dictionary<string, Vector3>();

    private void Start()
    {
        directionKeyCodes = new List<KeyCode>
        {
            upKey,
            leftKey,
            downKey,
            rightKey
        };

        directionKeyCodes.ForEach(keyCode => {
            string directionLabel = keyCode.ToString();
            directionKeyCodesMap.Add(directionLabel, keyCode);
            delayLevel.Add(directionLabel, 0f);
        });

        keyCode2VectorMap.Add(upKey.ToString(), Vector3.forward);
        keyCode2VectorMap.Add(leftKey.ToString(), Vector3.left);
        keyCode2VectorMap.Add(downKey.ToString(), Vector3.back);
        keyCode2VectorMap.Add(rightKey.ToString(), Vector3.right);
    }

    void FixedUpdate()
    {
        Vector3 velocityDir = Vector3.zero;
        float velocityMag = speed;
        for (int i = 0; i < directionKeyCodes.Count; i++)
        {
            KeyCode directionKeyCode = directionKeyCodes[i];
            string directionLabel = directionKeyCode.ToString();
            velocityDir = HandleDirection(velocityDir, directionKeyCode, keyCode2VectorMap[directionLabel]);
        }
        if (Input.GetKey(sprintKey))
        {
            velocityMag = sprint;
        }
        rb.velocity = new Vector3(velocityDir.x, 0, velocityDir.z).normalized * velocityMag + Vector3.up * rb.velocity.y;
    }

    private Vector3 HandleDirection(Vector3 velocityDir, KeyCode keyCode, Vector3 referenceDirection)
    {
        string directionLabel = keyCode.ToString();
        bool canMoveThisDirection = enableDelay ? delayLevel[directionLabel] >= delay : true;
        if (Input.GetKey(keyCode) && canMoveThisDirection)
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

        KeyCode directionKeyCode = directionKeyCodesMap[directionLabel];
        if (!Input.GetKey(directionKeyCode))
        {
            delayLevel[directionLabel] = 0f;
            return;
        }

        if (delayLevel[directionLabel] >= delay)
        {
            return;
        }
        delayLevel[directionLabel] = Mathf.Min(1f, delayLevel[directionLabel] + Time.deltaTime * chargeSpeed);
        print(directionLabel + delayLevel[directionLabel]);
    }
}
