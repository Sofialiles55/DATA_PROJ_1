using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Data_Uploader : MonoBehaviour
{
    
    public uint userID;
    DateTime sessionStartDate;

    private void OnEnable()
    {
        Simulator.OnNewPlayer += SendPlayer;
        Simulator.OnNewSession += CreateSession;
        Simulator.OnEndSession += SendSession;
    }
    private void OnDisable()
    {
        Simulator.OnNewPlayer -= SendPlayer;
        Simulator.OnNewSession -= CreateSession;
        Simulator.OnEndSession -= SendSession;
    }

    private void SendPlayer(string name, string country, DateTime date)
    {
        StartCoroutine(UploadPlayer( name, country, date));
    }
    void CreateSession(DateTime date)
    {
        sessionStartDate = date;
    }
    private void SendSession(DateTime date)
    {
        StartCoroutine(UploadSession(sessionStartDate, date));
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
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);

            userID = uint.Parse(www.downloadHandler.text);
            
            CallbackEvents.OnAddPlayerCallback.Invoke(userID);
            
        }
    }
    IEnumerator UploadSession(DateTime startDate, DateTime endDate)
    {
        WWWForm form = new WWWForm();

        form.AddField("Start Session", startDate.ToString("yyyy-MM-dd HH:mm:ss"));
        form.AddField("End Session", endDate.ToString("yyyy-MM-dd HH:mm:ss"));


        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~fernandofg2/Receive_Data.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);

            CallbackEvents.OnNewSessionCallback.Invoke(userID);

        }
    }
}
