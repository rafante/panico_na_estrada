using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class Propagandas : MonoBehaviour
{
    private BannerView bannerView;
    private RewardBasedVideoAd rewardBasedVideo;
    public static Propagandas instancia;

    public void Start()
    {
        if (instancia == null)
        {
            instancia = this;
        }
#if UNITY_ANDROID
        string appId = "ca-app-pub-1343647000894788~1193651346";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-1343647000894788~1193651346";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        this.RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1343647000894788/6574519276";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-1343647000894788/6574519276";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void RequestRewardBasedVideo()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-1343647000894788/9700156313";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-1343647000894788/9700156313";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.LoadAd(request, adUnitId);
        
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Historia.instancia.Salvar(true);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}