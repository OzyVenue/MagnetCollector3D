using UnityEngine;

public class ObstacleDebug : MonoBehaviour
{
    private Rigidbody[] childRBs;
    private void Start()
    {
        childRBs = GetComponentsInChildren<Rigidbody>();
    }
    //Debug menu methods for obstacle debugging
    public void FreeMove()
    {
        for(int i =0; i<childRBs.Length; i++)
        {
            childRBs[i].constraints = RigidbodyConstraints.None;
        }
    }
    public void NoMove()
    {
        for (int i = 0; i < childRBs.Length; i++)
        {
            childRBs[i].constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
