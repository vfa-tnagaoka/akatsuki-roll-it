using com.adjust.sdk;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class AdjustManager :Singleton<AdjustManager>
    {
        public const string Adjust_Token = "fxqh135eil1c";
        public long[] Adjust_Secret = new long[] { 1, 1860670797, 1091264936, 1166424827, 367614031 };
        public void Initialized()
        {
            AdjustConfig adjustConfig = new AdjustConfig(Adjust_Token, AdjustEnvironment.Production);
            adjustConfig.setAppSecret(Adjust_Secret[0], Adjust_Secret[1], Adjust_Secret[2], Adjust_Secret[3], Adjust_Secret[4]);
            adjustConfig.setLogLevel(AdjustLogLevel.Verbose);
            adjustConfig.setLogDelegate(LogCallBack);
            adjustConfig.setEventSuccessDelegate(EventSuccessCallback);
            adjustConfig.setEventFailureDelegate(EventFailureCallback);
            adjustConfig.setSessionSuccessDelegate(SessionSuccessCallback);
            adjustConfig.setSessionFailureDelegate(SessionFailureCallback);
            adjustConfig.setDeferredDeeplinkDelegate(DeferredDeeplinkCallback);
            adjustConfig.setAttributionChangedDelegate(AttributionChangedCallback);
            Adjust.start(adjustConfig);
        }

        public void TrackSimpleEvent(string eventToken)
        {
            Debug.Log("[Adjust] TrackSimpleEvent eventToken[" + eventToken+"]");
            AdjustEvent adjustEvent = new AdjustEvent(eventToken);
            Adjust.trackEvent(adjustEvent);
        }

        private void LogCallBack(string msg)
        {
            Debug.Log("Adjust>> " +msg);
        }

        public void HandleGooglePlayId(String adId)
        {
            Debug.Log("Google Play Ad ID = " + adId);
        }

        public void AttributionChangedCallback(AdjustAttribution attributionData)
        {
            LogCallBack("Attribution changed!");

            if (attributionData.trackerName != null)
            {
                Debug.Log("Tracker name: " + attributionData.trackerName);
            }
            if (attributionData.trackerToken != null)
            {
                Debug.Log("Tracker token: " + attributionData.trackerToken);
            }
            if (attributionData.network != null)
            {
                Debug.Log("Network: " + attributionData.network);
            }
            if (attributionData.campaign != null)
            {
                Debug.Log("Campaign: " + attributionData.campaign);
            }
            if (attributionData.adgroup != null)
            {
                Debug.Log("Adgroup: " + attributionData.adgroup);
            }
            if (attributionData.creative != null)
            {
                Debug.Log("Creative: " + attributionData.creative);
            }
            if (attributionData.clickLabel != null)
            {
                Debug.Log("Click label: " + attributionData.clickLabel);
            }
            if (attributionData.adid != null)
            {
                Debug.Log("ADID: " + attributionData.adid);
            }
        }

        public void EventSuccessCallback(AdjustEventSuccess eventSuccessData)
        {
            LogCallBack("Event tracked successfully!");

            if (eventSuccessData.Message != null)
            {
                LogCallBack("Message: " + eventSuccessData.Message);
            }
            if (eventSuccessData.Timestamp != null)
            {
                Debug.Log("Timestamp: " + eventSuccessData.Timestamp);
            }
            if (eventSuccessData.Adid != null)
            {
                Debug.Log("Adid: " + eventSuccessData.Adid);
            }
            if (eventSuccessData.EventToken != null)
            {
                Debug.Log("EventToken: " + eventSuccessData.EventToken);
            }
            if (eventSuccessData.CallbackId != null)
            {
                Debug.Log("CallbackId: " + eventSuccessData.CallbackId);
            }
            if (eventSuccessData.JsonResponse != null)
            {
                Debug.Log("JsonResponse: " + eventSuccessData.GetJsonResponse());
            }
        }

        public void EventFailureCallback(AdjustEventFailure eventFailureData)
        {
            LogCallBack("Event tracking failed!");

            if (eventFailureData.Message != null)
            {
                LogCallBack("Message: " + eventFailureData.Message);
            }
            if (eventFailureData.Timestamp != null)
            {
                Debug.Log("Timestamp: " + eventFailureData.Timestamp);
            }
            if (eventFailureData.Adid != null)
            {
                Debug.Log("Adid: " + eventFailureData.Adid);
            }
            if (eventFailureData.EventToken != null)
            {
                Debug.Log("EventToken: " + eventFailureData.EventToken);
            }
            if (eventFailureData.CallbackId != null)
            {
                Debug.Log("CallbackId: " + eventFailureData.CallbackId);
            }
            if (eventFailureData.JsonResponse != null)
            {
                Debug.Log("JsonResponse: " + eventFailureData.GetJsonResponse());
            }

            Debug.Log("WillRetry: " + eventFailureData.WillRetry.ToString());
        }

        public void SessionSuccessCallback(AdjustSessionSuccess sessionSuccessData)
        {
            LogCallBack("Session tracked successfully!");

            if (sessionSuccessData.Message != null)
            {
                LogCallBack("Message: " + sessionSuccessData.Message);
            }
            if (sessionSuccessData.Timestamp != null)
            {
                Debug.Log("Timestamp: " + sessionSuccessData.Timestamp);
            }
            if (sessionSuccessData.Adid != null)
            {
                Debug.Log("Adid: " + sessionSuccessData.Adid);
            }
            if (sessionSuccessData.JsonResponse != null)
            {
                Debug.Log("JsonResponse: " + sessionSuccessData.GetJsonResponse());
            }
        }

        public void SessionFailureCallback(AdjustSessionFailure sessionFailureData)
        {
            LogCallBack("Session tracking failed!");

            if (sessionFailureData.Message != null)
            {
                LogCallBack("Message: " + sessionFailureData.Message);
            }
            if (sessionFailureData.Timestamp != null)
            {
                Debug.Log("Timestamp: " + sessionFailureData.Timestamp);
            }
            if (sessionFailureData.Adid != null)
            {
                Debug.Log("Adid: " + sessionFailureData.Adid);
            }
            if (sessionFailureData.JsonResponse != null)
            {
                Debug.Log("JsonResponse: " + sessionFailureData.GetJsonResponse());
            }

            Debug.Log("WillRetry: " + sessionFailureData.WillRetry.ToString());
        }

        private void DeferredDeeplinkCallback(string deeplinkURL)
        {
            Debug.Log("Deferred deeplink reported!");

            if (deeplinkURL != null)
            {
                Debug.Log("Deeplink URL: " + deeplinkURL);
            }
            else
            {
                Debug.Log("Deeplink URL is null!");
            }
        }
    }
}

