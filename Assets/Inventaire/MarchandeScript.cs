using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MarchandeScript : MonoBehaviour {

    Text dialogue;
    int numMois;
    public GameObject panel;

	// Use this for initialization
	void Start () {
        numMois = Map.currentMonth;
        dialogue = panel.transform.GetChild(0).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        numMois = Map.currentMonth;
    }


    /**
     * fonction qui gere l'apparition de la marchande lors de la vente de legume en fonction de la qualité du legume vendu.
     * */ 

    public void dialogueQualite (Item item)
    {
        FM_SonScript.sonQualite(item.itemPower);
        if (item.itemPower / 25 >= 3)
        {
            panel.SetActive(true);
            dialogue.text = "Felicitation, vous avez fait un legume d'une super qualité ";
        }
        if (item.itemPower / 25 == 2)
        {
            panel.SetActive(true);
            dialogue.text = "Un legume de bonne qualité mais avec un meilleur arosage votre produit aurai été mieux";
        }
        if (item.itemPower / 25 == 1)
        {
            panel.SetActive(true);
            dialogue.text = "Legume de basse qualité, auriez vous oubliez de l'arroser?";
        }
        if (item.itemPower / 25 == 0)
        {
            panel.SetActive(true);
            dialogue.text = "Vous avez completement oublié votre legume, il foit etre arroser tous les jours ";
        }
    }

    /**
     *  fonction qui gere l'apparition de la marchande lors de l'achat de graine an fonction de si c'est la periode de planter ce legume.
     * */

    public bool LegumeDeSaison(Item item){
        if (item.itemName == "aubergine") {
            if (numMois < 1)
            {
                FM_SonScript.marchande();
                dialogue.text = "C'est trop tot pour planter des aubergines, il serait mieux de les planter plus tard";
                panel.SetActive(true);
                return false;
                 }
            else if (numMois > 3)
            {
                FM_SonScript.marchande();
                panel.SetActive(true);
                dialogue.text = "Il est trop tard pour planter des aubergines, attendre la bonne saison l'année prochaine serait plus judicieux";
                return false;
            }
        }
        if (item.itemName == "ble")
        {
            if (numMois < 8)
            {
                dialogue.text = "C'est trop tot pour planter du ble, il serait mieux de les planter plus tard";
                panel.SetActive(true);
                FM_SonScript.marchande();
                return false;
                 }
            else if (numMois > 10)
            {
                dialogue.text = "Il est trop tard pour planter du ble, attendre la bonne saison l'année prochaine serait plus judicieux";

                panel.SetActive(true);
                return false;
                    }
        }
        if (item.itemName == "oignon")
        {
            if (numMois < 2)
            {
                dialogue.text = "C'est trop tot pour planter des oignons, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
                  }
            else if (numMois > 3)
            {
                dialogue.text = "Il est trop tard pour planter des oignons, attendre la bonne saison l'année prochaine serait plus judicieux";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
           }
        }
        if (item.itemName == "carotte")
        {
            if (numMois < 1)
            {
                dialogue.text = "C'est trop tot pour planter des carottes, il serait mieux de les planter plus tard";

                panel.SetActive(true);
                return false;
                 }
            else if (numMois > 8)
            {
                dialogue.text = "Il est trop tard pour planter des carottes, attendre la bonne saison l'année prochaine serait plus judicieux";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
                 }
        }
        if (item.itemName == "chou-fleur")
        {
            if (numMois < 2)
            {
                dialogue.text = "C'est trop tot pour planter des chou-fleur, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
                 }
            else if (numMois > 5)
            {
                dialogue.text = "Il est trop tard pour planter des chou-fleur, attendre la bonne saison l'année prochaine serait plus judicieux";

                panel.SetActive(true);
                return false;
                 }
        }
        if (item.itemName == "citrouille")
        {
            if (numMois < 3)
            {
                dialogue.text = "C'est trop tot pour planter des citrouilles, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
                }
            else if (numMois > 5)
            {
                dialogue.text = "Il est trop tard pour planter des citrouilles, attendre la bonne saison l'année prochaine serait plus judicieux";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
        }
        if (item.itemName == "concombre")
        {
            if (numMois < 2)
            {
                dialogue.text = "C'est trop tot pour planter des concombres, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
            else if (numMois > 6)
            {
                dialogue.text = "Il est trop tard pour planter des concombres, attendre la bonne saison l'année prochaine serait plus judicieux";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
        }
        if (item.itemName == "mais")
        {
            if (numMois < 3)
            {
                dialogue.text = "C'est trop tot pour planter des mais, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
            else if (numMois > 7)
            {
                dialogue.text = "Il est trop tard pour planter des mais, attendre la bonne saison l'année prochaine serait plus judicieux";

                panel.SetActive(true);
                return false;
            }
        }
        if (item.itemName == "navet")
        {
            if (numMois < 2)
            {
                dialogue.text = "C'est trop tot pour planter des navets, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
            else if (numMois > 7)
            {
                dialogue.text = "Il est trop tard pour planter des navets, attendre la bonne saison l'année prochaine serait plus judicieux";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
        }
        if (item.itemName == "patate")
        {
            if (numMois < 1)
            {
                dialogue.text = "C'est trop tot pour planter des patates, il serait mieux de les planter plus tard";

                panel.SetActive(true);
                return false;
            }
            else if (numMois > 3)
            {
                dialogue.text = "Il est trop tard pour planter des patates, attendre la bonne saison l'année prochaine serait plus judicieux";
           
                panel.SetActive(true);
                return false;
             }
        }
        if (item.itemName == "poivron")
        {
            if (numMois < 1)
            {
                dialogue.text = "C'est trop tot pour planter des poivrons, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
            else if (numMois > 4)
            {
                dialogue.text = "Il est trop tard pour planter des poivrons, attendre la bonne saison l'année prochaine serait plus judicieux";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
        }
        if (item.itemName == "tomate")
        {
            if (numMois < 1)
            {
                dialogue.text = "C'est trop tot pour planter des tomates, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
            else if (numMois > 3)
            {
                dialogue.text = "Il est trop tard pour planter des tomates, attendre la bonne saison l'année prochaine serait plus judicieux";

                panel.SetActive(true);
                return false;
            }
        }
        if (item.itemName == "salade")
        {
            if (numMois < 1)
            {
                dialogue.text = "C'est trop tot pour planter des salades, il serait mieux de les planter plus tard";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
            else if (numMois > 5)
            {
                dialogue.text = "Il est trop tard pour planter des salades, attendre la bonne saison l'année prochaine serait plus judicieux";
                FM_SonScript.marchande();
                panel.SetActive(true);
                return false;
            }
        }
        return true;
    }
}
