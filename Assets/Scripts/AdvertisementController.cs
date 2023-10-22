using System.Collections;
using UnityEngine;
using AppsFlyerSDK;
using UnityEngine.Networking;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class AdvertisementController : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private GameObject _stub;
    [SerializeField] private GameObject _advertisement;
    [SerializeField] private float _vaitRequestTime;
    private string _idfa;
    private string _url;

    void Awake()
    {
        DontDestroyOnLoad(this);
        CheckIDFA();
        StartCoroutine(GetStatusCorutine());

        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            _stub.SetActive(true);
            _advertisement.SetActive(false);
        }

        StartCoroutine(FetchDataRequestCorutine());
    }

    private void LoadPrivacy()
    {
        var url = $"{_url}?idfa={_idfa}&gaid={AppsFlyer.getAppsFlyerId()}";
        
    }

    private void GetIDFA()
    {
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
#if UNITY_IOS
        if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        {
            Application.RequestAdvertisingIdentifierAsync((value, _, _) => _idfa = value);
        }
        else
        {
            _idfa = status.ToString();
        }
#endif
    }

    private void CheckIDFA()
    {
#if UNITY_IOS
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        if(status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif
    }

    private IEnumerator GetStatusCorutine()
    {
#if UNITY_IOS
        var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        while(status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
            yield return null;
        }
#endif
        GetIDFA();
        yield return null;
    }

    private IEnumerator FetchDataRequestCorutine()
    {
        yield return new WaitForSeconds(_vaitRequestTime);
        var url = PlayerPrefs.GetString("url", "");
        if (!string.IsNullOrEmpty(url))
        {
            _url = url;
            LoadPrivacy();
        }
        else if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _stub.SetActive(true);
            _advertisement.SetActive(false);
        }
        else
        {
            string hgf = "";//GetJoinStr - звідки?
            using (UnityWebRequest request = new UnityWebRequest(hgf))
            {
                yield return request.SendWebRequest();
                if(request.result == UnityWebRequest.Result.ConnectionError)
                {
                    _stub.SetActive(true);
                    _advertisement.SetActive(false);
                }
                else
                {
                    string h = "";//GetJoinStr - звідки?
                    if(request.downloadHandler.text.Contains(h)) 
                    { 
                        PlayerPrefs.SetString("url", request.downloadHandler.text);
                        LoadPrivacy();
                    }
                    else
                    {
                        _stub.SetActive(true);
                        _advertisement.SetActive(false);
                    }
                }

            }
        }

        yield return null;
    }
}
