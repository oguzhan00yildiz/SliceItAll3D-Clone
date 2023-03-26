using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHandle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanSlice")|| other.CompareTag("Platform")||other.CompareTag("CanSlicePen"))
        {
            KnifeMovement.knifeMovementInstance.PushBack();
        }
    }
}
