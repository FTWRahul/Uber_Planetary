﻿using UberPlanetary.Core.Interfaces;
using UberPlanetary.Player.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace UberPlanetary.Navigation
{
    /// Updates the Navigation Icon based on its position relative to the camera in the scene.
    /// Also Exposes properties of the icon to be modified at runtime
    public class NavigationIcon : MonoBehaviour , ILandmarkIcon
    {
        //private members
        private Camera _camera;
        private PlayerController _player;
        private ILandmark target;
        private Vector2 _xMinMax, _yMinMax;

        //exposed fields
        [SerializeField] private Vector3 offset;

        //public properties
        public Image iconImage { get; set; }
        public Color iconColor
        {
            get => iconImage.color;
            set => iconImage.color = value;
        }

        private void Awake()
        {
            iconImage = GetComponent<Image>(); 
            target = GetComponentInParent<ILandmark>();
            _xMinMax.x = iconImage.GetPixelAdjustedRect().width / 2f;
            _xMinMax.y = Screen.width - _xMinMax.x;
            
            _yMinMax.x = iconImage.GetPixelAdjustedRect().height / 2f;
            _yMinMax.y = Screen.height - _yMinMax.x;
            
        }

        private void Start()
        {
            _camera = Camera.main;

            _player = FindObjectOfType<PlayerController>();
        }
        
        private void FixedUpdate()
        {
            UpdateIconPosition();
        }

        public void ToggleImage()
        {
            iconImage.enabled = !iconImage.isActiveAndEnabled;
        }

        //Map the icon's position on the canvas based on the camera and world offset.
        public void UpdateIconPosition()
        { 
            Vector2 pos = _camera.WorldToScreenPoint(target.GetTransform.position + offset);
            
            pos.x = Mathf.Clamp(pos.x, _xMinMax.x, _xMinMax.y);
            pos.y = Mathf.Clamp(pos.y, _yMinMax.x, _yMinMax.y);
            
            if(Vector3.Dot((target.GetTransform.position - _player.transform.position), _player.transform.forward) < 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = _xMinMax.y;
                }
                else
                {
                    pos.x = _xMinMax.x;
                }
            }
            iconImage.transform.position = pos;
        }
    }
}