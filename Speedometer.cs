using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public GameObject target;

    public float maxSpeed = 40.0f; // The maximum speed of the target ** IN KM/H **

    public float minSpeedArrowAngle=45.0f;
    public float maxSpeedArrowAngle=-225.0f;

    public RectTransform arrow; // The arrow in the speedometer

    private void Update()
    {
        if (arrow != null)
            arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, target.GetComponent<PlayerController>().currentSpeed / maxSpeed));
    }
}