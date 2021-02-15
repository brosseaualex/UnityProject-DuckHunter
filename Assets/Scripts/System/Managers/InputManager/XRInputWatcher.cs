using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PrimaryButtonEvent : UnityEvent<bool> { }
public class SecondaryButtonEvent : UnityEvent<bool> { }
public class TriggerButtonEvent : UnityEvent<bool> { }
public class MenuButtonEvent : UnityEvent<bool> { }

public class XRInputWatcher : MonoBehaviour
{
    [SerializeField]
    private XRBaseController _xRBaseController;
    private InputDevice _inputDevice;

    public PrimaryButtonEvent primaryButtonPressEvent;
    public SecondaryButtonEvent secondaryButtonPressEvent;
    public TriggerButtonEvent triggerButtonPressEvent;
    public MenuButtonEvent menuButtonPressEvent;

    private bool _primaryLastButtonState = false;
    private bool _secondaryLastButtonState = false;
    private bool _triggerLastButtonState = false;
    private bool _menuLastButtonState = false;

    private void Awake()
    {
        _xRBaseController = GetComponent<XRBaseController>();

        if (primaryButtonPressEvent == null)
            primaryButtonPressEvent = new PrimaryButtonEvent();

        if (secondaryButtonPressEvent == null)
            secondaryButtonPressEvent = new SecondaryButtonEvent();

        if (triggerButtonPressEvent == null)
            triggerButtonPressEvent = new TriggerButtonEvent();
        if (menuButtonPressEvent == null)
            menuButtonPressEvent = new MenuButtonEvent();
    }

    private void Start()
    {
        if (_xRBaseController.tag == ("LeftController"))
        {
            if (XRInputManager.Instance.leftHandController != null)
                _inputDevice = XRInputManager.Instance.leftHandController;

            if (_inputDevice.name != null)
            {
                if (XRInputDebugger.Instance.inputDebugEnabled)
                    Debug.Log("XR InputWatcher has been linked to: " + _inputDevice.name);
            }
            else
            {
                Debug.LogError("Unable to link XR InputWatcher to Left Controller.");
            }
        }

        if (_xRBaseController.tag == ("RightController"))
        {
            if (XRInputManager.Instance.rightHandController != null)
                _inputDevice = XRInputManager.Instance.rightHandController;

            if (_inputDevice.name != null)
            {
                if (XRInputDebugger.Instance.inputDebugEnabled)
                    Debug.Log("XR InputWatcher has been linked to: " + _inputDevice.name);
            }
            else
            {
                Debug.LogError("Unable to link XR InputWatcher to Right Controller.");
            }
        }
    }

    private void Update()
    {
        bool primaryTempState = false;
        bool primaryButtonState = false;

        bool secondaryTempState = false;
        bool secondaryButtonState = false;

        bool triggerTempState = false;
        bool triggerButtonState = false;

        bool menuTempState = false;
        bool menuButtonState = false;

        primaryTempState = _inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonState) && primaryButtonState || primaryTempState;

        if (primaryTempState != _primaryLastButtonState)
        {
            primaryButtonPressEvent.Invoke(primaryTempState);
            _primaryLastButtonState = primaryTempState;
        }

        secondaryTempState = _inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonState) && secondaryButtonState || secondaryTempState;

        if (secondaryTempState != _secondaryLastButtonState)
        {
            secondaryButtonPressEvent.Invoke(secondaryTempState);
            _secondaryLastButtonState = secondaryTempState;
        }

        menuTempState = _inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out menuButtonState) && menuButtonState || menuTempState;

        if (menuTempState != _menuLastButtonState)
        {
            menuButtonPressEvent.Invoke(menuTempState);
            _menuLastButtonState = menuTempState;
        }

        triggerTempState = _inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonState) && triggerButtonState || triggerTempState;

        triggerButtonPressEvent.Invoke(triggerTempState);
        _triggerLastButtonState = triggerTempState;
    }
}