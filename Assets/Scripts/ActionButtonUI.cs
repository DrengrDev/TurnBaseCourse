using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;

    public void SetBaseAction(BaseAction baseAction)
    {
        textMeshPro.text = baseAction.GetActionName().ToUpper();
    }
}
