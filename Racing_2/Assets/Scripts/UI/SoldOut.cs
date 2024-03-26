using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldOut : MonoBehaviour
{
    private Image Image;

    void Start()
    {
        Image = GetComponent<Image>();
    }

    void Update()
    {
        if(gameObject!=null)
            Image.fillAmount += Time.deltaTime * 30;
    }
}
