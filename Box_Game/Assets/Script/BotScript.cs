using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BotScript : MonoBehaviour {
    public static BotScript botInstance;
    Animator myAnimator;
    float axisH;
    float axisV;
    Rigidbody myRgb;
    public float moveSpeed;
    public GameObject enemy;
    public float maxEnergy;
    public float insEnergy;
    float timer;
    float damageTimer;
    public Image[] boysImage = new Image[7];
    public float[] bodysHealths = new float[7];
    bool isDefensing;
    float aiTimer;
    //AiMod
    float distance;
    public float smoothMove;
    public bool isEnemyAttacing;

    // Use this for initialization
    void Start()
    {
        if (botInstance == null)
        {
            botInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        myAnimator = GetComponent<Animator>();
        myRgb = GetComponent<Rigidbody>();
        for(int i = 0; i < 7; i++)
        {
            bodysHealths[i] = 255f;
        }
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        isEnemyAttacing = false;
        aiTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;
        timer += Time.deltaTime;
        enemy = GameObject.FindGameObjectWithTag("Player");
        EnergyReg();
        transform.LookAt(new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z));
      
        AiMod();
    }
    void ControlAnim()
    {
        myAnimator.ResetTrigger("LeftPunch");
        myAnimator.ResetTrigger("RightPunch");
        myAnimator.ResetTrigger("LeftKick");
        myAnimator.ResetTrigger("RightKick");
      

        if ((myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move")) && insEnergy > 0)
        {
            if (Input.GetKey(KeyCode.Joystick1Button4))
            {
                myAnimator.SetBool("SpecialShot", true);
            }
            else
            {
                myAnimator.SetBool("SpecialShot", false);
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("LeftPunch");
                insEnergy -= 10;

            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("RightPunch");
                insEnergy -= 10;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("LeftKick");
                insEnergy -= 10;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("RightKick");
                insEnergy -= 10;
            }
            else if (Input.GetKey(KeyCode.Joystick1Button5))
            {
                moveSpeed = 2;
            }
            else
            {
                moveSpeed = 1;
            }
        

        }



    }
    void EnergyReg()
    {
        if (timer > 0.5f && insEnergy < maxEnergy)
        {
            insEnergy += 2;
            timer = 0;
        }
        
    }
    public void TakeDamage(string x)
    {    if(damageTimer > 1 && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightBlock") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftBlock") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dodge") && PlayerController.instance.isPlayerAttacking)
        {
            if (x == "RightHeadHit")
            {
                bodysHealths[0] -= 10;
                boysImage[0].color = new Color((255 - bodysHealths[0]) / 255, 0, 0, 1);
                myAnimator.SetTrigger("RightHeadHit");
            }
            if (x == "LeftHeadHit")
            {
                bodysHealths[0] -= 10;
                boysImage[0].color = new Color((255 - bodysHealths[0]) / 255, 0, 0, 1);
                myAnimator.SetTrigger("LeftHeadHit");
            }
            if (x == "RightBodyHit")
            {
                bodysHealths[1] -= 10;
                boysImage[1].color = new Color((255 - bodysHealths[1]) / 255, 0, 0, 1);
                myAnimator.SetTrigger("RightBodyHit");
            }
            if (x == "LeftBodyHit")
            {
                bodysHealths[1] -= 10;
                boysImage[1].color = new Color((255 - bodysHealths[1]) / 255, 0, 0, 1);
                myAnimator.SetTrigger("LeftBodyHit");
            }

          
            damageTimer = 0;
        }
       
        
    }
    void AiMod()
    {   CheckDistance();
        
        int chanceFac;
        if ((myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move")) && insEnergy > 0)
        {

            if (distance <1.2f && aiTimer > 0.5f)
            {
                aiTimer = 0;
                if (PlayerController.instance.isPlayerAttacking)
                {
                    chanceFac = Random.Range(0, 100);
                    if (Random.Range(0, 1000) < chanceFac * (insEnergy / 50))
                    {
                        PlayerController.instance.isPlayerAttacking = false;
                        Dodge();

                        chanceFac = Random.Range(0, 250);
                        if (Random.Range(0, 1000) < chanceFac * (insEnergy / 50))
                        {
                            Attack();
                        }
                    }
                    else if (Random.Range(0, 1000) < chanceFac * 5)
                    {
                        PlayerController.instance.isPlayerAttacking = false;
                        Defense();
                        isDefensing = false;
                      
                    }
                    
                }
                else
                {
                    if (insEnergy > 30)
                    {
                        Attack();
                    }
                    else
                    {
                        Defense();
                        isDefensing = false;
                     
                    }

                }

            }
            else
            {
                if (insEnergy > 30)
                {
                    if ((myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move")) && insEnergy > 0)
                        GetCloserPlayer();
                }
                else
                {
                    Defense();
                }

            }
        }
    }
    void CheckDistance()
    { 
        distance = (transform.position - enemy.transform.position).magnitude;
        
    }
    void GetCloserPlayer()
    {
        
        if(distance > 1.2f)
        {
            myAnimator.SetFloat("axisH",Mathf.Abs(Camera.main.transform.right.x) * moveSpeed);
            
            myRgb.velocity = -Camera.main.transform.right* moveSpeed;  
            Debug.Log("GetCloser");

        }
        else
        {
            myAnimator.SetFloat("axisH", 0);
            myAnimator.SetFloat("axisV", 0);
            myRgb.velocity = Vector3.zero;
        }
           
       
    }
    void Defense()
    {
        Debug.Log("Defens");
        isDefensing = true;
        myAnimator.SetTrigger("Defence");
    }
    void Dodge()
    {
        Debug.Log("Dodge");
        myAnimator.SetTrigger("Dodge");
      

    }
    void Attack()
    {
        isEnemyAttacing = true;
        Debug.Log("Attack");
        float x = Random.Range(0, 2f);
        float y = Random.Range(0, 2f);
        int randomButton = Random.Range(0, 4);
        if (randomButton==0)
        {
            myAnimator.SetFloat("shotH", x);
            myAnimator.SetFloat("shotV", y);
            myAnimator.SetTrigger("LeftPunch");
            insEnergy -= 10;
             

        }
        else if (randomButton==3)
        {
            myAnimator.SetFloat("shotH", x);
            myAnimator.SetFloat("shotV", y);
            myAnimator.SetTrigger("RightPunch");
            insEnergy -= 10;
             
        }
        else if (randomButton == 1)
        {
            myAnimator.SetFloat("shotH", x);
            myAnimator.SetFloat("shotV", y);
            myAnimator.SetTrigger("LeftKick");
            insEnergy -= 10;
            
        }
        else if (randomButton == 2)
        {
            myAnimator.SetFloat("shotH", x);
            myAnimator.SetFloat("shotV", y);
            myAnimator.SetTrigger("RightKick");
            insEnergy -= 10;
            
        }

    }

}
