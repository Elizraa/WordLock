using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameState;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerControl playerControl;
    Vector2 movement;

    public float dashSpeed, startDashTime, dashEnergy, restoreTimeDash;
    private float dashTime, currentEnergy;
    private int directionDash;


    public Transform boss;

    enum DirPlayer
    {
        up = 0,
        down = 1,
        right = 2,
        left = 3
    }

    int playerDir;

    // Start is called before the first frame update
    void Start()
    {
        playerDir = (int) DirPlayer.up;
        currentEnergy = dashEnergy;
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.levelManager.sceneState != SceneState.Talking)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            playerDir = Direction();
            anim.SetFloat("Direction", playerDir);
            if (LevelManager.levelManager.sceneState == SceneState.Fighting)
                anim.SetBool("Walking", true);
            else if(LevelManager.levelManager.sceneState == SceneState.Normal)
            {
                if(movement.sqrMagnitude != 0)
                    anim.SetBool("Walking", true);
                else
                    anim.SetBool("Walking", false);
            }
            if (Input.GetButton("Jump") && currentEnergy >= dashEnergy)
                Dash();
            if (currentEnergy < dashEnergy)
            {
                if (dashTime <= 0)
                {
                    dashTime = startDashTime;
                    moveSpeed = 5;
                    playerControl.invul = false;
                }
                else
                    dashTime -= Time.deltaTime;
                currentEnergy += (Time.fixedDeltaTime/restoreTimeDash);
                if (currentEnergy > dashEnergy)
                    currentEnergy = dashEnergy;
                //Debug.Log(currentEnergy);
                //PlayerUI.playerUI.UpdateEnergy(currentEnergy);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //Debug.Log(playerDir);
    }

    int Direction()
    {
        if (LevelManager.levelManager.sceneState == SceneState.Normal)
        {
            if (movement.y < 0)
                return (int)DirPlayer.down;
            else if (movement.y > 0)
                return (int)DirPlayer.up;
            else if (movement.x > 0)
                return (int)DirPlayer.right;
            else if (movement.x < 0)
                return (int)DirPlayer.left;
        }
        else if(LevelManager.levelManager.sceneState == SceneState.Fighting){
            Vector2 dir = (boss.position - transform.position).normalized;
            if (dir.x < 0.7 && dir.x > -0.7 && dir.y > 0.7)
                return (int)DirPlayer.up;
            else if (dir.x < 0.7 && dir.x > -0.7 && dir.y < -0.7)
                return (int)DirPlayer.down;
            else if (dir.x > 0.7 && dir.y > -0.7)
                return (int)DirPlayer.right;
            else if (dir.x < -0.7 && dir.y > -0.7)
                return (int)DirPlayer.left;
        }

        return playerDir;
    }

    void Dash()
    {
        if (movement.sqrMagnitude == 0)
            return;
        moveSpeed += dashSpeed;
        playerControl.invul = true;
        currentEnergy = 0;
        //Debug.Log("Dashing");
    }
}
