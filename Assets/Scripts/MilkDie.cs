using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MilkDie : MonoBehaviour {
    public Transform die;
    private int timer=0;
    private Transform die00,die01, die02,die03,die04,die05,die06,die07;
    private Transform die10, die11, die12, die13, die14, die15, die16, die17;
    //private Transform[] die0,die1;
    // Use this for initialization
    void Start () {
        //for (int i = 0; i < 8; i++)
        //    die0[i]= Instantiate(die, transform.position, Quaternion.identity);
        
            die00 = Instantiate(die, transform.position+ new Vector3(1.5f*Mathf.Sin(0 * Mathf.PI / 4),1.5f*Mathf.Cos(0 * Mathf.PI / 4), 0), Quaternion.identity);
            die01 = Instantiate(die, transform.position + new Vector3(1.5f*Mathf.Sin(1 * Mathf.PI / 4), 1.5f*Mathf.Cos(1 * Mathf.PI / 4), 0), Quaternion.identity);
            die02= Instantiate(die, transform.position + new Vector3(1.5f*Mathf.Sin(2 * Mathf.PI / 4), 1.5f*Mathf.Cos(2 * Mathf.PI / 4), 0), Quaternion.identity);
            die03 = Instantiate(die, transform.position + new Vector3(1.5f*Mathf.Sin(3 * Mathf.PI / 4), 1.5f*Mathf.Cos(3 * Mathf.PI / 4), 0), Quaternion.identity);
            die04 = Instantiate(die, transform.position + new Vector3(1.5f*Mathf.Sin(4 * Mathf.PI / 4), 1.5f*Mathf.Cos(4 * Mathf.PI / 4), 0), Quaternion.identity);
            die05 = Instantiate(die, transform.position + new Vector3(1.5f*Mathf.Sin(5 * Mathf.PI / 4), 1.5f*Mathf.Cos(5 * Mathf.PI / 4), 0), Quaternion.identity);
            die06 = Instantiate(die, transform.position + new Vector3(1.5f*Mathf.Sin(6 * Mathf.PI / 4), 1.5f*Mathf.Cos(6 * Mathf.PI / 4), 0), Quaternion.identity);
            die07 = Instantiate(die, transform.position + new Vector3(1.5f*Mathf.Sin(7 * Mathf.PI / 4), 1.5f*Mathf.Cos(7 * Mathf.PI / 4), 0), Quaternion.identity);

            die10 = Instantiate(die, transform.position, Quaternion.identity);
            die11 = Instantiate(die, transform.position, Quaternion.identity);
            die12 = Instantiate(die, transform.position, Quaternion.identity);
            die13 = Instantiate(die, transform.position, Quaternion.identity);
            die14 = Instantiate(die, transform.position, Quaternion.identity);
            die15 = Instantiate(die, transform.position, Quaternion.identity);
            die16 = Instantiate(die, transform.position, Quaternion.identity);
            die17 = Instantiate(die, transform.position, Quaternion.identity);

        

    }
	
	// Update is called once per frame
	void Update () {
        

            
        //for (int i = 0; i < 8; i++)
        die00.position += new Vector3(16.0f / 64 * Mathf.Sin(0 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(0 * Mathf.PI / 4), 0);
        die01.position += new Vector3(16.0f / 64 * Mathf.Sin(1 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(1 * Mathf.PI / 4), 0);
        die02.position += new Vector3(16.0f / 64 * Mathf.Sin(2 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(2 * Mathf.PI / 4), 0);
        die03.position += new Vector3(16.0f / 64 * Mathf.Sin(3 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(3 * Mathf.PI / 4), 0);
        die04.position += new Vector3(16.0f / 64 * Mathf.Sin(4 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(4 * Mathf.PI / 4), 0);
        die05.position += new Vector3(16.0f / 64 * Mathf.Sin(5 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(5 * Mathf.PI / 4), 0);
        die06.position += new Vector3(16.0f / 64 * Mathf.Sin(6 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(6 * Mathf.PI / 4), 0);
        die07.position += new Vector3(16.0f / 64 * Mathf.Sin(7 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(7 * Mathf.PI / 4), 0);

        
            die10.position += new Vector3(16.0f / 64 * Mathf.Sin(0 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(0 * Mathf.PI / 4), 0);
            die11.position += new Vector3(16.0f / 64 * Mathf.Sin(1 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(1 * Mathf.PI / 4), 0);
            die12.position += new Vector3(16.0f / 64 * Mathf.Sin(2 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(2 * Mathf.PI / 4), 0);
            die13.position += new Vector3(16.0f / 64 * Mathf.Sin(3 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(3 * Mathf.PI / 4), 0);
            die14.position += new Vector3(16.0f / 64 * Mathf.Sin(4 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(4 * Mathf.PI / 4), 0);
            die15.position += new Vector3(16.0f / 64 * Mathf.Sin(5 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(5 * Mathf.PI / 4), 0);
            die16.position += new Vector3(16.0f / 64 * Mathf.Sin(6 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(6 * Mathf.PI / 4), 0);
            die17.position += new Vector3(16.0f / 64 * Mathf.Sin(7 * Mathf.PI / 4), 16.0f / 64 * Mathf.Cos(7 * Mathf.PI / 4), 0);
        
        if (timer > 120)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        timer++;
        //Debug.Log(timer);


    }
    void FixedUpdate()
    {
        
    }
}
