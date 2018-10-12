using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtoPlace : MonoBehaviour {
    public GameObject[] arr = new GameObject[3]; //3 objeyi tuttugum yer
    int usingIndex; //üstünde durulan obje
    public float speed; // lerp hızı
    Rigidbody myRgb;
	// Use this for initialization
	void Start () {
        arr[0].transform.position = transform.position + new Vector3(0, -1f, 0);// ilk objeyi tam altımıza atıyoruz
        usingIndex = 0; //  ilk objeyi kullandıgımız olarak oto atıyoruz
        myRgb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float axisH = Input.GetAxis("Horizontal"); //hareket için 
        float axisV = Input.GetAxis("Vertical");
        myRgb.velocity = new Vector3(axisH * 2, 0, axisV * 2); //hareket fonksiyonu
        CheckPosition();
        TurnAround();
        ChangeCube();
	}
    void CheckPosition() // playerin  üstünde durdugu küpe göre olan konumuna bakıyoruz
    {
        Vector3 targetLoc;
        Vector3 distance;
        distance = arr[usingIndex].transform.position - transform.position; //altımızdaki küpe uzaklıgımız
        Debug.Log(distance);
        if (distance.magnitude > 1.2f) // eger küpün merkezine bu kadar uzaksak  ona göre gittiğimiz tarafa sıradaki küpü yerleştiriyoruz
        {
            if(Mathf.Abs(distance.x) > Mathf.Abs (distance.z)) // hangi eksende ilerlediğimize bakıyoruz 
            {
                if(distance.x > 0) //+ yöndemi  - yöndemi ilerlediğimize
                {
                    targetLoc = arr[usingIndex].transform.position + new Vector3(-2, 0, 0);
                   
                }
                else
                {
                    targetLoc = arr[usingIndex].transform.position + new Vector3(2, 0, 0);
                    
                }
            }
            else
            {
                if (distance.z > 0)//+ yöndemi  - yöndemi ilerlediğimize
                {
                    targetLoc = arr[usingIndex].transform.position + new Vector3(0, 0,-2);
                }
                else
                {
                    targetLoc = arr[usingIndex].transform.position + new Vector3(0, 0, 2);
                }

            }
            arr[(usingIndex + 1) % 3].transform.rotation = Quaternion.Euler(0, 0, 0);
            arr[(usingIndex + 1) % 3].transform.position = Vector3.Lerp(arr[(usingIndex + 1) % 3].transform.position, targetLoc, speed* Time.deltaTime);
        }
         
    }
    void TurnAround()
    {
        for(int i = 0; i < 3; i++)
        {
            if (i != usingIndex  )
            {   
                
                arr[i].transform.RotateAround(transform.position, new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0), 2);
            }
        }
    }
    void ChangeCube()
    {   float distance;
        Vector3 targetLoc;
        distance = (transform.position - arr[(usingIndex + 1) % 3].transform.position).magnitude;
      
        if(distance < 1.15f)
        {   
            arr[usingIndex].transform.position = transform.position + new Vector3(5, -3, 0);
            usingIndex = (usingIndex + 1) % 3;
        }
    }
    
}
