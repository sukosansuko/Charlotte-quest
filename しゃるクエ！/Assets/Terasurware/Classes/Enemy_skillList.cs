using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_skillList : ScriptableObject
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
		public string skillName;
		public double power;
		public string target;
		public double sp;
		public string time;
		public string referenceStatus;
	}
}

