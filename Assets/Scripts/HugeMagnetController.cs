using System.Collections.Generic;
using UnityEngine;

public class HugeMagnetController : MonoBehaviour
{
    [SerializeField] private float forceFactor = 500f;
    private List<Rigidbody> rbBalls;
    private void Start()
    {
        rbBalls = new List<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rbBalls != null)
        {
            MagnetEffect();
        }
    }
    private void MagnetEffect()
    {
        foreach (Rigidbody ballRb in rbBalls)
        {
            ballRb.AddForce((transform.position - ballRb.position) * forceFactor * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        if(other.TryGetComponent(out Rigidbody rb))
        {
            if(!rbBalls.Contains(rb))
                rbBalls.Add(rb);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        if(other.TryGetComponent(out Rigidbody rb))
        {
            if(rbBalls.Contains(rb))
                rbBalls.Remove(rb);
        }
    }
}
