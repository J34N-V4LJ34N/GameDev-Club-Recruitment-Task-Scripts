using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;
    private void RescaleCamera()
    {

        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;

        float targetaspect = 1;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 0.3f;
            rect.height = scaleheight*30/100;
            rect.x = 0.7f;
            rect.y = 1-rect.height;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth*30/100;
            rect.height = 0.3f;
            rect.x = 1-rect.width;
            rect.y = 0.7f;

            camera.rect = rect;
        }

        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }

    void Start()
    {
        RescaleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        RescaleCamera();
    }

    private void LateUpdate()
    {

        HandleTranslation();
        //HandleRotation();
    }

    private void HandleTranslation()
    {
        //var targetPosition = target.TransformPoint(offset);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
        Vector3 newPosition = target.position;
        newPosition.y=transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
    }
    //private void HandleRotation()
    //{
    //    var direction = target.position - transform.position;
    //    var rotation = Quaternion.LookRotation(direction, Vector3.up);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    //}
}