using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HPScript : MonoBehaviour
{
    public float hp = 100f;
    public bool isPlayer;

    private NavMeshAgent navAgent;
    private EnemyControler enemyController;
    private bool isDead; 
    void Awake()
    {
 
    }

    void Update()
    {
        
    }

    public void ApplyDmg(float damage)
    {
        if (isDead)
            return;

        hp -= damage;

        if (hp <= 0f)
        {
            PlayerDead();

            isDead = true;
        }
    }

    void PlayerDead()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyControler>().enabled = false;
        }

        GetComponent<PlayerController>().enabled = false;

        if (tag == "Player")
        {
            Invoke("RestartGame", 3f);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Gra");
    }

}
