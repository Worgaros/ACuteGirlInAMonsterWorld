using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySetPlayerVictory : MonoBehaviour
{ 
    void OnTriggerStay2D(Collider2D other) {
        Application.Quit();
    }
}
