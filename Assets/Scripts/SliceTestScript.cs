using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using TMPro;

public class SliceTestScript : MonoBehaviour
{
    public Material materialSlicedSide;
    public float explosionForceNear, explosionForceFar;
    public float explosionRadius;
    public bool gravity , kinematic;
    public int score;

    public static SliceTestScript SliceTestScriptInstant;
    public TMP_Text popUpScore;

    void Start()
    {
        SliceTestScriptInstant = this;
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CanSlice")
        {
            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject SlicedObjUp = sliceObj.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject SlicedObjLow = sliceObj.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);
            AddComponent(SlicedObjUp , explosionForceNear);
            AddComponent(SlicedObjLow , explosionForceFar);
            score++;
            Instantiate(popUpScore, transform.position, Quaternion.Euler(0, -60, 0));
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
