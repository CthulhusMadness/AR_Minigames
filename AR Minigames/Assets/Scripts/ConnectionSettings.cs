using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "connection_settings", menuName = "Networking/ConnectionSettings")]
public class ConnectionSettings : ScriptableObject
{
    public string gameVersion = "1";
    public string nickname = "user";
    public bool isNewUser = true;
    public string Nickname 
    {
        get {
            int value = Random.Range(0, 9999);
            return nickname + "_" + value;
        }
    }
    public int serializationRate = 10;
    public int sendRate = 20;
}
