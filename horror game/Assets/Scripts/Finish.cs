using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Finish : MonoBehaviour
{
    int trig = 0;
    public GameObject Object;
    // Start is called before the first frame update
    void Start()
    {
        Object.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrig(){
	trig = 1;
    }

    void OnTriggerEnter()
    {
	if(trig == 1){
	    //SceneManager.LoadScene("MainMenu");
	    Object.SetActive(true);
	}
    }
}
