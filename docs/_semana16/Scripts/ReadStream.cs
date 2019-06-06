// based on this: http://answers.unity3d.com/questions/1085293/www-not-getting-site-that-doesnt-end-on-html.html 
using UnityEngine;
using System.Collections.Generic;
using System.Net;
using UnityEngine.UI;
using System.Collections;
using System;

public class ReadStream : MonoBehaviour
{
	public string PhotonParticleURL = "https://api.particle.io/v1/devices/events?access_token=2bfa351914829e80ca3b7012f5d3a4febed47cd6";
	WebStreamReader request = null;
    DataClassLed parseLedState = new DataClassLed();

    public class DataClassLed
    {
        public string data;
    }

    void Start()
	{
		StartCoroutine(WRequest());
	}

	void Update() {

	}

	IEnumerator WRequest()
	{
		request = new WebStreamReader();
		request.Start(PhotonParticleURL);
		string stream = "";
		while (true)
		{
			string block = request.GetNextBlock();
			if (!string.IsNullOrEmpty(block))
			{
				stream += block;
				Debug.Log ("Stream1: " + stream);
				string[] data = stream.Split(new string[] { "\n\n" }, System.StringSplitOptions.None);
				Debug.Log ("Data length: " + data.Length);
				stream = data[data.Length - 1];

				for (int i = 0; i < data.Length - 1; i++)
				{
					if (!string.IsNullOrEmpty(data[i]))
					{
						Debug.Log ("Data: " + data [i]); // print all block of data (event + data)
						if (data [i].Contains ("led")) {
							string output = data [i].Substring(data [i].IndexOf("{"));
                            parseLedState = JsonUtility.FromJson<DataClassLed> (output);
                            gameObject.GetComponent<IoT>().ledState = parseLedState.data;
                        }
					}
				}
			
			}
			yield return new WaitForSeconds(1);
		}
	}

	void OnApplicationQuit()
	{
		if (request != null)
			request.Dispose();
	}

	void OnDataUpdate(decimal aAmount)
	{
		Debug.Log("Received new amount: " + aAmount);
	}
}