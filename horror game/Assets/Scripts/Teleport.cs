using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    PlayerMovement player;
    int ct = 0;
    [SerializeField] GameObject tu;
    [SerializeField] GameObject dungeon;
    [SerializeField] GameObject text;
    [SerializeField] GameObject textD;

    // Start is called before the first frame update
    void Start()
    {
	
        player = tu.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
	
    }

    void OnTriggerEnter(){
	if(ct == 0)
	{
	    EnemySpawner spawn = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemySpawner>();
	    Finish fin = GameObject.FindGameObjectWithTag("Finish").GetComponent<Finish>();
	    spawn.Spawn();
	    fin.SetTrig();
	    StartCoroutine("TeleportRoutine");
	    
	    ct = 1;
	}
    }
    
    IEnumerator TeleportRoutine(){
	
        player.disabled = true;
	yield return new WaitForSeconds(0.2f);
        player.transform.position = dungeon.transform.position;
	yield return new WaitForSeconds(0.2f);
	player.disabled = false;
        textD.SetActive(false);
	text.SetActive(true);
	textD.SetActive(false);
	yield return new WaitForSeconds(10f);
	text.SetActive(false);
	textD.SetActive(true);
    }
}
