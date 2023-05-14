using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] GameObject tu;
    [SerializeField] GameObject dungeon;

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
	StartCoroutine("TeleportRoutine");
    }
    
    IEnumerator TeleportRoutine(){
	
        player.disabled = true;
	yield return new WaitForSeconds(0.2f);
        player.transform.position = dungeon.transform.position;
	yield return new WaitForSeconds(0.2f);
	player.disabled = false;
    }
}
