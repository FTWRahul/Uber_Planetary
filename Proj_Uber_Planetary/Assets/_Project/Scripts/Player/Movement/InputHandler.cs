using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UberPlanetary.Core;
using UnityEngine;

namespace UberPlanetary.Player.Movement
{
    /// Implements the input interface and invokes events based on proper axis's names
    public class InputHandler : MonoBehaviour, IInputProvider
    {
        private Dictionary<KeyCode, ButtonEvent> _clickInfo = new Dictionary<KeyCode, ButtonEvent>();
        
        //Delegate Deceleration
        public event Action<Vector3> OnRotate;
        public event Action<float> OnMoveForward;
        public event Action<float> OnMoveBackward;
        public event Action<Vector3> OnMousePosition;
        public event Action<float> OnBoost;
        public event Action<Vector2> OnScroll;
        public Dictionary<KeyCode, ButtonEvent> ClickInfo => _clickInfo;

        //Exposed input names
        [Header("Movement")]
        [SerializeField] private string xRotAxisName;
        [SerializeField] private string yRotAxisName;
        [SerializeField] private string zRotAxisName;
        [SerializeField] private string boostAxisName;
        [SerializeField] private string forwardAxisName;
        [SerializeField] private string backwardAxisName;
        
        [Space(10)]
        [Header("Mouse")]
        [SerializeField] private string scrollAxisName;

        [Header("Buttons")] 
        [SerializeField] private KeyCode[] keyCodes;


        private void Awake()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            foreach (var keyCode in keyCodes)
            {
                if (!_clickInfo.ContainsKey(keyCode))
                {
                    _clickInfo.Add(keyCode, new ButtonEvent());
                }
            }
        }

        /// Capture Input and invoke respective Events
        private void Update()
        {
            UpdateMovementAxis();
            
            UpdateMouse();

            foreach (var kvp in _clickInfo)
            {
                ButtonPress(kvp.Key);
            }
        }

        private void UpdateMouse()
        {
            OnScroll?.Invoke(Input.mouseScrollDelta);
            OnMousePosition?.Invoke(Input.mousePosition);
        }
        
        private void ButtonPress(KeyCode keyCode)
        {
            if (Input.GetKeyDown(keyCode)) _clickInfo[keyCode].OnDown?.Invoke();
            if (Input.GetKey(keyCode)) _clickInfo[keyCode].OnHeld?.Invoke();
            if (Input.GetKeyUp(keyCode)) _clickInfo[keyCode].OnUp?.Invoke();
        }
        
        private void UpdateMovementAxis()
        {
            OnRotate?.Invoke(new Vector3(
                Input.GetAxisRaw(xRotAxisName),
                Input.GetAxisRaw(yRotAxisName),
                Input.GetAxisRaw(zRotAxisName)
            ));

            OnMoveForward?.Invoke(Input.GetAxis(forwardAxisName));
            OnMoveBackward?.Invoke(Input.GetAxis(backwardAxisName));
            OnBoost?.Invoke(Input.GetAxis(boostAxisName));
        }
    }

    
}
