using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    public HurdleSystem HurdleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
            HurdleSystem.bFindPlayer = true;
    }
}
