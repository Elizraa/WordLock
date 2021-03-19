using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuyulWarboss : MonoBehaviour
{
    public Transform player;
    public int AttackVariety;
    
    public GameObject arrow, knife, tuyulMini;

    public Transform knifeDir;
    public Transform[] arrowSpawn, knifeSpawn, tuyulSpawn;

    public float healthBoss, delayArrowSpawn, attackPerRound, manaRegenerationTime;
    public float delayAfterArrow, delayAfterKnife, delayAfterTuyul, delayAdjustHealth;
    public Animator animBoss;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomAttack(manaRegenerationTime, attackPerRound));
    }

    // Update is called once per frame
    void Update()
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
                    yield return new WaitForSeconds(delayAfterArrow - delayAdjustHealth);
                    break;
                case 1:
                    KnifeAttack();
                    yield return new WaitForSeconds(delayAfterKnife - delayAdjustHealth);
                    break;
                case 2:
                    TuyulAttack();
                    yield return new WaitForSeconds(delayAfterTuyul - delayAdjustHealth);
                    break;
            }
            attackCount--;
        }
    }

    IEnumerator ArrowAttack()
    {
        foreach(Transform spawnPlace in arrowSpawn)
        {
            Weapon arrowPrefab = Instantiate(arrow, spawnPlace.position, spawnPlace.rotation).GetComponent<Weapon>();
            //arrowPrefab.target = player;
            yield return new WaitForSeconds(delayArrowSpawn);
            //Debug.Log("SpawnArrow");
        }
    }

    void KnifeAttack()
    {
        FaceTarget(knifeDir);
        foreach (Transform spawnPlace in knifeSpawn)
        {
            Weapon arrowPrefab = Instantiate(knife, spawnPlace.position, spawnPlace.rotation).GetComponent<Weapon>();
            //Debug.Log("SpawnKnife");
        }
    }

    void FaceTarget(Transform source)
    {
        Vector3 targ = player.position;
        targ.z = 0f;

        Vector3 objectPos = source.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        source.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
    }

    void TuyulAttack()
    {
        foreach(Transform spawnPlace in tuyulSpawn)
        {
            GameObject tuyulPrefab = Instantiate(tuyulMini, spawnPlace.position, spawnPlace.rotation);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animBoss.SetBool("SpinHammer", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animBoss.SetBool("SpinHammer", false);
        }
    }

}
