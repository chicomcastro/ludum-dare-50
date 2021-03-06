using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Controller Keys")]
    public ControllerKey[] controllerKeys;

    [Header("Movement")]
    public float speed = 5f;
    public bool enableSprint = true;
    public float sprint = 15f;

    [Header("Delay mechanic")]
    public bool enableDelay = true;
    public float delay = 1f;
    public float chargeSpeed = 1f;

    private Dictionary<string, float> delayLevel = new Dictionary<string, float>();
    private Dictionary<string, Vector3> keyCode2VectorMap = new Dictionary<string, Vector3>();
    private Dictionary<string, ControllerKey> controllerKeyMap = new Dictionary<string, ControllerKey>();

    private Vector3 targetVelocity;
    private float targetSpeed;

    private void Start()
    {
        if (controllerKeys.Length < 5)
        {
            throw new Exception("There's no enought controller keys registered");
        }

        keyCode2VectorMap.Add(KeyLabel.up.ToString(), Vector3.forward);
        keyCode2VectorMap.Add(KeyLabel.left.ToString(), Vector3.left);
        keyCode2VectorMap.Add(KeyLabel.down.ToString(), Vector3.back);
        keyCode2VectorMap.Add(KeyLabel.right.ToString(), Vector3.right);

        controllerKeys.ToList().ForEach(controllerKey => {
            string keyLabel = controllerKey.keyLabel.ToString();
            controllerKeyMap.Add(keyLabel, controllerKey);

            if (keyCode2VectorMap.ContainsKey(keyLabel))
            {
                controllerKey.direction = keyCode2VectorMap[keyLabel];
            }

            if (controllerKey.slider != null)
            {
                controllerKey.slider.gameObject.SetActive(false);
            }

            delayLevel.Add(keyLabel, 0f);
        });
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(targetVelocity.x, 0, targetVelocity.z).normalized * targetSpeed + Vector3.up * rb.velocity.y;
    }

    private Vector3 HandleMovement(Vector3 velocityDir)
    {
        controllerKeys.ToList().ForEach(controllerKey => {
            string keyLabel = controllerKey.keyLabel.ToString();
            velocityDir = HandleDirection(velocityDir, controllerKey);
        });
        return velocityDir;
    }

    private Vector3 HandleDirection(Vector3 velocityDir, ControllerKey controllerKey)
    {
        KeyCode keyCode = controllerKey.keyCode;
        string directionLabel = controllerKey.keyLabel.ToString();
        bool movementValidation = enableDelay && controllerKey.delayEnabled;
        bool canMoveThisDirection = movementValidation ? delayLevel[directionLabel] >= delay : true;
        if (Input.GetKey(keyCode) && canMoveThisDirection)
        {
            velocityDir += controllerKey.direction;
        }
        if (Input.GetKeyUp(keyCode))
        {
            if (controllerKey.slider != null)
            {
                controllerKey.slider.value = 0f;
            }
        }
        return velocityDir;
    }

    private float HandleSprint(float velocityMag)
    {
        ControllerKey sprintControllerKey = controllerKeyMap[KeyLabel.sprint.ToString()];
        if (Input.GetKey(sprintControllerKey.keyCode) && enableSprint)
        {
            velocityMag = sprint;
        }
        return velocityMag;
    }

    private void Update()
    {
        HandleDelay();
        HandleInput();
    }

    private void HandleInput()
    {
        Vector3 velocityDir = Vector3.zero;
        float velocityMag = speed;
        if (CanMove())
        {
            velocityDir = HandleMovement(velocityDir);
            velocityMag = HandleSprint(velocityMag);
        }
        targetVelocity = velocityDir;
        targetSpeed = velocityMag;
    }

    private void HandleDelay()
    {
        if (!enableDelay || !CanMove())
        {
            return;
        }
        controllerKeys.ToList().ForEach(ChargeKey);
    }

    private void ChargeKey(ControllerKey controllerKey)
    {
        string keyLabel = controllerKey.keyLabel.ToString();
        if (!Input.GetKey(controllerKey.keyCode))
        {
            delayLevel[keyLabel] = 0f;
            return;
        }

        if (delayLevel[keyLabel] >= delay)
        {
            return;
        }
        delayLevel[keyLabel] = Mathf.Min(1f, delayLevel[keyLabel] + Time.deltaTime * chargeSpeed);
        if (controllerKey.slider != null)
        {
            controllerKey.slider.value = delayLevel[keyLabel];
        }
    }

    public void EnableDelay(KeyLabel keyLabel)
    {
        ControllerKey controllerKey = controllerKeyMap[keyLabel.ToString()];
        controllerKey.delayEnabled = true;
        if (controllerKey.slider != null)
        {
            controllerKey.slider.gameObject.SetActive(true);
        }
    }

    private bool CanMove()
    {
        return !LevelManager.instance.IsInInterval() && !LifeManager.instance.isDead && !PauseController.instance.isPaused;
    }
}

public enum KeyLabel { up, left, down, right, sprint };

[System.Serializable]
public class ControllerKey
{
    public KeyLabel keyLabel;
    public KeyCode keyCode;
    public bool delayEnabled;
    public Slider slider;

    [HideInInspector]
    public Vector3 direction;
}
