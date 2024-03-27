using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{

    void Update()
    {
        transform.eulerAngles += new Vector3(0, 60 * Time.deltaTime, 0);
    }
}
