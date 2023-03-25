using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStuck : MonoBehaviour
{
   public Rigidbody rb;
   
    private bool canMove = false;
    private bool isKnifeOnPlatform;

    KnifeMovement km;

    private void Start()
    {
        km = transform.gameObject.GetComponent<KnifeMovement>();

    }
    private void Update() 
    {
        

        if (Input.GetMouseButtonDown(0)) 
           {
            if (isKnifeOnPlatform) 
            {
                rb.isKinematic = false;
                isKnifeOnPlatform = false;
                km.Flip();
                StartCoroutine(Timer());
            }
           } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            rb.isKinematic = true;
            isKnifeOnPlatform=true;
        }
        else if (other.CompareTag("2x"))
        {
            rb.isKinematic = true;
            isKnifeOnPlatform=true;
        }
        else if (other.CompareTag("3x"))
        {
            rb.isKinematic = true;
            isKnifeOnPlatform=true;
        }
        else if (other.CompareTag("4x"))
        {
            rb.isKinematic = true;
            isKnifeOnPlatform=true;
        }
        else if (other.CompareTag("5x"))
        {
            rb.isKinematic = true;
            isKnifeOnPlatform=true;
        }
        else if (other.CompareTag("10x"))
        {
            rb.isKinematic = true;
            isKnifeOnPlatform=true;
        }

    }
    IEnumerator Timer()
    {
        Physics.IgnoreLayerCollision(6,7);
        yield return new WaitForSeconds(1f);
        Physics.IgnoreLayerCollision(6,7,false);  
    }

}
