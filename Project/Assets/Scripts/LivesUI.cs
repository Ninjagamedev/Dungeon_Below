using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{

    public Text livesText;
    void Update()
    {
        livesText.text = PlayerStats.Lives.ToString();
    }
}
