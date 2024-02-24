using UnityEngine;
using TMPro; // Include this if you want to output FPS to a UI Text element.

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsDisplay; // Assign a UI Text component in the inspector if you want to display FPS on the screen.
    private float deltaTime = 0.0f;

    void Update()
    {
        // Calculate delta time.
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calculate FPS.
        float fps = 1.0f / deltaTime;

        // Display FPS (optional).
        if (fpsDisplay != null)
        {
            fpsDisplay.text = Mathf.Ceil(fps).ToString() + " FPS";
        }
    }
}
