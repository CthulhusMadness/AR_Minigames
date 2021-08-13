using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    #region Fields

    public ConnectionSettings connectionSettings = null;

    #endregion

    #region UnityCallbacks

    private void Start()
    {
        if (connectionSettings)
            Connect();
    }

    #endregion

    #region Methods

    private void Connect()
    {
        var authValues = new AuthenticationValues(Random.Range(0, 9999).ToString());
        PhotonNetwork.AuthValues = authValues;

        PhotonNetwork.SendRate = connectionSettings.sendRate; // pacchetti al secondo default = 20
        PhotonNetwork.SerializationRate = connectionSettings.serializationRate; // quante volte vengono serializzate le view, default = 10

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = connectionSettings.Nickname;
        PhotonNetwork.GameVersion = connectionSettings.gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to Master");
        print("Nickname: " + PhotonNetwork.NickName);
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from master " + cause);
    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby");
    }

    public void NewUser()
    {

    }

    #endregion
}
