using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    // Update is called once per frame
    void Update()
    { // Destroy zone is move to player x position
        transform.position = new Vector2(player.transform.position.x, 0);
    }    
}
