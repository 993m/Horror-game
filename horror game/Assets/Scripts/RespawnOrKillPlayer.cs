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
    GameObject text;
    TMP_Text chances;

    // Start is called before the first frame update
    void Start()
    {
        canBeSaved = false;
        text = GameObject.Find("Canvas").transform.Find("KillText").gameObject;
        chances = GameObject.Find("Canvas").transform.Find("NumberOfSaves").GetComponents<TMP_Text>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        chances.SetText(saveChances.ToString());

        if (Input.GetKeyDown(KeyCode.Space) && saveChances != 0)
        {
            lastSavedPosition = transform.position;
            saveChances--;
            canBeSaved = true;
        }


        // check if player wants to exit game
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public IEnumerator attackPlayer()
    {
        text.SetActive(true);

        if (canBeSaved)
        {
            respawnPlayer();
        }
        else
        {
            killPlayer();
        }

        yield return new WaitForSeconds(2);
        text.SetActive(false);
    }

    void killPlayer()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void respawnPlayer()
    {
        transform.position = lastSavedPosition;
        canBeSaved = false;
    }
}
