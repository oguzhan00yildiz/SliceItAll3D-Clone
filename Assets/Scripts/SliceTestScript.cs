using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceTestScript : MonoBehaviour
{
    public Material materialSlicedSide;
    public float explosionForce;
    public float explosionRadius;
    public bool gravity , kinematic;

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("CanSlice"))
        {
            SlicedHull sliceObj = Slice(other.gameObject, materialSlicedSide);
            GameObject SlicedObjUp = sliceObj.CreateUpperHull(other.gameObject, materialSlicedSide);
            GameObject SlicedObjLow = sliceObj.CreateLowerHull(other.gameObject, materialSlicedSide);
            Destroy(other.gameObject);
            AddComponent(SlicedObjUp);
            AddComponent(SlicedObjLow);
        }
    }


    private SlicedHull Slice(GameObject obj, Material mat)
    {
        return obj.Slice(transform.position, transform.right, mat);
    }

    void AddComponent(GameObject obj)
    {
        obj.AddComponent<BoxCollider>();
        var rigidBody = obj.AddComponent<Rigidbody>();
        rigidBody.useGravity = gravity;
        rigidBody.isKinematic = kinematic;
        rigidBody.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius);
        obj.tag = "CanSlice";
    }
}
