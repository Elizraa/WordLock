using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyulWarboss : MonoBehaviour
{
    public Transform player;
    public int AttackVariety;
    
    public GameObject arrow, knife;

    public Transform[] ArrowSpawn;

    public float healthBoss, delayAttack, delayArrowSpawn, attackPerRound, manaRegenerationTime;
    public Animator animBoss;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(RandomAttack(manaRegenerationTime, delayAttack, attackPerRound));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RandomAttack(float regenerateTime,float atttackElapsed, float attackCount)
    {
        while (true)
        {
            while (LevelManager.levelManager.sceneState != GameState.SceneState.Fighting)
            {
                animBoss.SetBool("Idle", true);
                yield return new WaitForEndOfFrame();
            }
            animBoss.SetBool("Idle", false);
            if(attackCount <= 0)
            {
                regenerateTime = 0;
                //Buka bar regenerate
                while(regenerateTime < manaRegenerationTime)
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
                    StartCoroutine(ArrowAttack());
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            attackCount--;
            yield return new WaitForSeconds(delayAttack);
        }
    }

    IEnumerator ArrowAttack()
    {
        foreach(Transform spawnPlace in ArrowSpawn)
        {
            Weapon arrowPrefab = Instantiate(arrow, spawnPlace.position, Quaternion.LookRotation(player.position-spawnPlace.position)).GetComponent<Weapon>();
            arrowPrefab.target = player;
            yield return new WaitForSeconds(delayArrowSpawn);
        }
    }

}
