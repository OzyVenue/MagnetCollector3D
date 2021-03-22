using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 cameraOffset;

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        cameraOffset = transform.position - playerTransform.position;
    }

   
    private void LateUpdate() {
        transform.position = playerTransform.position + cameraOffset;
    }
#region Debug menu methods:
    
    public void TopView()
    {
        cameraOffset = new Vector3(0, 10f, -3f);
        transform.rotation = Quaternion.Euler(55, 0, 0);
    }
    public void MidView()
    {
        cameraOffset = new Vector3(0, 7f, -4.4f);
        transform.rotation = Quaternion.Euler(44, 0, 0);
    }
#endregion
}
