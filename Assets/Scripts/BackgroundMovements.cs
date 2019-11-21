using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovements : MonoBehaviour {

    [SerializeField] Transform target_;

    [SerializeField] float speed;

    
    void Update() {
        
        transform.position = target_.position;
        float xPos = target_.position.x * speed;
        float yPos = target_.position.y * speed;
        float zPos = 0;
        transform.position = new Vector3(xPos, yPos, zPos);
    }
}