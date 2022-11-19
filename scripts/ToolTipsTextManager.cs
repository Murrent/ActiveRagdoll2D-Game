using UnityEngine;
using UnityEngine.UI;

public class ToolTipsTextManager : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void SetText(string displayText)
    {
        text.text = displayText;
    }

    public void HideText()
    {
        text.text = "";
    }
}