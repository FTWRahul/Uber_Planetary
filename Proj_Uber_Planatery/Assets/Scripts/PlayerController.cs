﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler _inputHandler;
    private CursorController _cursorController;
    private Rigidbody _rigidbody;
    
    [SerializeField] private float rotationSpeed;
    [SerializeField] [Range(0,1)] private float rotationLossMultiplier;
    private float _originalRotLossMultiplier;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float boostSpeed;
    
    private void Awake()
    {
        AssignComponents();
        AssignDelegates();
        
        _originalRotLossMultiplier = rotationLossMultiplier;
        rotationLossMultiplier = 1f;
        
        Cursor.visible = false;
    }
    
    /// <summary>
    /// Rotate object based on mouse cursor position and other inputs
    /// </summary>
    /// <param name="dir"></param>
    private void Rotate(Vector3 dir)
    {
        transform.Rotate(new Vector3(-_cursorController.CursorAxis.y,_cursorController.CursorAxis.x,-dir.z) * (rotationLossMultiplier * (rotationSpeed * Time.deltaTime)));
    }

    /// <summary>
    /// Translate object forward
    /// </summary>
    /// <param name="val"></param>
    private void Move(float val)
    {
        transform.Translate(transform.forward * (val * (movementSpeed * Time.deltaTime)), Space.World);
    }

    /// <summary>
    /// Translate object forward and reduce rotation speed
    /// </summary>
    /// <param name="isHeld"></param>
    private void Boost(bool isHeld)
    {
        if (isHeld)
        {
            rotationLossMultiplier = _originalRotLossMultiplier;
        }
        else
        { 
            rotationLossMultiplier = 1f;
        }
        transform.Translate(transform.forward * (boostSpeed * Time.deltaTime), Space.World);
        // rotationSpeed = _originalRotSpeed;
    }
    
    /// <summary>
    /// Get Component Reference from gameobject
    /// </summary>
    private void AssignComponents()
    {
        _cursorController = GetComponentInChildren<CursorController>();
        _rigidbody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
    }
    
    /// <summary>
    /// Assign methods to respective delegates
    /// </summary>
    private void AssignDelegates()
    {
        _inputHandler.rotationDelegate += Rotate;
        _inputHandler.movementDelegate += Move;
        _inputHandler.boostDelegate += Boost;
    }

    /// <summary>
    /// UnAssign methods on Disable
    /// </summary>
    private void OnDisable()
    {
        if (_inputHandler.rotationDelegate != null) _inputHandler.rotationDelegate -= Rotate;
        if (_inputHandler.movementDelegate != null) _inputHandler.movementDelegate -= Move;
        if (_inputHandler.boostDelegate != null) _inputHandler.boostDelegate -= Boost;
    }
}
