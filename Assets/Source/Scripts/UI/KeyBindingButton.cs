using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class KeyBindingButton : MonoBehaviour
{
    public TMP_Text key;
    public InputActionReference reference;
    public int bindingIndex = 0;

    private void Start()
    {
        if (reference != null)
            key.text = reference.action.bindings[bindingIndex].ToDisplayString() ?? " ";
    }
    public void SetKeyBinding()
    {
        // change GUI
        key.text = "Press any key";

        // perform rebinding
        reference.action.PerformInteractiveRebinding()
        .WithControlsExcluding("Mouse")
        .WithTargetBinding(bindingIndex)
        .OnMatchWaitForAnother(0.1f)
        .OnComplete((task) =>
        {
            task.Dispose();
            foreach (var binding in reference.action.bindings)
            {
                Debug.Log(binding.effectivePath);
            }
            // change GUI
            key.text = reference.action.bindings[bindingIndex].ToDisplayString();
        })
        .Start();
    }
}
