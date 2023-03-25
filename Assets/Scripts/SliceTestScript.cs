using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using TMPro;

public class SliceTestScript : MonoBehaviour
{
    public Material materialSlicedSide;

    private int tempIndex = 0;
    private int materialIndex;
    public float explosionForceNear, explosionForceFar;
    public float explosionRadius;
    public bool gravity , kinematic;
    public int score;

    public Material[] materials = new Material[9];

    

    public static SliceTestScript SliceTestScriptInstant;
    public TMP_Text popUpScore;

    void Start()
    {
        SliceTestScriptInstant = this;
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("CanSlice"))
        {
            materialIndex = Random.Range(1,9);
            
            if(tempIndex != materialIndex){
                materialSlicedSide = materials[materialIndex];
                tempIndex = materialIndex;
            }
            else
            {
                materialIndex = Random.Range(1,9);
                materialSlicedSide = materials[materialIndex];
            }
            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject SlicedObjUp = sliceObj.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject SlicedObjLow = sliceObj.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);
            AddComponent(SlicedObjUp , explosionForceNear);
            AddComponent(SlicedObjLow , explosionForceFar);
            score++;
            Instantiate(popUpScore, SlicedObjUp.transform.position + new Vector3(3,0,-3), Quaternion.Euler(0, -60, 0));
            StartCoroutine(DestroyHulls(SlicedObjUp, SlicedObjLow));
        }
        else if(other.gameObject.CompareTag("CanSlicePen"))
        {   
            materialSlicedSide = materials[Random.Range(1,9)];
            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject SlicedObjUp = sliceObj.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject SlicedObjLow = sliceObj.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);
            AddComponent(SlicedObjUp , explosionForceNear);
            //AddComponent(SlicedObjLow , explosionForceFar);
            score++;
            Instantiate(popUpScore, SlicedObjUp.transform.position + new Vector3(0,0,0)/*transform.position*/, Quaternion.Euler(0, -60, 0));
            StartCoroutine(DestroyHulls(SlicedObjUp, SlicedObjLow));
            
        }
    }


    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.right, mat);
    }

    void AddComponent(GameObject obj , float expForce)
    {
        obj.AddComponent<BoxCollider>();
        var rigidBody = obj.AddComponent<Rigidbody>();
        rigidBody.useGravity = gravity;
        rigidBody.isKinematic = kinematic;
        rigidBody.AddExplosionForce(expForce, obj.transform.position, explosionRadius); 
    }

    IEnumerator DestroyHulls(GameObject UpHull, GameObject LowHull)
    {
        yield return new WaitForSeconds(6f);
        Destroy(UpHull);
        Destroy(LowHull);
    }
}
