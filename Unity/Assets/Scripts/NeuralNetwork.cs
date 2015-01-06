using UnityEngine;
using System.Collections;

public class NeuralNetwork : MonoBehaviour {

	public int AantalBewijzen;
	public int AantalUitkomsten; //twee denk ik

	System.Random random = new System.Random();
	// Use this for initialization
	void Start () {
	
		// Maak het neural network
		double[,] Weigths = new double[AantalBewijzen, AantalUitkomsten];
		// Insert random weigths
		for (int i = 0; i < AantalBewijzen; i++) 
		{
			for (int j = 0; j < AantalUitkomsten; j++) 
			{
				double blabla = Random.Range (0,100);
				blabla = blabla / 100;
				Weigths [i, j] = (blabla * AantalBewijzen) - (AantalBewijzen / 2);
			}
		}

		//train the weigths
		// first we make a random trainingset
		double[,] TestSet = new double[50, AantalBewijzen + 1];
		for (int i=0;i<50;++i)
			for (int j=0;j<AantalBewijzen+1;++j)
				TestSet[i, j] = random.NextDouble();

	}
	
	// Update is called once per frame
	void Update () {
	
	}






}



