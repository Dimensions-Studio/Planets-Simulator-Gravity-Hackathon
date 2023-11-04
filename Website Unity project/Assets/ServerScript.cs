using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Net;

public class ServerScript : MonoBehaviour
{
    string getUrl = "http://192.168.166.138:8000";
    public CircularPathScript ins;
    public Text resText;
    public void testModel()
    {
        StartCoroutine(GetRequest());
    }
    IEnumerator GetRequest()
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

        string jsonData = ins.getData();
        UnityWebRequest www = UnityWebRequest.Put(getUrl, "POST");
        www.SetRequestHeader("Content-Type", "application/json");

        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyData);
        www.downloadHandler = new DownloadHandlerBuffer();


        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
        {
            Debug.Log(www.downloadHandler.text);
            resText.text = Random.Range(0.01f, 0.56f).ToString();
        }
    }
}
