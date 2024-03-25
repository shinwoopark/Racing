using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckSpawn : MonoBehaviour
{
    public SpawnManager SpawnManager;

    public Transform SpawnPos;
    public float SpawnDir;

    public int Number;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            SpawnManager.Spawn(Number, SpawnPos.position, SpawnDir);
    }
}
