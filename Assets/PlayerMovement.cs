using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Controller Keys")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode downKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Movement")]
    public float speed = 5f;
    public bool enableSprint = true;
    public float sprint = 15f;

    [Header("Delay mechanic")]
    public bool enableDelay = true;
    public float delay = 1f;
    public float chargeSpeed = 1f;

    [HideInInspector]    
    public bool delayUp = true;
    [HideInInspector]    
    public bool delayLeft = true;
    [HideInInspector]    
    public bool delayDown = true;
    [HideInInspector]
    public bool delayRight = true;

    private Dictionary<string, float> delayLevel = new Dictionary<string, float>();
    private List<KeyCode> directionKeyCodes;
    private Dictionary<string, KeyCode> directionKeyCodesMap = new Dictionary<string, KeyCode>();
    private Dictionary<string, Vector3> keyCode2VectorMap = new Dictionary<string, Vector3>();
    private Dictionary<string, bool> keyCode2DelayStatus = new Dictionary<string, bool>();

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
        velocityMag = HandleSprint(velocityMag);
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

    private float HandleSprint(float velocityMag)
    {
        if (Input.GetKey(sprintKey) && enableSprint)
        {
            velocityMag = sprint;
        }
        return velocityMag;
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

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor
{
    override public void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var myScript = target as PlayerMovement;
        if (myScript.enableDelay)
        {
            myScript.delayUp = GUILayout.Toggle(myScript.delayUp, "Delay Up");
            myScript.delayLeft = GUILayout.Toggle(myScript.delayLeft, "Delay Left");
            myScript.delayDown = GUILayout.Toggle(myScript.delayDown, "Delay Down");
            myScript.delayRight = GUILayout.Toggle(myScript.delayRight, "Delay Right");
        }
    }
}
