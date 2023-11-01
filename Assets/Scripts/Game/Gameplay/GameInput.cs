using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Game.Gameplay
{
    public class GameInput : MonoBehaviour
    {
        private readonly ISubject<Vector2> _onDownClicked = new Subject<Vector2>();
        private readonly ISubject<Vector2> _onUpClicked = new Subject<Vector2>();
        private readonly ISubject<Vector2> _onDragging = new Subject<Vector2>();

        private InputAction _leftMouseClick;
        private bool _isPressed;
        private bool _isEnabled;

        public IObservable<Vector2> onDownClicked => _onDownClicked;
        public IObservable<Vector2> onUpClicked => _onUpClicked;
        public IObservable<Vector2> onDragging => _onDragging;

        private void Awake()
        {
            _leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        }

        private void Update()
        {
            if (Application.isEditor && _isEnabled && _isPressed)
            {
                _onDragging.OnNext(GetMousePoint());
            }
        }

        private void OnDestroy()
        {
            SetEnabled(false);
        }

        public void SetEnabled(bool isEnabled)
        {
            if (_isEnabled == isEnabled)
            {
                return;
            }

            if (isEnabled)
            {
                if (Application.isEditor)
                {
                    _leftMouseClick.Enable();
                    _leftMouseClick.performed += HandleMouseDown;
                    _leftMouseClick.canceled += HandleMouseUp;
                }
                else
                {
                    TouchSimulation.Enable();
                    EnhancedTouchSupport.Enable();
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += HandleTouchDown;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += HandleTouchUp;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += HandleTouchMove;
                }
            }
            else
            {
                if (Application.isEditor)
                {
                    _leftMouseClick.performed -= HandleMouseDown;
                    _leftMouseClick.canceled -= HandleMouseUp;
                    _leftMouseClick.Disable();
                }
                else
                {
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= HandleTouchDown;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= HandleTouchUp;
                    UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= HandleTouchMove;
                    TouchSimulation.Disable();
                    EnhancedTouchSupport.Disable();
                }

                _isPressed = false;
            }

            _isEnabled = isEnabled;
        }

        private void HandleMouseDown(InputAction.CallbackContext context)
        {
            ClickDown(GetMousePoint());
        }

        private void HandleMouseUp(InputAction.CallbackContext context)
        {
            ClickUp(GetMousePoint());
        }

        private void HandleTouchDown(Finger finger)
        {
            ClickDown(finger.screenPosition);
        }

        private void HandleTouchUp(Finger finger)
        {
            ClickUp(finger.screenPosition);
        }

        private void HandleTouchMove(Finger finger)
        {
            if (!_isPressed)
            {
                return;
            }

            _onDragging.OnNext(finger.screenPosition);
        }

        private void ClickDown(Vector2 screenPoint)
        {
            if (IsClickOnUI(screenPoint))
            {
                return;
            }

            _onDownClicked.OnNext(screenPoint);
            _isPressed = true;
        }

        private void ClickUp(Vector2 screenPoint)
        {
            _isPressed = false;

            if (IsClickOnUI(screenPoint))
            {
                return;
            }

            _onUpClicked.OnNext(screenPoint);
        }

        private Vector2 GetMousePoint()
        {
            return Mouse.current.position.ReadValue();
        }

        private static bool IsClickOnUI(Vector2 screenPoint)
        {
            var pointerEventData = new PointerEventData(EventSystem.current) { position = screenPoint };
            var raycastResultsList = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

            return raycastResultsList.Any(result => result.gameObject is not null);
        }
    }
}
