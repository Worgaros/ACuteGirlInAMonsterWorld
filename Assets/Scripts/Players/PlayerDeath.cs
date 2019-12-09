using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private const float deathPos = -5.495f;
    private void Update()
    {
        if (transform.position.y <= deathPos)
        {
            Application.Quit();
        }
    }
}

    