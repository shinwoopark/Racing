using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyItem : MonoBehaviour
{
    public int Money;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameInstance.instance.CurrentMoney += Money;
            Destroy(gameObject);
        }
    }
}
