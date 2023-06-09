using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    public static KnifeMovement knifeMovementInstance;
    private Rigidbody rb;
    public Vector3 force;
    public Vector3 forceB;
    [SerializeField] private float torque = 4f;
    private float realRotationAmount;
    private Color originalColor;
    public float rotationAmount;
    [SerializeField] private float maxHorizontalVelocity=3f,minDegree,maxDegree;
    private float rotX;
    private float rotY;
    private bool canMove;
    private bool angDrag=false;
    public bool isFailed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        knifeMovementInstance=this;
        originalColor = transform.GetChild(2).GetComponent<Renderer>().material.color;
        isFailed = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canMove =true;
        }
    }

    void FixedUpdate()
    {
           if (canMove)
        {
            StartCoroutine(Timer());
            Flip();
            canMove = false;
        }
        else
        {
            if (realRotationAmount>=minDegree && realRotationAmount <maxDegree && angDrag ) 
            {
                rb.angularDrag = 7f;
            }
        }
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
        yield return new WaitForSeconds(.3f);
        angDrag=true; 
    }

    private void Move()
    {
        realRotationAmount = UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
        if (rb.velocity.magnitude > maxHorizontalVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxHorizontalVelocity;
        }
        rotationAmount = transform.eulerAngles.x;
    }

    public void PushBack()
    {
        StartCoroutine(Flash());
        rb.velocity=new Vector3(0,0,0);
        rb.AddForce(forceB, ForceMode.Impulse);  
    }

    private IEnumerator Flash()
    {
       transform.GetChild(2).GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
       transform.GetChild(2).GetComponent<Renderer>().material.color=originalColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FailGround"))
        {
            rb.isKinematic = true;
            isFailed = true;
            this.GetComponent<KnifeMovement>().enabled = false;
        }
    }  
   
}
