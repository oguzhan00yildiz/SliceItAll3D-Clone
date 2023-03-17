using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 force;
  
    public float torque = 4f;
    float endTorque = 0f;


    public float rotationAmount;

    [SerializeField] private float maxHorizontalVelocity=3f;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
   
    }

    void Update()
    {
        if (rb.velocity.magnitude > maxHorizontalVelocity)
    {
        rb.velocity = rb.velocity.normalized * maxHorizontalVelocity;
    }

        rotationAmount = transform.eulerAngles.x;
        

        if (Input.GetMouseButtonDown(0))
        {
            Flip();
        }
   
        if (transform.eulerAngles.x>=300f && transform.eulerAngles.x <410f ) //when its is applying increase vertical force
        {
           
           rb.angularDrag = 1.4f;
        
        }
        else
        {
            rb.angularDrag=0.05f;
        }



    }
    

    public void Flip()
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
