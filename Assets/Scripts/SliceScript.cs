using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using TMPro;

public class SliceScript : MonoBehaviour
{
    public Material materialSlicedSide;
    private int tempIndex = 0;
    private int materialIndex;
    [SerializeField] private float explosionForceNear, explosionForceFar;
    [SerializeField] private float explosionRadius;
    [SerializeField] private bool gravity , kinematic;
    public int score;

    public Material[] materials = new Material[9];
    
    public static SliceScript SliceTestScriptInstant;
    [SerializeField]  private TMP_Text popUpScore;

    [SerializeField] private GameObject Splash;

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
            Color objectColor = other.GetComponent<Renderer>().material.color;         
            var tempSplash=Instantiate(Splash,SlicedObjUp.transform.position,Quaternion.Euler(-45,0,0));
            var tempSplashSettings = tempSplash.GetComponent<ParticleSystem>().main;
            tempSplashSettings.startColor =objectColor;
            Destroy(tempSplash,1f);
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
            score++;
            Instantiate(popUpScore, SlicedObjUp.transform.position + new Vector3(0,0,0), Quaternion.Euler(0, -60, 0));
            Color objectColor = other.GetComponent<Renderer>().material.color;
            var tempSplash=Instantiate(Splash,SlicedObjUp.transform.position,Quaternion.Euler(-45,0,0));
            var tempSplashSettings = tempSplash.GetComponent<ParticleSystem>().main;
            tempSplashSettings.startColor =objectColor;
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
        yield return new WaitForSeconds(10f);
        Destroy(UpHull);
        Destroy(LowHull);
    }
}
