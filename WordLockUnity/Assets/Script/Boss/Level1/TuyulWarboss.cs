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

    public float healthBoss, delayArrowSpawn, attackPerRound, durationIdle;
    public float delayAfterArrow, delayAfterKnife, delayAfterTuyul, delayAdjustHealth;
    public Animator animBoss;

    [HideInInspector]
    public bool regenerate;

    public int manaPlayerGet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomAttack(attackPerRound));
        BossUI.bossUI.SetMaxHealth(healthBoss);
        BossUI.bossUI.SetMaxMana(durationIdle);
    }

    // Update is called once per frame
    void Update()
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
                    StartCoroutine(ArrowAttack());
                    yield return new WaitForSeconds(delayAfterArrow - delayAdjustHealth - (attackCount == 1 ? 1 : 0));
                    break;
                case 1:
                    KnifeAttack();
                    yield return new WaitForSeconds(delayAfterKnife - delayAdjustHealth - (attackCount == 1 ? 1 : 0));
                    break;
                case 2:
                    TuyulAttack();
                    yield return new WaitForSeconds(delayAfterTuyul - delayAdjustHealth - (attackCount == 1 ? 1 : 0));
                    break;
            }
            player.GetComponent<PlayerControl>().UpdateMana(-manaPlayerGet);
            attackCount--;
            if (attackCount == 0)
            {
                regenerate = true;
                LevelManager.levelManager.SetState(GameState.SceneState.SpellWrite);
                float time = 0, endValue = 0, regenerateTime = 0;
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

    public void TakeDamage(float damage)
    {
        healthBoss -= damage;
        BossUI.bossUI.UpdateHealth(healthBoss);
        Debug.Log("BossKenaDamage");
        regenerate = false;
    }

}
