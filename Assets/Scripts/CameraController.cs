using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<GameObject> cameras;

    private void Start()
    {
        SetCamera((int)CamerasNumber.CAM_NORMAL);
    }

    public void SetCamera(int cameraNumber)
    {
        foreach (var cam in cameras)
            cam.SetActive(false);

        cameras[cameraNumber].SetActive(true);
    }
}
