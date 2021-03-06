﻿using UnityEngine;
using System.Collections;

public enum Seasons{ Automn, Spring, Summer, Winter };


public class MapManager : MonoBehaviour {
	public static Sprite[] backgroundMapList;
	public static GameObject[] objectLayers;
	public GameObject backgroundMap;
	public int currentMonth;
	public Seasons currentSeason;
	
	void Start(){
		backgroundMapList = new Sprite[4];
		backgroundMapList[(int)Seasons.Automn] = Resources.Load<Sprite>("automnBck");
		backgroundMapList[(int)Seasons.Spring] = Resources.Load<Sprite>("springBck");
		backgroundMapList[(int)Seasons.Summer] = Resources.Load<Sprite>("springBck");
		backgroundMapList[(int)Seasons.Winter] = Resources.Load<Sprite>("winterBck");
		
		objectLayers = new GameObject[4];
		objectLayers[(int)Seasons.Automn] = (GameObject)Object.Instantiate(Resources.Load<Object>("automn"), new Vector3(-34.61f, 22.69f, 0.0f), Quaternion.identity);
		objectLayers[(int)Seasons.Automn].SetActive(false);
		objectLayers[(int)Seasons.Spring] = (GameObject)Object.Instantiate(Resources.Load<Object>("spring"), new Vector3(-34.61f, 22.69f, 0.0f), Quaternion.identity);
		objectLayers[(int)Seasons.Spring].SetActive(false);
		objectLayers[(int)Seasons.Summer] = (GameObject)Object.Instantiate(Resources.Load<Object>("spring"), new Vector3(-34.61f, 22.69f, 0.0f), Quaternion.identity);
		objectLayers[(int)Seasons.Summer].SetActive(false);
		objectLayers[(int)Seasons.Winter] = (GameObject)Object.Instantiate(Resources.Load<Object>("winter"), new Vector3(-34.61f, 22.69f, 0.0f), Quaternion.identity);
		objectLayers[(int)Seasons.Winter].SetActive(false);

		backgroundMap = new GameObject("backgroundMap");
		Vector3 pos = new Vector3();
		pos.x = -7.93672f;
		pos.y = -3.97f;
		backgroundMap.transform.position = pos;
		backgroundMap.AddComponent<SpriteRenderer>();
		backgroundMap.GetComponent<SpriteRenderer>().sprite = backgroundMapList[(int)Seasons.Spring];
		backgroundMap.GetComponent<SpriteRenderer>().material = new Material(Shader.Find("Sprites/Diffuse"));

		currentMonth = (int)Months.Mars;
		updateSeason();
	}
	void Update(){ }
	public void pleuvoir(){ Map.globalEvent(EventType.rain); }
	public void neiger(){ Map.globalEvent(EventType.snow); }
	public void beginDay(){ Map.globalBeginDay(); }
	public void endDay(){ Map.globalEndDay(); }
	public void setMonth(int numMonth){
		Map.setMonth(numMonth);
		currentMonth = numMonth;
		updateSeason();
	}
	public void updateSeason(){
		Seasons newSeason;
		if( currentMonth >= (int)Months.Avril && currentMonth <= (int)Months.Juin ){ newSeason = Seasons.Spring; }
		else if( currentMonth >= (int)Months.Juillet && currentMonth <= (int)Months.Septembre ){ newSeason = Seasons.Summer; }
		else if( currentMonth >= (int)Months.Octobre && currentMonth <= (int)Months.Decembre ){ newSeason = Seasons.Automn; }
		else{ newSeason = Seasons.Winter; }

		if( newSeason != currentSeason ){
			objectLayers[(int)currentSeason].SetActive(false);
			currentSeason = newSeason;
			updateBackgroundMap();
			updateObjectLayer();
		}
	}
	public void updateBackgroundMap(){ backgroundMap.GetComponent<SpriteRenderer>().sprite = backgroundMapList[ (int)currentSeason ]; }
	public void updateObjectLayer(){ objectLayers[(int)currentSeason].SetActive(true); }
}
