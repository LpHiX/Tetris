using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RebindingScript : MonoBehaviour
{
    [SerializeField] private GameObject dataObject;
    [SerializeField] private TMP_Text bindingDisplayNameText;
    [SerializeField] private GameObject startRebindObject;
    [SerializeField] private GameObject pendingObject;
    [SerializeField] private int actionInt;

    InputAction action;
    DataScript dataScript;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private InputAction getAction()
    {
        switch (actionInt)
        {
            case 0:
                return dataScript.controls.Player.Left;
            case 1:
                return dataScript.controls.Player.Right;
            case 2:
                return dataScript.controls.Player.RotateLeft;
            case 3:
                return dataScript.controls.Player.RotateRight;
            case 4:
                return dataScript.controls.Player.Rotate180;
            case 5:
                return dataScript.controls.Player.Hold;
            case 6:
                return dataScript.controls.Player.SoftDrop;
            case 7:
                return dataScript.controls.Player.HardDrop;
            case 8:
                return dataScript.controls.Player.Restart;
            case 9:
                return dataScript.controls.Player.MainMenu;
            default:
                return null;
        }
    }

    public void startRebinding()
    {
        dataScript = dataObject.GetComponent<DataScript>();
        action = getAction();
        startRebindObject.SetActive(false);
        pendingObject.SetActive(true);
        rebindingOperation = action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => rebindComplete())
            .Start();
    }

    private void rebindComplete()
    {
        int bindingIndex = action.GetBindingIndexForControl(action.controls[0]);

        bindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        rebindingOperation.Dispose();

        startRebindObject.SetActive(true);
        pendingObject.SetActive(false);

                Debug.Log(action.GetBindingDisplayString());
    }
}
