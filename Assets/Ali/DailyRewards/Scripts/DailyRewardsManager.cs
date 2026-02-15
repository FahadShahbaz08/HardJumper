using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NiobiumStudios;
using System;

public class DailyRewardsManager : MonoBehaviour
{
    private void OnEnable()
    {
        DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }

    private void OnDisable()
    {
        DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    private void OnClaimPrizeDailyRewards(int day)
    {
        Reward reward = DailyRewards.instance.GetReward(day);

        print(reward.unit);
        print(reward.reward);


        //if(UIManager.Instance !=null)
        //{
        //    if (reward.unit == "Diamonds")
        //    {
        //        UIManager.Instance.AddDiamond(reward.reward);
        //    }
        //    else if (reward.unit == "Dollars")
        //    {
        //        UIManager.Instance.AddDollar(reward.reward);

        //    }
        //}
        //else
        //{
        //    print("Unable to find UIManager");
        //}
        
    }
}
