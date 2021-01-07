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
    key.text = reference?.action.bindings[bindingIndex].ToDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions) ?? " ";
  }
  public void SetKeyBinding()
  {
    // change GUI
    key.text = "press any key";

    // perform rebinding
    reference.action.PerformInteractiveRebinding()
    .WithControlsExcluding("Mouse")
    .OnMatchWaitForAnother(0.1f)
    .OnCancel((task) =>
    {

    })
    .OnApplyBinding((task, changedKey) =>
    {
      // change GUI
      // string changedKey = InputControlPath.ToHumanReadableString(
      //   action.bindings[0].effectivePath,
      //   InputControlPath.HumanReadableStringOptions.OmitDevice
      // );

    })
    .OnComplete((task) =>
    {
      task.Dispose();
      foreach (var binding in reference.action.bindings)
      {
        Debug.Log(binding.effectivePath);
      }
      // change GUI
      key.text = reference.action.bindings[bindingIndex].ToDisplayString();
      Debug.Log("rebind completed");
    })
    .Start();
  }
}
