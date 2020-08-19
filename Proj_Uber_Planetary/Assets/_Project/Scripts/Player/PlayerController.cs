﻿using System.Diagnostics.CodeAnalysis;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player
{
    /// <summary>
    /// Delegates tasks to other classes according to input
    /// </summary>
    public class PlayerController : MonoBehaviour 
    {
        //Private Members
        private IInputProvider _inputHandler;
        private CursorController _cursorController;
        private IRotationHandler _rotationHandler;
        private IMovementHandler _movementHandler;
        private IBoostHandler _boostHandler;
        
        private void Awake()
        {
            AssignComponents();
            AssignDelegates();
        }
        
        private void Rotate(Vector3 dir)
        {
            _rotationHandler.Rotate(new Vector3(-_cursorController.CursorAxis.y,_cursorController.CursorAxis.x,dir.z));
        }
        
        private void MoveForward(float val)
        {
            _movementHandler.MoveForward(val);
        }

        private void MoveBackward(float val)
        {
            _movementHandler.MoveBackward(val);
        }
        
        private void OnBoost(float val)
        {
            if (val >= .1)
            {
                _rotationHandler.DampenRotation();
            }
            else
            { 
                _rotationHandler.ResetRotationMultiplier();
            }
            _boostHandler.Boost(val);
        }
        
        /// <summary>
        /// Get Component Reference from GameObject
        /// </summary>
        private void AssignComponents()
        {
            _cursorController = FindObjectOfType<CursorController>();
            _inputHandler = GetComponent<InputHandler>();
            _boostHandler = GetComponent<IBoostHandler>();
            _rotationHandler = GetComponent<IRotationHandler>();
            _movementHandler = GetComponent<IMovementHandler>();
        }
    
        /// <summary>
        /// Assign methods to respective delegates
        /// </summary>
        private void AssignDelegates()
        {
            _inputHandler.OnRotate += Rotate;
            _inputHandler.OnMoveForward += MoveForward;
            _inputHandler.OnBoost += OnBoost;
            _inputHandler.OnMoveBackward += MoveBackward;
        }

        /// <summary>
        /// UnAssign methods on Disable
        /// </summary>
        private void OnDisable()
        {
            _inputHandler.OnRotate -= Rotate;
            _inputHandler.OnMoveForward -= MoveForward;
            _inputHandler.OnBoost -= OnBoost;
            _inputHandler.OnMoveBackward -= MoveBackward;
        }
    }
}
