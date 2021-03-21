using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaGate : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Transform targetPosition;
    public float durationEnter;
    public int directionEnter;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CloseArena();
            StartCoroutine(playerMovement.LerpPosition(targetPosition.position, durationEnter, directionEnter));
        }
    }

    void CloseArena()
    {
        anim.Play("DoorClosed");
    }

}
