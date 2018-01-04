using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRandom : MonoBehaviour {
    private float[] pro = { 25, 25, 25, 25 };
    
	// Use this for initialization
	void Start () {
        
        transform.position = new Vector3((Choose(pro)+2) * 10f, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0.5f, 0, 0);
        if (transform.position.x<-1)
            
        transform.position = new Vector3((Choose(pro)+2) * 10f, transform.position.y, transform.position.z);
    }

    int Choose(float[] probs)
    {
        //将事件元素加入到数组中，如上面有4个元素，分别为50,25,20,5
        float total = 0;
        foreach (float elem in probs)
            total += elem;
        //Random.value方法返回一个0—1的随机数
        float randomPoint = Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
                return i;
            else
                randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }
}
