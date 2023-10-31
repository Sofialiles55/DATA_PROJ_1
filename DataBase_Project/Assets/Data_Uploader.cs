using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Data_Uploader : MonoBehaviour
{
    
    public uint userID;
    

    private void OnEnable()
    {
        Simulator.OnNewPlayer += SendPlayer;
        Simulator.OnNewSession += SendSessionInProgress;
        Simulator.OnEndSession += SendSessionClosing;
        Simulator.OnBuyItem += SendBuyItem;
    }
    private void OnDisable()
    {
        Simulator.OnNewPlayer -= SendPlayer;
        Simulator.OnNewSession -= SendSessionInProgress;
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
    private void SendSessionInProgress(DateTime date)
    {
        StartCoroutine(UploadSession(date, true));
    }
    private void SendSessionClosing(DateTime date)
    {
        StartCoroutine(UploadSession(date, false));
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
        //form.AddField("Id", id);
        form.AddField("Name", name);
        form.AddField("Country", country);
        form.AddField("Date", date.ToString("yyyy-MM-dd HH:mm:ss"));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Data.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Player form upload complete!");
            Debug.Log(www.downloadHandler.text);

            userID = uint.Parse(www.downloadHandler.text);
            
            CallbackEvents.OnAddPlayerCallback.Invoke(userID);
            
        }
    }
    IEnumerator UploadSession(DateTime uploadedDate, bool sessionInProgress)
    {
        WWWForm form = new WWWForm();

        if (sessionInProgress)
        {
            form.AddField("Start Session", uploadedDate.ToString("yyyy-MM-dd HH:mm:ss"));
              form.AddField("Session in progress", "1");
        }
        else
        {
            form.AddField("End Session", uploadedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            form.AddField("Session in progress", "0");
        }
        
        

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Data.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Sessions form upload complete!");
            Debug.Log(www.downloadHandler.text);

            CallbackEvents.OnNewSessionCallback.Invoke(userID);

        }
    }
    IEnumerator UploadItem(int item, DateTime date)
    {
        WWWForm form = new WWWForm();       
        form.AddField("Item", item);
        form.AddField("Buy Date", date.ToString("yyyy-MM-dd HH:mm:ss"));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Data.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Buy form upload complete!");
            Debug.Log(www.downloadHandler.text);

            userID = uint.Parse(www.downloadHandler.text);

            CallbackEvents.OnItemBuyCallback.Invoke();

        }
    }

}
