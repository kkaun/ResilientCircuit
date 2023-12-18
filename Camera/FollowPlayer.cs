using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public float height = 1f;
    public float distance = 2f;

    public float horizontalInput;
    public float turnSpeed = 90f;

    private CameraDirection currentCameraDirection;

    private static float baseCameraHeightY = 1.5f;
    private static float baseCameraSideOffset = 12f;

    private Vector3 northCameraOffsetX = new Vector3(0, baseCameraHeightY, -baseCameraSideOffset);
    private Vector3 eastCameraOffsetX = new Vector3(-baseCameraSideOffset, baseCameraHeightY,0);
    private Vector3 southCameraOffsetX = new Vector3(0, baseCameraHeightY, baseCameraSideOffset);
    private Vector3 westCameraOffsetX = new Vector3(baseCameraSideOffset, baseCameraHeightY, 0);

    private Quaternion northRotationY = Quaternion.Euler(0, 0, 0);
    private Quaternion eastRotationY = Quaternion.Euler(0, 90f, 0);
    private Quaternion southRotationY = Quaternion.Euler(0, 180f, 0);
    private Quaternion westRotationY = Quaternion.Euler(0, 270f, 0);

    private Vector3 cameraOffsetX;
    private Quaternion cameraRotation;

    void Start()
    {
        currentCameraDirection = CameraDirection.NORTH;

        transform.position = northCameraOffsetX;
        transform.rotation = northRotationY;
    }

    void LateUpdate()
    {
        switch (currentCameraDirection)
        {
            case CameraDirection.WEST:
                cameraOffsetX = westCameraOffsetX;
                cameraRotation = westRotationY;
                break;
            case CameraDirection.EAST:
                cameraOffsetX = eastCameraOffsetX;
                cameraRotation = eastRotationY;
                break;
            case CameraDirection.SOUTH:
                cameraOffsetX = southCameraOffsetX;
                cameraRotation = southRotationY;
                break;
            default:
                cameraOffsetX = northCameraOffsetX;
                cameraRotation = northRotationY;
                break;
        }

        transform.position = player.transform.position + cameraOffsetX;
        transform.rotation = cameraRotation;
    }

    public void SetCameraDirection(CameraDirection direction)
    {
        currentCameraDirection = direction;
    }
}
