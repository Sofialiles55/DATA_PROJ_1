using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Data_Uploader : MonoBehaviour
{
    public string characterName;
    public int id;
    void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", id);
        form.AddField("Name", characterName);

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
            
        }
    }
}
