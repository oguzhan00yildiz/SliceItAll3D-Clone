using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("Jump Forth")]
    [SerializeField] private Vector3 _jumpForthForce;
    [SerializeField] private Vector3 _spinForthTorque;
    [Header("Jump Back")]
    [SerializeField] private Vector3 _jumpBackForce;
    [SerializeField] private Vector3 _spinBackTorque;
    
    private Rigidbody _rigidbody;
    public Transform rbt;
    [SerializeField] private bool hasCollided;
    private void Awake()
    {
       _rigidbody= rbt.transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rigidbody.isKinematic = false;
            
            Jump();
            Spin();
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform" && !hasCollided)
    {
        hasCollided = true;
        Debug.Log(other.tag);
        Stuck(); 
        StartCoroutine(Timer());
    }
    }
    
    

    private void Jump(int direction = 1)
    {
        Vector3 jumpForce = direction == 1 ? _jumpForthForce : _jumpBackForce;
        
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        
    }

    private void Spin(int direction = 1)
    {
        Vector3 spinTorque = direction == 1 ? _spinForthTorque : _spinBackTorque;
        
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddTorque(spinTorque, ForceMode.Acceleration);
    }

   private void Stuck()
    {
        _rigidbody.isKinematic = true;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        hasCollided = false;
    }
}
