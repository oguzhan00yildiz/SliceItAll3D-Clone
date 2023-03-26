using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStuck : MonoBehaviour
{
   [SerializeField] private Rigidbody rb;
   
    private bool isKnifeOnPlatform;

    public bool isLevelDone;

    KnifeMovement km;

    static public KnifeStuck KnifeStuckInstance;

    private void Start()
    {
        km = transform.gameObject.GetComponent<KnifeMovement>();
        KnifeStuckInstance = this;
        isLevelDone = false;
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
            this.GetComponent<KnifeMovement>().enabled = false;
            isLevelDone = true;
        }
        else if (other.CompareTag("3x"))
        {
            rb.isKinematic = true;
            this.GetComponent<KnifeMovement>().enabled = false;
            isLevelDone = true;
        }
        else if (other.CompareTag("4x"))
        {
            rb.isKinematic = true;
            this.GetComponent<KnifeMovement>().enabled = false;
            isLevelDone = true;
        }
        else if (other.CompareTag("5x"))
        {
            rb.isKinematic = true;
            this.GetComponent<KnifeMovement>().enabled = false;
            isLevelDone = true;
        }
        else if (other.CompareTag("10x"))
        {
            rb.isKinematic = true;
            this.GetComponent<KnifeMovement>().enabled = false;
            isLevelDone = true;
        }
        else if (other.CompareTag("1x"))
        {
            rb.isKinematic = true;          
            this.GetComponent<KnifeMovement>().enabled = false;
            isLevelDone = true;
        }

    }
    IEnumerator Timer()
    {
        Physics.IgnoreLayerCollision(6,7);
        yield return new WaitForSeconds(1f);
        Physics.IgnoreLayerCollision(6,7,false);  
    }

}
