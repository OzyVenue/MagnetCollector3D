using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    [SerializeField] private float forceFactor=2000f;
    private List<Rigidbody> rbBalls;
    public bool gameCompleted;
    private void Start()
    {
        rbBalls = new List<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rbBalls != null && !gameCompleted)
        {
            MagnetEffect();
        }
    }
    private void MagnetEffect()
    {
        foreach(Rigidbody ballRB in rbBalls)
        {
            ballRB.AddForce((transform.position - ballRB.position) * forceFactor * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if(other.TryGetComponent(out Rigidbody rb))
                rbBalls.Add(rb);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if(other.TryGetComponent(out Rigidbody rb))
                rbBalls.Remove(rb);
        }
    }
}
