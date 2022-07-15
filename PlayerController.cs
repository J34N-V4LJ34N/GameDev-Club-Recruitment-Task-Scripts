using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float speed;
    public float normalSpeed;
    public float boostedspeed;
    public float rotationSpeed;
    public float acceleration;
    public GameObject checkpoints;
    public GameObject endMenuUI;
    public TextMeshProUGUI reason;
    // public float rotationAcceleration;
    public float currentSpeed = 0.0f;
    // public float currentRotationSpeed = 0.0f;

    private void Start()
    {
        speed = normalSpeed;
    }
    void Update()
    {
        speed = Input.GetKey(KeyCode.Space)?0: Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? boostedspeed : normalSpeed; 
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxis("Horizontal");
        float t = Time.deltaTime;

        if (v == 1)
        {
            currentSpeed = currentSpeed < speed ? currentSpeed + (acceleration * t) : speed;
        }
        else if (v == -1)
        {
            currentSpeed = currentSpeed > -speed ? currentSpeed - (acceleration * t) : -speed;
        }
        else
        {
            currentSpeed = currentSpeed > 0 ? currentSpeed - (1.5f * acceleration * t) : currentSpeed < 0 ? currentSpeed + (1.5f * acceleration * t) : 0;
        }

        // if (h == 1)
        // {
        //     currentRotationSpeed = currentRotationSpeed < rotationSpeed ? currentRotationSpeed + (rotationAcceleration * t) : rotationSpeed;
        // }
        // else if (h == -1)
        // {
        //     currentRotationSpeed = currentRotationSpeed > -rotationSpeed ? currentRotationSpeed - (rotationAcceleration * t) : -rotationSpeed;
        // }
        // else
        // {
        //     currentRotationSpeed = currentRotationSpeed > 0 ? currentRotationSpeed - (1.5f * rotationAcceleration * t) : currentRotationSpeed < 0 ? currentRotationSpeed + (1.5f * rotationAcceleration * t) : 0;
        // }

        float translation = currentSpeed;
        float rotation = h * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= t;
        rotation *= t;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "Checkpoint")
        {
            other.gameObject.SetActive(false);
        }
        //else if (other.gameObject.tag == "NPC")
        //{

        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NPC"|| collision.gameObject.tag == "Obstacles"|| collision.gameObject.tag == "Cones")
        {
            Time.timeScale = 0f;
            endMenuUI.SetActive(true);
            reason.text = collision.gameObject.tag == "NPC"?"You crashed into a car!": collision.gameObject.tag == "Cones" ? "You crashed into a traffic cone!": "You crashed into a building!";
        }
    }
}