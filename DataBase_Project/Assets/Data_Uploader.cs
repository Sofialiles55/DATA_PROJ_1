using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Data_Uploader : MonoBehaviour
{
    
    uint userId;
    uint sessionId;
    uint purchaseId;
    

    private void OnEnable()
    {
        Simulator.OnNewPlayer += SendPlayer;
        Simulator.OnNewSession += SendSessionStart;
        Simulator.OnEndSession += SendSessionClosing;
        Simulator.OnBuyItem += SendBuyItem;
    }
    private void OnDisable()
    {
        Simulator.OnNewPlayer -= SendPlayer;
        Simulator.OnNewSession -= SendSessionStart;
        Simulator.OnEndSession -= SendSessionClosing;
        Simulator.OnBuyItem -= SendBuyItem;
    }

    private void SendPlayer(string name, string country, DateTime date)
    {
        StartCoroutine(UploadPlayer( name, country, date));
    }
    private void SendBuyItem(int item, DateTime date)
    {
        StartCoroutine(UploadItem( item, date));
    }
    private void SendSessionStart(DateTime date)
    {
        StartCoroutine(UploadStartSession(date));
    }
    private void SendSessionClosing(DateTime date)
    {
        StartCoroutine(UploadEndSession(date));
    }

    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    IEnumerator UploadPlayer(string name, string country,DateTime date)
    {
        WWWForm form = new WWWForm();
        
        form.AddField("Name", name);
        form.AddField("Country", country);
        form.AddField("Date", date.ToString("yyyy-MM-dd HH:mm:ss"));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Player_Data.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Player form upload complete!");
            Debug.Log(www.downloadHandler.text);

            
            uint.TryParse(www.downloadHandler.text, out userId);
            
            CallbackEvents.OnAddPlayerCallback.Invoke(userId);
            
        }
    }
    IEnumerator UploadStartSession(DateTime uploadedDate)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www;
        form.AddField("User_ID", userId.ToString());
        form.AddField("Start_Session", uploadedDate.ToString("yyyy-MM-dd HH:mm:ss"));



        www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Session_Data.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("Sessions form upload complete!");
            // Debug.Log(www.downloadHandler.text);
            uint.TryParse(www.downloadHandler.text, out sessionId);

            CallbackEvents.OnNewSessionCallback.Invoke(sessionId);

        }

    }
    IEnumerator UploadEndSession(DateTime uploadedDate)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www;
        form.AddField("User_ID", userId.ToString());
        form.AddField("End_Session", uploadedDate.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("Session_ID", sessionId.ToString());


        www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Close_Session_Data.php", form);


        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("Sessions form end complete!");
            // Debug.Log(www.downloadHandler.text);
            //uint.TryParse(www.downloadHandler.text, out sessionId);

            CallbackEvents.OnEndSessionCallback.Invoke(sessionId);

        }

        //Debug.Log(www.downloadHandler.text);
    }
    IEnumerator UploadItem(int item, DateTime date)
    {
        WWWForm form = new WWWForm();       
        form.AddField("Item", item);
        form.AddField("User_ID", userId.ToString());
        form.AddField("Session_ID", sessionId.ToString());
        form.AddField("Buy_Date", date.ToString("yyyy-MM-dd HH:mm:ss"));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Purchase_Data.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Debug.Log("Buy form upload complete!");
            //Debug.Log(www.downloadHandler.text);

            uint.TryParse(www.downloadHandler.text, out purchaseId);

            CallbackEvents.OnItemBuyCallback.Invoke();

        }
    }

}
