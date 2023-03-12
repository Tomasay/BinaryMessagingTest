using UnityEngine;
using System;
using BestHTTP.SocketIO3;
using System.IO;
using BestHTTP.SocketIO3.Parsers;

public class MessageTester : MonoBehaviour
{
    SocketManager manager;
    private const string url = "https://vrpartygame.herokuapp.com/";
    private const string socketUrl = url + "socket.io/";

    void Awake()
    {
        /*
        SocketOptions socketOptions = new SocketOptions();
        socketOptions.ConnectWith = BestHTTP.SocketIO3.Transports.TransportTypes.WebSocket;
        manager = new SocketManager(new Uri(socketUrl), socketOptions);
        */

        manager = new SocketManager(new Uri(socketUrl));
        //manager.Parser = new MsgPackParser();

        manager.Socket.Once("connect", () => Debug.Log("connected!"));

        InvokeRepeating("SendTestData", 0, 0.05f);

        DontDestroyOnLoad(gameObject);
    }

    void OnApplicationQuit()
    {
        manager?.Close();
        manager?.Socket?.Disconnect();
    }

    void SendTestData()
    {
        byte[] bytes;

        //Dummy data, should be 94 bytes
        using (MemoryStream m = new MemoryStream())
        {
            using (BinaryWriter writer = new BinaryWriter(m))
            {
                writer.Write(new SerializedVector3(0, 0, 0));
                writer.Write(new SerializedVector3(0, 0, 0));
                writer.Write(new SerializedVector3(0, 0, 0));

                writer.Write(new SerializedQuaternion(0, 0, 0, 0));
                writer.Write(new SerializedQuaternion(0, 0, 0, 0));
                writer.Write(new SerializedQuaternion(0, 0, 0, 0));

                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
                writer.Write((byte)0);
            }
            bytes = m.ToArray();
        }

        manager.Socket.Emit("XRDataToServer", bytes);
        Debug.Log("Sent dummy data with length: " + bytes.Length);
    }
}
