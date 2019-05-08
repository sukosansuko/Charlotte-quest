using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_skillList : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public double ID;
		public string charName;
		public string skillName;
		public double Lv;
		public double power;
		public double sp;
		public string Time;
		public string effect;
		public string etc;
	}
}

