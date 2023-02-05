using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disable : MonoBehaviour
{
    public GameObject Object;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void whenButtonClicked()
    {
        Object.SetActive(false);
    }
}
