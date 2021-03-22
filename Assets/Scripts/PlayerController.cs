using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rB;
    [SerializeField] private GameManager gameManager;
    public bool gameCompleted;

    //rotation
    public bool rotationToFix;
    private Vector3 initialRotation;

    //movement
    private Vector3 firstPos;
    private Vector3 deltaPos;
    private Vector3 lateralVelocity;
    private int direction;
    [SerializeField] private float lateralSpeed = 0.5f;
    public float forwardSpeed;

    //for debug menu
    private float initialForwardSpeed;


    private void Start()
    {
        initialForwardSpeed = forwardSpeed;
        DOTween.Init();
        rB = GetComponent<Rigidbody>();
        initialRotation = transform.rotation.eulerAngles;
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!gameCompleted)
        {
            //checks if the player falls:
            FailCheck();
            MoveLateral();
            MoveForward();
            //player rotation can be fixed if the touch ends
            if (rotationToFix)
                FixRotation();
        }
    }
    
    private void MoveForward()
    {
        rB.velocity = Vector3.forward * Time.fixedDeltaTime * forwardSpeed + lateralVelocity + new Vector3(0, rB.velocity.y, 0);
    }

    private void MoveLateral()
    {

        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)&&firstPos!=Vector3.zero)
        {
            deltaPos = Input.mousePosition - firstPos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            deltaPos = Vector3.zero;
            firstPos = Vector3.zero;
            direction = 0;
            rotationToFix = true;
        }
        if (deltaPos.x > 0)
        {
            direction = 1;
        }
        if (deltaPos.x < 0)
        {
            direction = -1;
        }
        lateralVelocity = new Vector3(direction * lateralSpeed * Time.fixedDeltaTime, 0, 0);
    }
    //to fix the rotation when touch ends:
    private void FixRotation()
    {
        transform.DORotateQuaternion(Quaternion.Euler(initialRotation), .3f);
        rotationToFix = false;
    }
    private void FailCheck()
    {
        //-5f can be derived from the finishline's y component if more levels to be designed.
        if (transform.position.y < -5f)
            gameManager.gameOver = true;
    }

    //Debug menu methods:
    public void RegularMode()
    {
        rB.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    public void FreeStyle()
    {
        rB.constraints = RigidbodyConstraints.None;
    }
    public void SliderSpeed(float coefficient)
    {
        forwardSpeed = initialForwardSpeed*coefficient;
    }
}
