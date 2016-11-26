using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

	void Start(){ }
	void Update(){ }
	public void pleuvoir(){ Map.globalEvent(EventType.rain); }
	public void neiger(){ Map.globalEvent(EventType.snow); }
	public void beginDay(){ Map.globalBeginDay(); }
	public void endDay(){ Map.globalEndDay(); }
	public void setMonth(int numMonth){ Map.setMonth(numMonth); }
}
