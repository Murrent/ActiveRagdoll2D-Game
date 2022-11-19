using UnityEngine;
using UnityEngine.UI;

public class FPScounter : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = "FPS: " + (int)(1f / Time.unscaledDeltaTime);
    }
}
