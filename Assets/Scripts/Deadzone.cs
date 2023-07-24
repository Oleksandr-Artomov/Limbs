using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;

    private void OnTriggerEnter2D(Collider2D collide)
    {
        if(collide.CompareTag(("Deadzone")))
        {
            transform.position = respawnPoint.transform.position;
        }
    }
}
