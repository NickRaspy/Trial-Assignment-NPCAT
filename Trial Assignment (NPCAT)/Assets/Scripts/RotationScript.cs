using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float cameraMoveSpeed = 1f;
    public bool isInComponent;
    [Header("Sphere Collider Limit")]
    public float standartColliderRadius;
    public float minColliderRadius;
    public float maxColliderRadius;
    public float StandartColliderRadius
    {
        get { return standartColliderRadius; }
        set { standartColliderRadius = value; }
    }
    public float MaxColliderRadius
    {
        get { return maxColliderRadius; }
        set { maxColliderRadius = value; }
    }
    public float MinColliderRadius
    {
        get { return minColliderRadius; }
        set { minColliderRadius = value; }
    }
    [Space]
    public GameObject camera;
    void OnMouseDrag()
    {
        if(!this.enabled)
        {
            return;
        }
        float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
        // select the axis by which you want to rotate the GameObject
        transform.Rotate(Vector3.up, XaxisRotation);
        transform.Rotate(Vector3.left, YaxisRotation);
    }
    private void OnGUI()
    {
        Vector3 cameraPos = camera.transform.localPosition + new Vector3(0f, 0f, Input.mouseScrollDelta.y * cameraMoveSpeed * Time.deltaTime);
        cameraPos.z = Mathf.Clamp(cameraPos.z, -maxColliderRadius - 1f, -minColliderRadius - 1f);
        camera.transform.localPosition = cameraPos;
        GetComponent<SphereCollider>().radius = Mathf.Clamp(GetComponent<SphereCollider>().radius - Input.mouseScrollDelta.y * cameraMoveSpeed * Time.deltaTime, minColliderRadius, maxColliderRadius);
    }
}
