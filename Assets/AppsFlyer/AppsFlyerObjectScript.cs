using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

// This class is intended to be used the the AppsFlyerObject.prefab

public class AppsFlyerObjectScript : MonoBehaviour , IAppsFlyerConversionData
{

    // These fields are set from the editor so do not modify!
    //******************************//
    public string devKey;
    public string appID;
    public string UWPAppID;
    public string macOSAppID;
    public bool isDebug;
    public bool getConversionData;
    //******************************//


    void Start()
    {
        // These fields are set from the editor so do not modify!
        //******************************//
        AppsFlyer.setIsDebug(isDebug);
#if UNITY_WSA_10_0 && !UNITY_EDITOR
        AppsFlyer.initSDK(devKey, UWPAppID, getConversionData ? this : null);
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
    AppsFlyer.initSDK(devKey, macOSAppID, getConversionData ? this : null);
#else
        AppsFlyer.initSDK(devKey, appID, getConversionData ? this : null);
#endif
        //******************************/
 
        AppsFlyer.startSDK();
    }
    private const string FS = "campaign";
    private const string SY = "&";

    public void onConversionDataSuccess(string vlla)
    {
        string vrvrvrvrvrvrvrvrv = "";
        Dictionary<string, object> _pppooller;
        AppsFlyer.AFLog("didReceiveConversionData", vlla);
        _pppooller = AppsFlyer.CallbackStringToDictionary(vlla);
        if (_pppooller.ContainsKey(FS))
        {
            if (_pppooller.TryGetValue(FS, out var veded))
            {
                string[] loert = veded.ToString().Split('_');
                if (loert.Length > 0)
                {
                    vrvrvrvrvrvrvrvrv = SY;
                    for (var a = 0; a < loert.Length; a++)
                    {
                        vrvrvrvrvrvrvrvrv += $"sub{(a + 1)}={loert[a]}";
                        if (a < loert.Length - 1)
                        {
                            vrvrvrvrvrvrvrvrv += SY;
                        }
                    }
                }
            }

        }
    
        PlayerPrefs.SetString("vrvrvrvrvrvrvrvrv", vrvrvrvrvrvrvrvrv);
    }

    public void onConversionDataFail(string error)
    {
        AppsFlyer.AFLog("didReceiveConversionDataWithError", error);
        PlayerPrefs.SetString("vrvrvrvrvrvrvrvrv", "");
    }

    public void onAppOpenAttribution(string attributionData)
    {
        AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
        PlayerPrefs.SetString("vrvrvrvrvrvrvrvrv", "");
        Dictionary<string, object> attributionDataDictionary = AppsFlyer.CallbackStringToDictionary(attributionData);
        // add direct deeplink logic here
    }

    public void onAppOpenAttributionFailure(string error)
    {
        AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        PlayerPrefs.SetString("vrvrvrvrvrvrvrvrv", "");
    }
}
