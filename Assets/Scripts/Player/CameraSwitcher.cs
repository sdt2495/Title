using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Transform cam; // Main Camera
    private bool isFPS = false;

    // TPS ‚Æ FPS ‚ÌˆÊ’u
    public Vector3 tpsPos = new Vector3(0, 2, -4);
    public Vector3 fpsPos = new Vector3(0, 1.2f, 0.2f);

    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        cam.localPosition = tpsPos;
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            isFPS = !isFPS;

            if (isFPS)
                cam.localPosition = fpsPos;
            else
                cam.localPosition = tpsPos;
        }
    }
}
