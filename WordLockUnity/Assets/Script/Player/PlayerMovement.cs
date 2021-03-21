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

    public float dashSpeed, startDashTime, restoreTimeDash;
    private float dashTime, currentEnergy, dashEnergy = 20;

    public Transform boss;

    public enum DirPlayer
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
        PlayerUI.playerUI.SetMaxEnergy(dashEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.levelManager.sceneState != SceneState.Talking && LevelManager.levelManager.sceneState != SceneState.SpellWrite)
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
        }
        else if(LevelManager.levelManager.sceneState == SceneState.SpellWrite)
            anim.SetBool("Walking", false);

    }

    private void FixedUpdate()
    {
        if(LevelManager.levelManager.sceneState != SceneState.Talking && LevelManager.levelManager.sceneState != SceneState.SpellWrite)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
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
        StartCoroutine(LerpDashEnergy());
        //Debug.Log("Dashing");
    }

    public IEnumerator LerpPosition(Vector3 targetPosition, float duration, int direction)
    {
        LevelManager.levelManager.SetState(SceneState.Talking);
        anim.SetFloat("Direction", direction);
        anim.SetBool("Walking", true);

        float time = 0;
        Vector2 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        LevelManager.levelManager.StartTalking(0);
        anim.SetBool("Walking", false);
        transform.position = targetPosition;
    }

    IEnumerator LerpDashEnergy()
    {
        float time = 0, startValue = 0;
        while (time < restoreTimeDash)
        {
            if (dashTime <= 0)
            {
                dashTime = startDashTime;
                moveSpeed = 5;
                Invoke("InvulOn", 0.5f);
            }
            else
                dashTime -= Time.deltaTime;
            currentEnergy = Mathf.Lerp(startValue, dashEnergy, time / restoreTimeDash);
            time += Time.deltaTime;
            yield return null;
            PlayerUI.playerUI.UpdateEnergy(currentEnergy);
        }
        currentEnergy = dashEnergy;
        PlayerUI.playerUI.UpdateEnergy(currentEnergy);
    }

    void InvulOn()
    {
        playerControl.invul = false;
    }
}
