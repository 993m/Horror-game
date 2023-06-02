using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RespawnOrKillPlayer : MonoBehaviour
{
    public int saveChances = 3;
    Vector3 lastSavedPosition;
    bool canBeSaved;
    TriggerText triggerText;
    public GameObject printSavesCanvas;
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        canBeSaved = false;
        triggerText = GetComponent<TriggerText>();
        text = printSavesCanvas.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText(saveChances.ToString());

        if (Input.GetKeyDown("space") && saveChances != 0)
        {
            lastSavedPosition = transform.position;
            saveChances--;
            canBeSaved = true;
        }
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(3);
    }

    public void attackPlayer()
    {
        triggerText.OnTriggerEnter();
        WaitForFunction();
        triggerText.OnTriggerExit();

        if (canBeSaved)
        {
            respawnPlayer();
        }
        else
        {
            killPlayer();
        }
    }

    void killPlayer()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void respawnPlayer()
    {
        canBeSaved = false;
        transform.position = lastSavedPosition;
    }
}
