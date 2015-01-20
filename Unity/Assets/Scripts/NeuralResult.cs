using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

public class NeuralResult : MonoBehaviour {

	private float[][] W1 = new float[][]
	{
		new float[] {-0.93555f,0.30249f,-0.35401f,-0.48818f,-0.90505f,-0.64421f,0.058168f,0.32731f,-0.61747f},
		new float[] {-0.26276f,-0.037327f,-0.54831f,-0.98497f,0.067601f,0.67235f,-0.066804f,0.34365f,-0.1671f},
		new float[] {-0.44215f,-0.79562f,-0.026015f,0.77338f,-0.50745f,-0.56515f,-0.12096f,-0.54113f,-0.75341f},
		new float[] {0.50668f,-0.76576f,-0.82983f,0.89052f,-0.40909f,0.23417f,0.75789f,-0.10048f,0.52234f},
		new float[] {0.9578f,0.88102f,-0.15237f,-0.23362f,0.41094f,-0.67571f,0.29437f,-0.32257f,0.48923f},
		new float[] {-0.5846f,0.34435f,0.3066f,-0.81398f,-0.71128f,-0.20973f,-0.76635f,0.58472f,-0.10486f},
		new float[] {0.4961f,0.74166f,-0.24019f,-0.73863f,-0.78046f,0.61195f,0.92337f,0.91377f,0.35412f},
		new float[] {0.5701f,1.0441f,0.54077f,-0.33122f,0.12607f,0.21481f,0.31823f,-0.15898f,0.63071f},
		new float[] {0.069024f,0.5729f,0.19151f,0.22088f,-0.64635f,0.76019f,0.038643f,0.953f,-0.59414f},
		new float[] {0.84044f,0.18934f,1.039f,0.74667f,-0.16563f,0.37713f,0.5901f,0.28053f,-0.64654f}
	};

	private float[][] W2 = new float[][]
	{
		new float[] {0.78355f,-0.18371f,-0.22386f,0.58024f,-0.68041f,0.54887f,0.68227f,-0.89865f,-0.65381f,0.28919f},
		new float[] {-0.42793f,-0.9035f,0.03661f,0.99022f,-0.39727f,-0.75846f,0.2371f,0.63998f,-0.34588f,1.7474f}
	};

	private float[] T1 = new float[] {-0.047772f,0.94058f,0.63986f,-1.0442f,0.79251f,0.90971f,-0.70145f,0.87157f,-0.42895f,0.23156f};

	private float[] T2 = new float[] {0.25463f,1.6389f};

	public float[] foundevidence;
	public int uitkomst;



	// Use this for initialization
	void Start () {

	
		Setfoundevidence ();

		uitkomst = CalcOutput(foundevidence);

		UpdateSave (); 

		UpdateServer ();


	}
	
	//// Update is called once per frame
	//void Update () {
	
	//}

	void UpdateSave() {
		string filename = "cloud.save";
		string[] nieuw = File.ReadAllLines (filename);
		
		nieuw[13] = "True";
		if (this.uitkomst == 2)
		{
			nieuw[14] = "True";
		} 
				
		StreamWriter sr = File.CreateText (filename);
		for(int i = 0; i<15; i++)
		{
			sr.WriteLine(nieuw[i]);

		}
		sr.Close ();
	}

	void UpdateServer () {

		string[] content = new string[15];
		
		string filename = "cloud.save";
		
		if (File.Exists (filename)) {
			content = File.ReadAllLines (filename);
		} else {
			Debug.Log ("No Save File found");
		}

		//update boolean 9
		string url = "http://drproject.twi.tudelft.nl:8084/pickupitem?userId=" + content [0] + "&itemId=" + 9;
		WWW www = new WWW (url);
		StartCoroutine (GETAddEvidence (www));

		//update boolean 10
		if (uitkomst == 2)
		{
			string url = "http://drproject.twi.tudelft.nl:8084/pickupitem?userId=" + content [0] + "&itemId=" + 10;
			WWW www = new WWW (url);
			StartCoroutine (GETAddEvidence (www));
		}




	}

	IEnumerator GETAddEvidence(WWW www){
		yield return www;

		if (www.error == null) {
			Debug.Log ("SUCCESS");
				} else {
			Debug.Log ("WWW Error:" + www.error	);
		}
	}


	void Setfoundevidence(){
		string filename = "cloud.save";
		bool[] items = new bool[9];
		
		
		string[] content = File.ReadAllLines (filename);
		
		for (int i = 0; i<9; i++) 
		{
			if (bool.Parse( content[i+4])){
				foundevidence[i]=1f;
			}
			else
			{
				foundevidence[i]=0f;
			}
		}


	}

	int CalcOutput( float[] n){

		//calc hidden layer
		float[] hidden = new float[10];
		hidden = VectorMatrix(n, W1);
		hidden = VectorAftrekken(hidden, T1);
		hidden = VectorSign(hidden);

		//calc output
		float[] result = new float[2];
		result = VectorMatrix(hidden, W2);
		result = VectorAftrekken(result, T2);
		result = VectorSign(result);

		//select output
		int ans = 1;
		if (result[1]>result[0])
		{
			ans++;
		}

		return ans;

	}

	float[] VectorMatrix(float[] m, float[][] n){

		float[] answer = new float[n.Length];

		for (int i = 0 ; i<n.Length; i++)
		{
			for (int j = 0 ; j<m.Length; j++)
			{
				answer[i] += m[j]*n[i][j];
			}
		}
		return answer;
	}

	float[] VectorAftrekken(float[] n , float[] m){

		float[] answer = new float[n.Length];
		for (int i=0; i<n.Length; i++) 
		{
			answer [i] = n [i] - m [i];
		}
		return answer; 

	}

	float[] VectorSign(float[] n){

		float[] answer = new float[n.Length];
		for (int i = 0; i<n.Length; i++) 
		{
			answer [i] = 1 / (1 + Mathf.Exp (-1 * n [i]));
		}
		return answer;
	}

}