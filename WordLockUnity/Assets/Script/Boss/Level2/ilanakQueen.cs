using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ilanakQueen : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public int AttackVariety;
    float zAngle = 0;

    public GameObject aura, dfb, tuyulMini;

    public Transform auraSpawner;
    public Transform[] auraSpawn, knifeSpawn;

    public float healthBoss, DFBDuration, attackPerRound, manaRegenerationTime;
    public float delayAfterAura, delayAfterDFB, delayAfterGaze, delayAdjustHealth;
    public Animator animBoss;

    bool seePlayer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomAttack(manaRegenerationTime, attackPerRound));
        auraSpawn = GetComponentsInChildren<Transform>();
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

    IEnumerator RandomAttack(float regenerateTime, float attackCount)
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
            if (attackCount <= 0)
            {
                regenerateTime = 0;
                //Buka bar regenerate
                while (regenerateTime < manaRegenerationTime)
                {
                    regenerateTime += Time.fixedDeltaTime;
                    if (regenerateTime > manaRegenerationTime)
                    {
                        regenerateTime = manaRegenerationTime;
                        //Tutup bar regenerate
                    }
                    yield return new WaitForEndOfFrame();
                }
                attackCount = attackPerRound;
            }
            switch (Random.Range(0, AttackVariety))
            {
                case 0:
                    AuraAttack();
                    yield return new WaitForSeconds(delayAfterAura - delayAdjustHealth);
                    break;
                case 1:
                    StartCoroutine(DeathFromBelow(3));
                    yield return new WaitForSeconds(delayAfterDFB - delayAdjustHealth);
                    break;
                case 2:
                    StartCoroutine(GazeAttack());
                    yield return new WaitForSeconds(delayAfterGaze - delayAdjustHealth);
                    break;
            }
            attackCount--;
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
}
