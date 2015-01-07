using UnityEngine;
using System.Collections;

public class NeuralNetwork : MonoBehaviour {

	public int AantalBewijzen;
	public int AantalUitkomsten; //twee denk ik
	public int NumHidden;
	private double error = 100;
	private double e = System.Math.E;


	// Use this for initialization
	void Start () {
	
		// Maak het neural network
		double[,] W1 = new double[AantalBewijzen, NumHidden];
		double[,] W2 = new double[NumHidden, AantalUitkomsten];
		double[] T1 = new double[NumHidden];
		double[] T2 = new double[AantalUitkomsten];

		// Insert random weigths
		for (int i = 0; i < AantalBewijzen; i++) 
		{
			for (int j = 0; j < NumHidden; j++) 
			{
				double blabla = Random.Range (0.0f, 1.0f);
				W1 [i, j] = (blabla * AantalBewijzen) - (AantalBewijzen / 2);
			}
		}
		for (int i = 0; i < NumHidden; i++) 
		{
			for (int j = 0; j < AantalUitkomsten; j++) 
			{
				double blabla = Random.Range (0.0f, 1.0f);
				W2 [i, j] = (blabla * NumHidden) - (NumHidden / 2);
			}
		}
		for (int i = 0; i < NumHidden; i++) 
		{
			double blabla = Random.Range (0.0f, 1.0f);
			T1[i] = (blabla * NumHidden) - (NumHidden / 2);
		}

		for (int i = 0; i < AantalUitkomsten; i++) 
		{
			double blabla = Random.Range (0.0f, 1.0f);
			T2[i] = (blabla * AantalUitkomsten) - (AantalUitkomsten / 2);
		}
		//define a random testset
		int[,] TestSet = new int[50, AantalBewijzen + 1];

		for (int i = 0; i<50; i++) 
		{
			for (int j=0; j<AantalBewijzen+1; j++) 
			{
				if (j==AantalBewijzen)
				{
					TestSet[i, j]= Random.Range(0, AantalUitkomsten);
				}
				else
				{
					TestSet [i, j] = Random.Range (0, 2);
				}

			}
		}






	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// alle producten defineren
	void MatrixProd(double[,] A, double[,] B){

		if (A.GetLength(1)==B.GetLength(0))
		{
			double[,] result = new double[A.GetLength (0), B.GetLength (1)];
			for (int i =0;i<A.GetLength(0);i++)
			{
				for (int j = 0; j<B.GetLength(1);j++)
				{
					for (int k=0;k<A.GetLength(1);k++)
					{
						result[i,j] += A[i,k]*B[j,k];
					}
				}
			}
			return result;
		}
		return null;
	}

	void VectorMatrix(double[] A, double[,] B){
		if (A.GetLength(1)==B.GetLength(0))
		{
			int[] result = new int[A.GetLength(1)];
			for (int i=0; i<B.GetLength(1);i++)
			{
				for (int j=0;j<A.GetLength(1);j++)
				{
					result[i]+= A[j]*B[j,i];
				}
			}
			return result;
		}
		return null;
	}

	void VectorProd(double[] A, double[] B){
		if (A.GetLength==B.GetLength)
		{
			int result= new int;
			for (int i =0;i<A.GetLength;i++)
			{
				result += A[i]*B[i];
			}
			return result;

		}
		return null;
	}

	void UpdateWeigths(){

		double[] input = TestSet[i,:]
	}

}


	
