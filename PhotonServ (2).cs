// Интегрированые в IDE библеотеки
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Библеотека с сетью
using Photon.Pun;

public class PhotonServ : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _connectUser;
    [SerializeField] private TextMeshProUGUI _Info;
    [SerializeField] private Button _findBtn;
    [SerializeField] private PhotonView _phv;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _findBtn.enabled = true;
        _connectUser.text = "Соединение с " + PhotonNetwork.CountOfPlayers;
        _phv = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    [PunRPC]
    public void GetBatteryStatus(float battery)
    {
        battery = battery * 100;
        _Info.text = "Информация об удаленном устройстве: \n battery: " + battery.ToString() + "%";
    }

    public void StartLocal()
    {
        _phv.RPC("StartAllUserSound", RpcTarget.All);
    }

    public void StopLocal()
    {
        _phv.RPC("StopAllUserSound", RpcTarget.All);
    }

    [PunRPC]
    public void StartAllUserSound()
    {
        if (_phv.IsMine)
            return;

        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<PhotonView>().RPC("GetBatteryStatus", RpcTarget.All, SystemInfo.batteryLevel);
    }

    [PunRPC]
    public void StopAllUserSound()
    {
        if (_phv.IsMine)
            return;

        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<PhotonView>().RPC("GetBatteryStatus", RpcTarget.All, SystemInfo.batteryLevel);
    }
}
