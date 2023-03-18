using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 force;
    public Vector3 forceB;
  
    public float torque = 4f;
    public float torqueB = 4f;
    float endTorque = 0f;


    public float rotationAmount;

    [SerializeField] private float maxHorizontalVelocity=3f,minDegree,maxDegree;
    private float rotX;
    private float rotY;

    private bool angDrag=false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
   
    }

    void Update()
    {
        Move();
    
    }
    public void Flip()
    {
        rb.velocity=new Vector3(0,0,0);
        rb.angularVelocity=Vector3.zero;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(torque, 0f, 0f, ForceMode.Impulse);
    }

    IEnumerator Timer()
    {
        angDrag=false;
        rb.angularDrag=0.05f;
        yield return new WaitForSeconds(1f);
        angDrag=true; 
    }

    private void Move()
    {
        var x = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;

        if (rb.velocity.magnitude > maxHorizontalVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxHorizontalVelocity;
        }
        rotationAmount = transform.eulerAngles.x;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Timer());
            Flip();
        }
        else
        {
            if (x>=minDegree && x <maxDegree && angDrag ) 
            {
                rb.angularDrag = 7f;
            }
        }


    }


    public void PushBack()
    {
            rb.velocity=new Vector3(0,0,0);
            rb.AddForce(forceB, ForceMode.Impulse);
    }
      
   
}
