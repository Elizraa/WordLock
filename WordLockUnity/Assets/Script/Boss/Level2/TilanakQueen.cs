using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilanakQueen : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public int AttackVariety;
    float zAngle = 0;

    public GameObject aura, dfb;

    public Transform auraSpawner;
    private Transform[] auraSpawn;

    public float healthBoss, DFBDuration, attackPerRound, durationIdle;
    public float delayAfterAura, delayAfterDFB, delayAfterGaze, delayAdjustHealth;
    public Animator animBoss;

    bool seePlayer;

    [HideInInspector]
    public bool regenerate;

    public int manaPlayerGet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomAttack(attackPerRound));
        auraSpawn = GetComponentsInChildren<Transform>();
        BossUI.bossUI.SetMaxHealth(healthBoss);
        BossUI.bossUI.SetMaxMana(durationIdle);
    }

    // Update is called once per frame
    void Update()
    {
        auraSpawner.Rotate(0, 0, zAngle);
        seePlayer = CanSeePlayer();
    }

    private void OnDrawGizmos()
    {
        
    }

    IEnumerator RandomAttack(float attackCount)
    {
        while (true)
        {
            while (LevelManager.levelManager.sceneState != GameState.SceneState.Fighting)
            {
                //animBoss.SetBool("Idle", true);
                //Debug.Log("aa");
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1);
            //animBoss.SetBool("Idle", false);

            switch (Random.Range(0, AttackVariety))
            {
                case 0:
                    AuraAttack();
                    yield return new WaitForSeconds(delayAfterAura - delayAdjustHealth - (attackCount == 1 ? 1 : 0));
                    break;
                case 1:
                    StartCoroutine(DeathFromBelow(3));
                    yield return new WaitForSeconds(delayAfterDFB - delayAdjustHealth - (attackCount == 1 ? 1 : 0));
                    break;
                case 2:
                    StartCoroutine(GazeAttack());
                    yield return new WaitForSeconds(delayAfterGaze - delayAdjustHealth - (attackCount == 1 ? 1 : 0));
                    break;
            }
            attackCount--;
            player.GetComponent<PlayerControl>().UpdateMana(-manaPlayerGet);
            if (attackCount == 0)
            {
                regenerate = true;
                LevelManager.levelManager.SetState(GameState.SceneState.SpellWrite);
                float time = 0, endValue = 0, regenerateTime;
                while (time < durationIdle && regenerate)
                {
                    regenerateTime = Mathf.Lerp(durationIdle, endValue, time / durationIdle);
                    time += Time.deltaTime;
                    yield return null;
                    BossUI.bossUI.UpdateMana(regenerateTime);
                }
                LevelManager.levelManager.SetState(GameState.SceneState.Fighting);
                regenerateTime = endValue;
                BossUI.bossUI.UpdateMana(regenerateTime);
                regenerate = false;
                attackCount = attackPerRound;
            }
        }
    }

    void AuraAttack()
    {
        foreach (Transform spawnPlace in auraSpawn)
        {
            if (spawnPlace.CompareTag("SpawnPoint"))
            {
                Aura aurabullet = Instantiate(aura, spawnPlace.position, spawnPlace.rotation).GetComponent<Aura>();
            }
        }
    }

    IEnumerator DeathFromBelow(int atkTime)
    {
        Vector3 deathCenter;
        for(int i = 0; i < atkTime; i++)
        {
            deathCenter = player.position;
            DeathFromBelow DFB = Instantiate(dfb, deathCenter, player.rotation).GetComponent<DeathFromBelow>();
            yield return new WaitForSeconds(DFBDuration);
        }
    }

    IEnumerator GazeAttack()
    {
        
        float timeGazed = 0;
        Debug.Log("Dazing");

        for (float f = 5f; f >= -0.05f; f -= 0.05f)
        {
            if (seePlayer)
            {
                timeGazed += 0.05f;
            }

            if(timeGazed >= 3f)
            {
                player.gameObject.GetComponent<PlayerControl>().health = 0; ;
            }

            Debug.Log("Daze for : " + timeGazed);
            yield return new WaitForSeconds(0.05f);
        }
        
    }

    bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, 1 << LayerMask.NameToLayer("CanDetect"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
            else return false;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //animBoss.SetBool("Teriak", true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //animBoss.SetBool("Teriak", false);
        }
    }

    public void TakeDamage(float damage)
    {
        healthBoss -= damage;
        BossUI.bossUI.UpdateHealth(healthBoss);
        Debug.Log("BossKenaDamage");
    }

    public void DoneTyping()
    {
        regenerate = false;
    }

}
