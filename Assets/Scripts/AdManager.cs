using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{

    public static AdManager Ins;
    public InterstitialAd interstitial;
    public RewardBasedVideoAd rewardBasedVideo;
    // Use this for initialization
    void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
        MobileAds.Initialize("ca-app-pub-3940256099942544~3347511713");
        this.interstitial = new InterstitialAd("ca-app-pub-1589360682424634/9091081337");
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        this.VideoRequest();
        AdRequest();
    }
    private void HandleRewardBasedVideoRewarded(object sender, Reward e)
    {
        //StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().giftBoxClickIEnumerator());
    }
    void VideoRequest()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, "ca-app-pub-3940256099942544/5224354917");
    }
    void AdRequest()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void ShowVideo()
    {
//#if UNITY_EDITOR
//        StartCoroutine(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().giftBoxClickIEnumerator());
//#endif
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            VideoRequest();
        }
        else
        {
            VideoRequest();
        }

    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {

            this.interstitial.Show();
            AdRequest();
        }
        else
        {
            AdRequest();
        }

    }

}
