using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    Animator myAnimator;
    float axisH;
    float axisV;
    Rigidbody myRgb;
    public float moveSpeed;
    public GameObject enemy;
    public float maxEnergy;
    public float insEnergy;
    float timer;
    public Slider energySlider;
    public GameObject enemy1, enemy2;
    float damageTimer;
    public Image[] boysImage = new Image[7];
    public float[] bodysHealths = new float[7];
    public bool isPlayerAttacking;
    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        myAnimator = GetComponent<Animator>();
        myRgb = GetComponent<Rigidbody>();
        for (int i = 0; i < 7; i++)
        {
            bodysHealths[i] = 255f;
        }
    }
    float seconds;
    float milliSeconds;
	// Update is called once per frame
	void FixedUpdate () {
        seconds += Time.deltaTime;
        Debug.Log(seconds.ToString("n2"));
        
        
        damageTimer += Time.deltaTime;
        timer += Time.deltaTime;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        EnergyReg();
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");
        
        myAnimator.SetFloat("axisH", axisH);
        myAnimator.SetFloat("axisV", axisV);
        ControlAnim();
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            myRgb.velocity = transform.forward * axisH * moveSpeed;
            myRgb.velocity += transform.right * -axisV * moveSpeed;
          
        }
        else
        {
            myRgb.velocity = Vector3.zero;
           
        }

        transform.LookAt(new Vector3( enemy.transform.position.x,1,enemy.transform.position.z));
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
            if (Input.GetKeyDown(KeyCode.Joystick1Button6))
            {
                myAnimator.SetTrigger("Dodge");

            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("LeftPunch");
                insEnergy -= 10;
                isPlayerAttacking = true;

            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("RightPunch");
                insEnergy -= 10;
                isPlayerAttacking = true;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("LeftKick");
                insEnergy -= 10;
                isPlayerAttacking = true;
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                myAnimator.SetFloat("shotH", axisH);
                myAnimator.SetFloat("shotV", axisV);
                myAnimator.SetTrigger("RightKick");
                insEnergy -= 10;
                isPlayerAttacking = true;
            }
            else if (Input.GetKey(KeyCode.Joystick1Button5))
            {
                myAnimator.SetTrigger("Defence");
                
            }
            else
            {
              

            }
            energySlider.value = insEnergy;
            
        }
      


    }
    void EnergyReg()
    { 
        if(timer > 0.5f && insEnergy < maxEnergy)
        {
            insEnergy += 2;
            timer = 0;
        }
        energySlider.value = insEnergy;
    }
    public void TakeDamage(string x)
    {
       
        if (damageTimer > 1 && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("RightBlock") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeftBlock") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Dodge"))
        {
            Debug.Log(x);
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
    public void ChangeEnemy()
    {
        if (enemy1.active)
        {
            enemy1.active = false;
            enemy2.active = true;
        }
        else
        {
            enemy1.active = true;
            enemy2.active = false;
        }
    }


}
