using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {
    GameObject player, enemy;
    Vector3 lookPoint;
    Vector3 standPoint;
    public float camHeight;
    public float zoomAmount;
    float distance;
    public float smooth;
    public float lookHeight;
	// Use this for initialization
	void Start () {
        
       
	}
    private void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void LateUpdate () {
        CalculatePoint();
	}
    void CalculatePoint()
    {
        distance = (player.transform.position - enemy.transform.position).magnitude;
        if(distance > 4f)
        {
           standPoint = ((player.transform.position + enemy.transform.position) / 2) + player.transform.right.normalized *distance;
        }
        else if(distance > 0)
        {
            standPoint = ((player.transform.position + enemy.transform.position) / 2) + player.transform.right.normalized * zoomAmount;
        }
      transform.position= Vector3.Lerp(transform.position, new Vector3(standPoint.x, camHeight, standPoint.z), smooth * Time.deltaTime);
        Vector3 lookPoint = ((player.transform.position + enemy.transform.position) / 2);
        lookPoint = new Vector3(lookPoint.x, lookHeight, lookPoint.z);

        transform.LookAt( lookPoint);
    }
}
