using Photon.Pun;
using Photon.Realtime;
using System;
using Random = UnityEngine.Random;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    #region Fields

    public ConnectionSettings connectionSettings = null;

    public event Action OnNewUser;
    public event Action OnConnectionEnd;
    public event Action OnConnectionFail;

    #endregion

    #region UnityCallbacks

    

    #endregion

    #region Methods

    public bool Initiate()
    {
        if (connectionSettings)
        {
            if (connectionSettings.isNewUser)
            {
                OnNewUser?.Invoke();
                return false;
            }
            else
                Connect();
            return true;
        }
        return false;
    }

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
        OnConnectionEnd?.Invoke();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from master " + cause);
        OnConnectionFail?.Invoke();
    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby");
    }

    #endregion
}
