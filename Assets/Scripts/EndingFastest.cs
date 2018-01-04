using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingFastest : MonoBehaviour {
    private bool end_fast = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="weapon" &&
            end_fast)
        {
            GameObject milk = GameObject.Find("milk");
            string[] plot = {"say 2 为什么要攻击人家啦喵",
                "say 1 为什么？这还用问吗？你的语癖已经暴露了你的身份！",
                "say 2 语癖是什么喵？",
                "say 1 语癖就是每一个种族所特有的，会在句尾不自觉地加上的声音！而你的语癖，明显不是我们兔耳族的！",
                "say 2 兔耳族的语癖是什么喵，为什么你没有喵？",
                "say 1 我当然有了，就是",
                "say 2 那是什么啊喵，为什么人家完全听不到的喵。",
                "say 1 那是因为…你的耳朵太小了！",
                "say 2 （受到了强烈的鄙视）",
                "say 1 你那尖尖的耳朵，和你那奇怪的讲话方式，答案只有一个，你就是…",
                "say 2 （什么，要暴露了吗？）",
                "say 1 精灵族！待在森林里，擅长玩弓，喜欢穿树叶的种族！",
                "say 2 人家才不是精灵族呢喵，你真笨啊喵，人家明明是在宇宙中旅行的高贵的猫耳族喵，这次来月球，就是为了掳走你们的公主喵，所以才假装皇家妹抖混进了王宫喵。这么完美的计划，你们愚蠢的兔耳族怎么可能理解喵！",
                "say 1 这个游戏的反派，智商这么低的吗？",
                "say 0 原来你不是皇家妹抖的一员，那么我们要没收你的制服。",
                "say 2 不要！不要拿走人家的制服喵！人家好不容易才能穿上这种适合贫乳的妹抖服，不能拿走喵！只要不拿走，叫人家做什么都行！",
                "say 1 既然这样，那么…"
            };
            milk.GetComponent<Platformer2DUserControl>().EnterAVGMode("PLOT0000");
        }

    }

    public void EndFlagOn()
    {
        end_fast = true;
    }
}
