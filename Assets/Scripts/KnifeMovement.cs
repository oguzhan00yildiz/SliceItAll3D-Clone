using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector2 force;
  
    public float torque = 4f;
    float endTorque = 0f;


    public float rotationAmount;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        rotationAmount = transform.eulerAngles.x;
        

        if (Input.GetMouseButtonDown(0))
        {
            Flip();
        }
   
        if (transform.eulerAngles.x>=300f && transform.eulerAngles.x <360f )
        {
           
           rb.angularDrag = 1f;
        
        }
        else
        {
            rb.angularDrag=0.05f;
        }



    }
    

    private void Flip()
    {
        rb.angularVelocity=Vector3.zero;
        rb.AddForce(force, ForceMode.Impulse);
        
        rb.AddTorque(torque, 0f, 0f, ForceMode.Impulse);
        
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
       
        
    }
}
