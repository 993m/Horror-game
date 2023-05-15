using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintBar : MonoBehaviour
{
    public Image sprintBarSprite;
    public void UpdateSprintBar(float sprintTime, float sprintTimer)
    {
        sprintBarSprite.fillAmount = sprintTimer / sprintTime;
    }
}
