using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeStuck : MonoBehaviour
{
   public Rigidbody rb;
    private bool isStuck;

    private void Start()
    {
      
    }

    private void Update() {
        Debug.Log(rb.isKinematic);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag=="Platform")
        {
           rb.isKinematic=true; 
           isStuck=true;
        }
        
         if(Input.GetMouseButtonDown(0) && isStuck)
        {
            rb.isKinematic=false;
            //StartCoroutine(Timer());
            isStuck=false;
            
        }
        else
        {
            rb.isKinematic=false;
        }
        
        
    }

    IEnumerator Timer()
    {
        Physics.IgnoreLayerCollision(6,7);
        yield return new WaitForSeconds(0.1f);
        Physics.IgnoreLayerCollision(6,7,false);


    }

}
