using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputReactorWeapon : MonoBehaviour
{
    [SerializeField]
    private XRInputWatcher _xRInputWatcher;
    private XRGrabInteractable _xRGrabInteractable;
    [SerializeField]
    private bool _primaryIsPressed = false, _secondaryIsPressed = false;

    private IWeapon _iWeapon;

    private void Start()
    {
        _iWeapon = (IWeapon)GetComponent(typeof(IWeapon));
        _xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        _primaryIsPressed = pressed;

        if (pressed)
        {
            _iWeapon.Reload();
            
            /*if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                string debugMessage = name + " Primary Button Event Fired";
                Debug.Log(debugMessage);
                XRInputDebugger.Instance.DebugLogInGame(debugMessage);
            }*/
        }
    }

    public void onSecondaryButtonEvent(bool pressed)
    {
        _secondaryIsPressed = pressed;

        if (pressed)
        {
            /*if (XRInputDebugger.Instance.inputDebugEnabled)
            {
                string debugMessage = name + " Secondary Button Event Fired";
                Debug.Log(debugMessage);
                XRInputDebugger.Instance.DebugLogInGame(debugMessage);
            }*/
        }
    }

    public void AssignWatcher()
    {
        XRBaseInteractor xRBaseInteractor = _xRGrabInteractable.selectingInteractor;
        _xRInputWatcher = GameObject.Find(xRBaseInteractor.gameObject.name).GetComponent<XRInputWatcher>();

        _xRInputWatcher.primaryButtonPressEvent.AddListener(onPrimaryButtonEvent);
        _xRInputWatcher.secondaryButtonPressEvent.AddListener(onSecondaryButtonEvent);
    }

    public void ClearWatcher()
    {
        _xRInputWatcher.primaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher.secondaryButtonPressEvent.RemoveAllListeners();
        _xRInputWatcher = null;
    }
}
