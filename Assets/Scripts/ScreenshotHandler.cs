using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    [Header("Game View Resolution")]
    public int width = Screen.width;
    public int height = Screen.height;

    [Header("Instructions")]
    [TextArea(3, 6)]
    public string _ =
        "1. Set your resolution in the Game view (reflected above). If the resolution is > 10k in any direction use scale option below. \n2. Set the filename for the screenshot. \n3. Press Play and press 'K' to take a screenshot.";

    [Header("Options")]
    [Tooltip("The size multiplier for the screenshot. 1 = normal size, 2 = double size, etc.")]
    public int scaleMultiplier = 1;

    [Tooltip("The filename for the screenshot + '.png'")]
    public string filename = "screenshot";

    [Header("Final Resolution")]
    public int finalWidth = 0;
    public int finalHeight = 0;

    void OnValidate()
    {
        UnityEditor.PlayModeWindow.GetRenderingResolution(out uint _width, out uint _height);
        width = (int)_width;
        height = (int)_height;
        // Debug.Log($"Editor Render Resolution: {width}x{height}");

        finalWidth = width * scaleMultiplier;
        finalHeight = height * scaleMultiplier;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Saves "screenshot.png" to your project's root folder
            // Optional: Multiply size for high-res (e.g., 2 = 2x resolution)
            ScreenCapture.CaptureScreenshot(filename + ".png", scaleMultiplier);
        }
    }
}
