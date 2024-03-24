using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterItem : MonoBehaviour
{
    public int BoosterNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameInstance.instance.CurrentInventorys[0] == 0)
            {
                GameInstance.instance.CurrentInventorys[0] = BoosterNumber;
            }
            else if (GameInstance.instance.CurrentInventorys[1] == 0)
            {
                GameInstance.instance.CurrentInventorys[1] = BoosterNumber;
            }
            else
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}
