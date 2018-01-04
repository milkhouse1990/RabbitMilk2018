using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGroup
{
    private float[] probs;
    public RandomGroup(float[] p_probs)
    {
        int length = p_probs.Length;
        probs = new float[length];
        for (int i=0;i<length;i++)
        {
            probs[i] = p_probs[i];
        }
    }
    public int RandomChoose()
    {
        //将事件元素加入到数组中，如上面有4个元素，分别为50,25,20,5
        float total = 0;
        foreach (float elem in probs)
            total += elem;
        //Random.value方法返回一个0—1的随机数
        float randomPoint = Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint <= probs[i])
                return i;
            else
                randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }
}

