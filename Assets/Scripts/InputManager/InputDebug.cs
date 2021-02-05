using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDebug : MonoBehaviour
{
    List<UnityEngine.XR.InputDevice> leftHandedControllers;
    public CCCanvas leftHandCC;

    List<UnityEngine.XR.InputDevice> rightHandedControllers;
    public CCCanvas rightHandCC;

    // Start is called before the first frame update
    void Start()
    {
        ConnectControllers();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLeftHandedInputs();
        CheckRightHandedInputs();
    }

    void ConnectControllers()
    {
        leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        rightHandedControllers = new List<UnityEngine.XR.InputDevice>();

        //Left Hand Controller
        UnityEngine.XR.InputDeviceCharacteristics leftHandedCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;

        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(leftHandedCharacteristics, leftHandedControllers);

        foreach (UnityEngine.XR.InputDevice device in leftHandedControllers)
        {
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }

        //Right Hand Controller
        UnityEngine.XR.InputDeviceCharacteristics rightHandedCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;

        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rightHandedCharacteristics, rightHandedControllers);

        foreach (UnityEngine.XR.InputDevice device in rightHandedControllers)
        {
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }
    }

    void CheckLeftHandedInputs()
    {
        bool triggerValue;
        if (leftHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            string debugText = leftHandedControllers[0].name + " Trigger button is pressed.";
            Debug.Log(debugText);
            leftHandCC.CCText.text = debugText;
        }

        bool gripValue;
        if (leftHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
        {
            Debug.Log(leftHandedControllers[0].name + " Grip button is pressed.");
        }

        bool primaryButton;
        if (leftHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButton) && primaryButton)
        {
            Debug.Log(leftHandedControllers[0].name + " Primary button (X) is pressed.");
        }

        bool secondaryButton;
        if (leftHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButton) && secondaryButton)
        {
            Debug.Log(leftHandedControllers[0].name + " Secondary button (Y) is pressed.");
        }

        bool menuButton;
        if (leftHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuButton) && menuButton)
        {
            Debug.Log(leftHandedControllers[0].name + " Menu button is pressed.");
        }

        bool primary2DAxisClick;
        if (leftHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            Debug.Log(leftHandedControllers[0].name + " Joystick click button is pressed.");
        }

        bool primary2DAxisTouch;
        if (leftHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            Debug.Log(leftHandedControllers[0].name + " Joystick is moved.");
        }
    }

    void CheckRightHandedInputs()
    {
        bool triggerValue;
        if (rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            Debug.Log(rightHandedControllers[0].name + " Trigger button is pressed.");
        }

        bool gripValue;
        if (rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out gripValue) && gripValue)
        {
            Debug.Log(rightHandedControllers[0].name + " Grip button is pressed.");
        }

        bool primaryButton;
        if (rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButton) && primaryButton)
        {
            Debug.Log(rightHandedControllers[0].name + " Primary button (A) is pressed.");
        }

        bool secondaryButton;
        if (rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButton) && secondaryButton)
        {
            Debug.Log(rightHandedControllers[0].name + " Secondary button (B) is pressed.");
        }

        bool menuButton;
        if (rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out menuButton) && menuButton)
        {
            Debug.Log(rightHandedControllers[0].name + " Menu button is pressed.");
        }

        bool primary2DAxisClick;
        if (rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out primary2DAxisClick) && primary2DAxisClick)
        {
            Debug.Log(rightHandedControllers[0].name + " Joystick click button is pressed.");
        }

        bool primary2DAxisTouch;
        if (rightHandedControllers[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisTouch, out primary2DAxisTouch) && primary2DAxisTouch)
        {
            Debug.Log(rightHandedControllers[0].name + " Joystick is moved.");
        }
    }
}
