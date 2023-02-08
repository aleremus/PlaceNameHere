using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 cameraOffset = new Vector3(12, 10, 0);
    private Vector3 cameraOffsetRatio = new Vector3(1, 2, 0);
    private Vector3 cameraRotation = new Vector3(50, -90, 0);

    private const float minCameraOffset = 5;
    private const float maxCameraOffset = 15;
    private float targetCameraOffset;

    [SerializeField, Range(0.1f, 1)] float scrollSensitiviety = .5f;
    [SerializeField] float cameraOffsetSpeed = 30;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position + cameraOffset;
        transform.rotation = Quaternion.Euler(cameraRotation);
        targetCameraOffset = cameraOffset.y;
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        if(Input.mouseScrollDelta.y != 0)
        {
            targetCameraOffset -= Input.mouseScrollDelta.y * scrollSensitiviety;
            if (targetCameraOffset < minCameraOffset)
            {
                targetCameraOffset = minCameraOffset;
            }

            if (targetCameraOffset > maxCameraOffset)
            {
                targetCameraOffset = maxCameraOffset;
            }
        }

        cameraOffset.y = Mathf.SmoothStep(cameraOffset.y, targetCameraOffset, Time.deltaTime * cameraOffsetSpeed);

        transform.position = player.transform.position + cameraOffset;
        transform.LookAt(player.transform.position);
        
    }
}
