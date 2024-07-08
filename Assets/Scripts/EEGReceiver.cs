using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class EEGReceiver : MonoBehaviour
{
    private TcpClient client;
    private StreamReader reader;
    private const string host = "127.0.0.1";
    private const int port = 65432;

    void Start()
    {
        try
        {
            client = new TcpClient(host, port);
            reader = new StreamReader(client.GetStream());
            Debug.Log("Connected to EEG data server.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to connect to server: {e.Message}");
        }
    }

    void Update()
    {
        if (client != null && client.Connected)
        {
            try
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    string[] values = data.Split(',');
                    float alphaMetric = float.Parse(values[0]);
                    float betaMetric = float.Parse(values[1]);
                    // Use the alpha and beta metrics in your game
                    Debug.Log($"Alpha: {alphaMetric}, Beta: {betaMetric}");
                    // For example, you can update some game object's properties based on these metrics
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error reading from server: {e.Message}");
            }
        }
    }

    void OnApplicationQuit()
    {
        reader?.Close();
        client?.Close();
    }
}
