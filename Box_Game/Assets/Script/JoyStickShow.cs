using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickShow : MonoBehaviour {
    public Image joyStick;
    public Image button0, button1, button2, button3,button4,button5,button6,button7;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            button0.color = Color.red;
        }
        else
        {
            button0.color = Color.white;
        }
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            button1.color = Color.red;
        }
        else
        {
            button1.color = Color.white;
        }
        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            button2.color = Color.red;
        }
        else
        {
            button2.color = Color.white;
        }
        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            button3.color = Color.red;
        }
        else
        {
            button3.color = Color.white;
        }
      
        if (Input.GetKey(KeyCode.Joystick1Button4))
        {
            button4.color = Color.red;
        }
        else
        {
            button4.color = Color.white;
        }
        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            button5.color = Color.red;
        }
        else
        {
            button5.color = Color.white;
        }
        if (Input.GetKey(KeyCode.Joystick1Button6))
        {
            button6.color = Color.red;
        }
        else
        {
            button6.color = Color.white;
        }
        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            button7.color = Color.red;
        }
        else
        {
            button7.color = Color.white;
        }
        joyStick.GetComponent<RectTransform>().transform.localPosition = new Vector2(axisH * 50, axisV * 50);
        if (joyStick.GetComponent<RectTransform>().transform.localPosition != Vector3.zero)
        {
            joyStick.color = Color.red;
        }
        else
        {
            joyStick.color = Color.white;
        }
    }
}
