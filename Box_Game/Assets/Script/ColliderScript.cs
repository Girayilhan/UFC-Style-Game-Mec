using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour {
    public static ColliderScript colliderInstance;
    public GameObject[] playerColliders = new GameObject[6];
    public GameObject[] enemyColliders = new GameObject[6];

    public GameObject[] playerParts = new GameObject[6];
    public GameObject[] enemyParts = new GameObject[6];
    // Use this for initialization
    void Start () {
        if(colliderInstance == null)
        {
            colliderInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //-0 for head -1 for body  -2 for lefthand -3 for righthand -4 for leftfoot -5 for rightfoot
        playerParts[0] = GameObject.FindGameObjectWithTag("PlayerHead");
        playerParts[1] = GameObject.FindGameObjectWithTag("PlayerBody");
        playerParts[2] = GameObject.FindGameObjectWithTag("PlayerLeftHand");
        playerParts[3] = GameObject.FindGameObjectWithTag("PlayerRightHand");
        playerParts[4] = GameObject.FindGameObjectWithTag("PlayerLeftFoot");
        playerParts[5] = GameObject.FindGameObjectWithTag("PlayerRightFoot");

        enemyParts[0] = GameObject.FindGameObjectWithTag("EnemyHead");
        enemyParts[1] = GameObject.FindGameObjectWithTag("EnemyBody");
        enemyParts[2] = GameObject.FindGameObjectWithTag("EnemyLeftHand");
        enemyParts[3] = GameObject.FindGameObjectWithTag("EnemyRightHand");
        enemyParts[4] = GameObject.FindGameObjectWithTag("EnemyLeftFoot");
        enemyParts[5] = GameObject.FindGameObjectWithTag("EnemyRightFoot");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        SetPositions();

    }
    void SetPositions()
    {  
        for(int i = 0; i < 6; i++)
        {
            playerColliders[i].transform.position =Camera.main.WorldToScreenPoint (playerParts[i].transform.position);
            playerColliders[i].transform.position = new Vector3(playerColliders[i].transform.position.x, playerColliders[i].transform.position.y, 0);
        }

        for (int i = 0; i < 6; i++)
        {
            enemyColliders[i].transform.position = Camera.main.WorldToScreenPoint(enemyParts[i].transform.position);
            enemyColliders[i].transform.position = new Vector3(enemyColliders[i].transform.position.x, enemyColliders[i].transform.position.y, 0);
        }

    }
    public void CheckCollision(string x,string y)
    {
      
        if(x== "PlayerLeftHand")
        {
            if(y == "EnemyHead")
            {
                BotScript.botInstance.TakeDamage("LeftHeadHit");
            }
            if(y== "EnemyBody")
            {
                BotScript.botInstance.TakeDamage("LeftBodyHit");
            }
        }
        if (x == "PlayerRightHand")
        {
            if (y == "EnemyHead")
            {
                BotScript.botInstance.TakeDamage("RightHeadHit");
            }
            if (y == "EnemyBody")
            {
                BotScript.botInstance.TakeDamage("RightBodyHit");
            }
        }
        if (x == "PlayerLeftFoot")
        {
            if (y == "EnemyHead")
            {
                BotScript.botInstance.TakeDamage("LeftHeadHit");
            }
            if (y == "EnemyBody")
            {
                BotScript.botInstance.TakeDamage("LeftBodyHit");
            }
        }
        if (x == "PlayerRightFoot")
        {
            if (y == "EnemyHead")
            {
                BotScript.botInstance.TakeDamage("RightHeadHit");
            }
            if (y == "EnemyBody")
            {
                BotScript.botInstance.TakeDamage("RightBodyHit");
            }
        }

        //********************************************************************
        if (x == "EnemyLeftHand")
        {
            if (y == "PlayerHead")
            {
                PlayerController.instance.TakeDamage("LeftHeadHit");
            }
            if (y == "PlayerBody")
            {
                PlayerController.instance.TakeDamage("LeftBodyHit");
            }
        }
        if (x == "EnemyRightHand")
        {
            if (y == "PlayerHead")
            {
                PlayerController.instance.TakeDamage("RightHeadHit");
            }
            if (y == "PlayerBody")
            {
                PlayerController.instance.TakeDamage("RightBodyHit");
            }
        }
        if (x == "EnemyLeftFoot")
        {
            if (y == "PlayerHead")
            {
                PlayerController.instance.TakeDamage("LeftHeadHit");
            }
            if (y == "PlayerBody")
            {
                PlayerController.instance.TakeDamage("LeftBodyHit");
            }
        }
        if (x == "EnemyRightFoot")
        {
            if (y == "PlayerHead")
            {
                PlayerController.instance.TakeDamage("RightHeadHit");
            }
            if (y == "PlayerBody")
            {
                PlayerController.instance.TakeDamage("RightBodyHit");
            }
        }

    }

}
