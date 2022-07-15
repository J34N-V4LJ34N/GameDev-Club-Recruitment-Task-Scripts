using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCController : MonoBehaviour
{
    private float currentSpeed = 12.5f;
    public AudioSource horn;
    public float speed = 12.5f;
    public float rotationSpeed = 1220;
    public float stopDistance = .1f;
    public Vector3 destination;
    public Animator animator;
    public bool reachedDestination;

    private Vector3 lastPosition;
    Vector3 velocity;

    private void Awake()
    {
        currentSpeed = speed;
        animator = GetComponent<Animator>();
        horn.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;
            if(destinationDistance >=stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,rotationSpeed*Time.deltaTime);
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination=true;
            }
            velocity=(transform.position-lastPosition)/Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            velocity = velocity.normalized;
            var fwdDotProduct=Vector3.Dot(transform.forward,velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);
            if (animator != null)
            {
                animator.SetFloat("Forward", fwdDotProduct);
                animator.SetFloat("Horizontal", rightDotProduct);
            }
        }
        lastPosition=transform.position;
    }
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC"|| other.gameObject.tag == "MainPlayer")
        {

            horn.Play();
            currentSpeed = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        horn.Pause();
        currentSpeed = speed;
    }
    private void OnCollisionEnter(Collision collision)
    {

        horn.Pause();
        Destroy(transform.gameObject);
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    speed = 12.5f;
    //}
}
