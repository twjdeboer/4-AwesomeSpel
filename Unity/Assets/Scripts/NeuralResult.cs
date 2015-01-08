using UnityEngine;
using System.Collections;

public class NeuralResult : MonoBehaviour {

	private double[][] W1 = new double[][]
	{
		new double[] {-0.93555,0.30249,-0.35401,-0.48818,-0.90505,-0.64421,0.058168,0.32731,-0.61747},
		new double[] {-0.26276,-0.037327,-0.54831,-0.98497,0.067601,0.67235,-0.066804,0.34365,-0.1671},
		new double[] {-0.44215,-0.79562,-0.026015,0.77338,-0.50745,-0.56515,-0.12096,-0.54113,-0.75341},
		new double[] {0.50668,-0.76576,-0.82983,0.89052,-0.40909,0.23417,0.75789,-0.10048,0.52234},
		new double[] {0.9578,0.88102,-0.15237,-0.23362,0.41094,-0.67571,0.29437,-0.32257,0.48923},
		new double[] {-0.5846,0.34435,0.3066,-0.81398,-0.71128,-0.20973,-0.76635,0.58472,-0.10486},
		new double[] {0.4961,0.74166,-0.24019,-0.73863,-0.78046,0.61195,0.92337,0.91377,0.35412},
		new double[] {0.5701,1.0441,0.54077,-0.33122,0.12607,0.21481,0.31823,-0.15898,0.63071},
		new double[] {0.069024,0.5729,0.19151,0.22088,-0.64635,0.76019,0.038643,0.953,-0.59414},
		new double[] {0.84044,0.18934,1.039,0.74667,-0.16563,0.37713,0.5901,0.28053,-0.64654}
	};

	private double[][] W2 = new double[][]
	{
		new double[] {0.78355,-0.18371,-0.22386,0.58024,-0.68041,0.54887,0.68227,-0.89865,-0.65381,0.28919},
		new double[] {-0.42793,-0.9035,0.03661,0.99022,-0.39727,-0.75846,0.2371,0.63998,-0.34588,1.7474}
	};

	private double[] T1 = new double[] {-0.047772,0.94058,0.63986,-1.0442,0.79251,0.90971,-0.70145,0.87157,-0.42895,0.23156};

	private double[] T2 = new double[] {0.25463,1.6389};

	public double[] foundevidence;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int CalcOutput( double[] n){

		double[] hidden = new double[10];
		hidden = VectorMatrix(n,W1);
		hidden = VectorAftrekken(hidden, T1);
		hidden = VectorSign(hidden);

		double[] result = new double[2];
		result = VectorMatrix(hidden, W2);
		result = VectorAftrekken(result, T2);
		result = VectorSign(result);

		double maxValue = result.Max();
 		int maxIndex = result.ToList().IndexOf(maxValue);

 		return maxIndex++;
	}

	double[] VectorMatrix(double[] m, double[][] n){

		double[] answer = new double[n[0].Length];

		for (int i = 0 ; i<n.Length; i++)
		{
			for (int j = 0 ; j<m.Length; j++)
			{
				answer[i] += m[j]*n[i,j];
			}
		}
		return answer;
	}

	double[] VectorAftrekken(double[] n , double[] m){

		double[] answer = new double[n.Length];
		for (int i=0; i<n.Length; i++) {
				answer [i] = n [i] - m [i];
		}
		return answer; 

	}

	double[] VectorSign(double[] n){

		double[] answer = new double[n.Length];
		for (int i = 0; i<n.Length; i++) 
		{
				answer [i] = 1 / (1 + Mathf.Exp (-1 * n [i]));
		}
		return answer;
	}

}
