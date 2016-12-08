using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine.UI;
using System.Linq;

public class DialogBubble : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	public GameObject vCurrentBubble = null; //just to make sure we cannot open multiple bubble at the same time.
	public bool IsTalking = false;
	public int numeroMois = 0;
	public List<PixelBubble> vBubble = new List<PixelBubble>();
	private PixelBubble vActiveBubble = null;

	//show the right bubble on the current character
	void ShowBubble(DialogBubble vcharacter)
	{
		bool gotonextbubble = false;

		//if vcurrentbubble is still there, just close it
		if (vActiveBubble != null) {
			if (vActiveBubble.vClickToCloseBubble) {
				//get the function to close bubble
				Appear vAppear = vcharacter.vCurrentBubble.GetComponent<Appear> ();
				vAppear.valpha = 0f;
				vAppear.vTimer = 0f; //instantly
				vAppear.vchoice = false; //close bubble
				
				//check if last bubble
				if (vActiveBubble == vcharacter.vBubble.Last ())
					vcharacter.IsTalking = false;
			}
		}
		
		foreach (PixelBubble vBubble in vcharacter.vBubble)
		{
			//make sure the bubble isn't already opened
			if (vcharacter.vCurrentBubble == null)
			{
				//make the character in talking status
				vcharacter.IsTalking = true;
				
				//cut the message into 24 characters
				string vTrueMessage = "";
				string cLine = "";
				int vLimit = 24;
				if (vBubble.vMessageForm == BubbleType.Round)
					vLimit = 16;
				
				vBubble.vMessage = getRandomDialogByMounth(numeroMois);
				//cut each word in a text in 24 characters.
				foreach (string vWord in vBubble.vMessage.Split(' '))
				{
					if (cLine.Length + vWord.Length > vLimit)
					{
						vTrueMessage += cLine+System.Environment.NewLine;  
						
						//add a line break after
						cLine = ""; //then reset the current line
					}
					
					//add the current word with a space
					cLine += vWord+" ";
				}
				
				//add the last word
				vTrueMessage += cLine;
				GameObject vBubbleObject = null;
				
				//create a rectangle or round bubble
				if (vBubble.vMessageForm == BubbleType.Rectangle)
				{
					//create bubble
					vBubbleObject = Instantiate(Resources.Load<GameObject> ("Customs/BubbleRectangle"));
					vBubbleObject.transform.position = vcharacter.transform.position + new Vector3(1.35f, 1.9f, 0f); //move a little bit the teleport particle effect
				}
				else 
				{
					//create bubble
					vBubbleObject = Instantiate(Resources.Load<GameObject> ("Customs/BubbleRound"));
					vBubbleObject.transform.position = vcharacter.transform.position + new Vector3(0.15f, 1.75f, 0f); //move a little bit the teleport particle effect
				}

				//show the mouse and wait for the user to left click OR NOT (if not, after 10 sec, it disappear)
				vBubbleObject.GetComponent<Appear>().needtoclick = vBubble.vClickToCloseBubble;
				
				Color vNewBodyColor = new Color(vBubble.vBodyColor.r, vBubble.vBodyColor.g, vBubble.vBodyColor.b, 0f);
				Color vNewBorderColor = new Color(vBubble.vBorderColor.r, vBubble.vBorderColor.g, vBubble.vBorderColor.b, 0f);
				Color vNewFontColor = new Color(vBubble.vFontColor.r, vBubble.vFontColor.g, vBubble.vFontColor.b, 255f);
				
				//get all image below the main Object
				foreach (Transform child in vBubbleObject.transform)
				{
					SpriteRenderer vRenderer = child.GetComponent<SpriteRenderer> ();
					TextMesh vTextMesh = child.GetComponent<TextMesh> ();
					
					if (vRenderer != null && child.name.Contains("Body"))
					{
						//change the body color
						vRenderer.color = vNewBodyColor;
						
						if (vRenderer.sortingOrder < 10)
							vRenderer.sortingOrder = 1500;
					}
					else if (vRenderer != null && child.name.Contains("Border"))
					{
						//change the border color
						vRenderer.color = vNewBorderColor;
						if (vRenderer.sortingOrder < 10)
							vRenderer.sortingOrder = 1501;
					} 
					else if (vTextMesh != null && child.name.Contains("Message"))
					{
						//change the message and show it in front of everything
						vTextMesh.color = vNewFontColor;
						vTextMesh.text = vTrueMessage;
						child.GetComponent<MeshRenderer>().sortingOrder = 1550;
						
						Transform vMouseIcon = child.FindChild("MouseIcon");
						if (vMouseIcon != null && !vBubble.vClickToCloseBubble)
							vMouseIcon.gameObject.SetActive(false);
					}
					
					//disable the mouse icon because it will close by itself
					if (child.name == "MouseIcon" && !vBubble.vClickToCloseBubble)
						child.gameObject.SetActive(false);
					else
						vActiveBubble =  vBubble; //keep the active bubble and wait for the Left Click
				}
				
				vcharacter.vCurrentBubble = vBubbleObject; //attach it to the player
				vBubbleObject.transform.parent = vcharacter.transform; //make him his parent
			} else if (vActiveBubble == vBubble && vActiveBubble.vClickToCloseBubble)
			{
				gotonextbubble = true;
				vcharacter.vCurrentBubble = null;
			}
		}
	}	

	void Update () 
	{
		//check if we have the mouse over the character
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	
		//make sure we left click and is on a NPC
		if (Physics.Raycast (ray, out hit) && Input.GetMouseButtonDown (0)) {
			//only return NPC
			if (hit.transform == this.transform) {
				//check the bubble on the character and make it appear!
				if (vBubble.Count > 0) {
					ShowBubble (hit.transform.GetComponent<DialogBubble> ());
				}
			}
		}

		//can't have a current character 
		if (!IsTalking)
		{			
			vActiveBubble = null;
		}
	}


	public string getRandomDialogByMounth(int mois) {

		switch (mois) {
			case 0:
			List<string> janvierDialogue = new List<string>();
			janvierDialogue.Add("Si vous avez plantés des carottes, Janvier c'est le dernier mois pour passer aux récoltes !");
			janvierDialogue.Add("C'est bientôt la saison des aubergines, pensez à acheter vos graines !");
			janvierDialogue.Add("Le mois prochain, c'est le moment idéal pour semer des carottes, pensez à acheter vos graines !");
			janvierDialogue.Add("Ma base de données indique que c'est le dernier moment pour récolter des choux. Nous en avons semés ?");
			janvierDialogue.Add("On est bientôt en Février, on va pouvoir cultiver des patates, bip !");
			janvierDialogue.Add("Mon module de croissance des plantes m'indique que c'est le moment idéal pour semer des graines de tomates.");
			

			janvierDialogue.Add("Bip bip ...");
			return janvierDialogue[Random.Range(0, janvierDialogue.Count - 1)];
			
			break;

			case 1:
			List<string> fevrierDialogue = new List<string>();
			fevrierDialogue.Add("C'est la saison des semailles pour les aubergines, pensez à acheter vos graines !");
			fevrierDialogue.Add("C'est le moment idéal pour semer des carottes, pensez à acheter vos graines !");
			fevrierDialogue.Add("On est en Février, on peut désormais cultiver des patates, bip !");
			fevrierDialogue.Add("Mon module de croissance des plantes m'indique que c'est le moment idéal pour semer des graines de tomates, et de poivrons.");
			fevrierDialogue.Add("Le mois prochain nous pourrons semer du choux fleur.");
			fevrierDialogue.Add("Le mois prochain nous pourrons semer des navets, et des oignons.");
			

			fevrierDialogue.Add("Bip bip ...");
			return fevrierDialogue[Random.Range(0, fevrierDialogue.Count - 1)];
			
			break;
			
			case 2:

			List<string> marsDialogue = new List<string>();
			marsDialogue.Add("Ce mois ci, nous pouvons continuer à semer : des aubergines, des carottes, des patates, et des poivrons !");
			marsDialogue.Add("On est en Mars, on peut désormais cultiver des choux fleur, bip !");
			marsDialogue.Add("Mon module de croissance des plantes m'indique que c'est le moment idéal pour semer des graines de concombres");
			marsDialogue.Add("De Mars à Avril, c'est la saison idéale pour semer des oignons !");
			marsDialogue.Add("Le mois prochain nous pourrons semer du mais.");
			

			marsDialogue.Add("Bip bip ...");
			return marsDialogue[Random.Range(0, marsDialogue.Count - 1)];
			break;
			
			case 3:
			List<string> avrilDialogue = new List<string>();
			avrilDialogue.Add("Ce mois ci, nous pouvons continuer à semer : des aubergines, des carottes, des patates, des poivrons, des oignons et des choux fleur!");
			avrilDialogue.Add("On est en avril, on peut désormais cultiver des choux fleur, bip !");
			avrilDialogue.Add("Mon module de croissance des plantes m'indique que c'est le moment idéal pour semer des graines de citrouille");
			avrilDialogue.Add("De Mars à Avril, c'est la saison idéale pour semer des oignons !");
			avrilDialogue.Add("D'Avril à Juin, c'est la saison idéale pour semer du mais !");
			avrilDialogue.Add("De Mars à Août, c'est la saison idéale pour semer des navets, pensez à achetez vos graines !");
			
			avrilDialogue.Add("Avril, c'est le dernier mois pour semer des aubergines. Semez-vite si ce n'est pas encore fait !");
			

			avrilDialogue.Add("Bip bip ...");
			return avrilDialogue[Random.Range(0, avrilDialogue.Count - 1)];
			break;
			
			case 4:
				List<string> maiDialogue = new List<string>();
			maiDialogue.Add("Il y a tellement de variétés de choux, qu'on peut en semer de Septembre à Juin.");
			maiDialogue.Add("De Mars à Juin, c'est le bon timing pour semer des choux fleur, bip !");
			maiDialogue.Add("La citrouille ? Ca se sème d'Avril à Juin, et se récolte entre Août et Octobre. Pile Poil pour Halloween");
			maiDialogue.Add("Bip bip, d'après des les forums de jardiniers, le comcombre se sème de Mars jusqu'à début Juillet et peut être récolté jusqu'en Novembre.");
			maiDialogue.Add("D'Avril à Juin, c'est la saison idéale pour semer du mais !");
			maiDialogue.Add("De Mars à Août, c'est la saison idéale pour semer des navets !");
			
			maiDialogue.Add("Avril, c'est le dernier mois pour semer des aubergines. Semez-vite si ce n'est pas encore fait !");
			maiDialogue.Add("Si vous avez semé des patates dans les temps, elles ne devraient plus tarder à être prêtes !");
			maiDialogue.Add("C'est le dernier mois pour semer des poivrons, c'est maintenant ou jamais !");
			

			maiDialogue.Add("Bip bip ...");
			return maiDialogue[Random.Range(0, maiDialogue.Count - 1)];
			
			break;
			
			case 5:
					List<string> juinDialogue = new List<string>();
			juinDialogue.Add("Il y a tellement de variétés de choux, qu'on peut en semer de Septembre à Juin.");
			juinDialogue.Add("De Mars à Juin, c'est le bon timing pour semer des choux fleur, bip !");
			juinDialogue.Add("La citrouille ? Ca se sème d'Avril à Juin, et se récolte entre Août et Octobre. Pile Poil pour Halloween");
			juinDialogue.Add("Bip bip, d'après des les forums de jardiniers, le comcombre se sème de Mars jusqu'à début Juillet et peut être récolté jusqu'en Novembre.");
			juinDialogue.Add("D'Avril à Juin, c'est la saison idéale pour semer du mais !");
			juinDialogue.Add("De Mars à Août, c'est la saison idéale pour semer des navets !");
			
			juinDialogue.Add("Avril, c'est le dernier mois pour semer des aubergines. Semez-vite si ce n'est pas encore fait !");
			juinDialogue.Add("Si vous avez semé des patates dans les temps, elles ne devraient plus tarder à être prêtes !");
			juinDialogue.Add("C'est le dernier mois pour semer des poivrons, c'est maintenant ou jamais !");
			

			juinDialogue.Add("Bip bip ...");
			return juinDialogue[Random.Range(0, juinDialogue.Count - 1)];
			break;
			
			case 6:
			break;
			
			case 7:
			break;
			
			case 8:
			break;
			
			case 9:
			break;
			
			case 10:
			break;

			case 11:
			break;
		}

	return "echec";
	}
}
