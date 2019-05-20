using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerGrow : ScriptableObject
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
		public string Name;
		public double GROW_HP;
		public double GROW_SP;
		public double GROW_ATK;
		public double GROW_DEF;
		public double GROW_SPD;
		public double GROW_MAT;
		public double GROW_MDF;
		public double GROW_LUK;
	}
}

