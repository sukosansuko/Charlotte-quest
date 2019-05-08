using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class playerSkill_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/ExcelData/playerSkill.xls";
	private static readonly string exportPath = "Assets/Resources/ExcelData/playerSkill.asset";
	private static readonly string[] sheetNames = { "playerSkill", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Player_skillList data = (Player_skillList)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Player_skillList));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Player_skillList> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Player_skillList.Sheet s = new Player_skillList.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Player_skillList.Param p = new Player_skillList.Param ();
						
					cell = row.GetCell(0); p.ID = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.charName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.skillName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.Lv = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.power = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.sp = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.Time = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(7); p.effect = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(8); p.etc = (cell == null ? "" : cell.StringCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
