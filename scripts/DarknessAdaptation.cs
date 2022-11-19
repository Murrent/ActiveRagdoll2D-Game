using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DarknessAdaptation : MonoBehaviour
{
    public float brightness;
    public float speed;
    bool fadeIn;
    bool fading;
    Light2D light;
    private void Start()
    {
        light = GetComponent<Light2D>();
        light.intensity = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            fadeIn = true;
            fading = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            fadeIn = false;
            fading = true;
        }
    }

    void Update()
    {
        if (fading)
        {
            if (fadeIn)
            {
                light.intensity = Mathf.Lerp(light.intensity, brightness, speed * Time.deltaTime);
                if (light.intensity > brightness - 0.01f)
                {
                    fading = false;
                    light.intensity = brightness;
                }
            }
            else
            {
                light.intensity = Mathf.Lerp(light.intensity, 0.0f, speed * Time.deltaTime);
                if (light.intensity < 0.01f)
                {
                    fading = false;
                    light.intensity = 0.0f;
                }
            }
        }
    }
}
