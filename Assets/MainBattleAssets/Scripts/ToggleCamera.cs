using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    [Header("Cameras to Toggle")]
    public Camera topDownCam;
    public Camera pilotCam;

    void Start()
    {
        // ensure initial state
        topDownCam.gameObject.SetActive(true);
        pilotCam.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ToggleCameras();
    }

    void ToggleCameras()
    {
        bool usingTopDown = topDownCam.gameObject.activeSelf;

        topDownCam.gameObject.SetActive(!usingTopDown);
        pilotCam.gameObject.SetActive(usingTopDown);
    }
}