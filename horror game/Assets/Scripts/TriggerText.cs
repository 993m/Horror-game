using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{

    public GameObject Object;
    // Start is called before the first frame update
    void Start()
    {
        Object.SetActive(false);
    }


    public void OnTriggerEnter()
    {
        Object.SetActive(true);

    }

    public void OnTriggerExit()
    {
        Object.SetActive(false);
    }

}