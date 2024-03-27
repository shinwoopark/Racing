using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOn : MonoBehaviour
{
    private Image Image;

    public bool bLockOff;

    void Start()
    {
        Image = GetComponent<Image>();
    }

    void Update()
    {
        Debug.Log(bLockOff);

        if (bLockOff)
            Image.fillAmount -= Time.unscaledDeltaTime * 3;
        
            

        if (Image.fillAmount <= 0)
            Destroy(gameObject);
    }
}
