
using Godot.Collections;
using Godot;
public class Utils
{
	public static string ReplacePlayerStrings(string str, ICharacter data)
	{
		str = str.Replace("<character>", data.CharacterName);
		str = str.Replace("<name>", data.CharacterName);
		str = str.Replace("<selves>", data.Selves);
		str = str.Replace("<they>", data.They);
		str = str.Replace("<them>", data.Them);
		str = str.Replace("<theirs>", data.Theirs);
		str = str.Replace("<their>", data.Their);
		return str;
	}
	public static string ReplaceOtherPlayerStrings(string str, ICharacter data)
	{
		str = str.Replace("<ocharacter>", data.CharacterName);
		str = str.Replace("<oname>", data.CharacterName);
		str = str.Replace("<oselves>", data.Selves);
		str = str.Replace("<othey>", data.They);
		str = str.Replace("<othem>", data.Them);
		str = str.Replace("<otheirs>", data.Theirs);
		str = str.Replace("<otheir>", data.Their);
		return str;
	}
	public static string ReplaceDamageStrings(string str, Effect data)
	{
		int dmg = data.EffectValue + data.EffectModifier;
		str = str.Replace("<value>", dmg.ToString());
		str = str.Replace("<ename>", data.EffectName);
		str = str.Replace("<etype>", data.EffectType);
		str = str.Replace("<eduration>", data.EffectDuration.ToString());
		return str;
	}

	public static void SavePartyData(Array<CharacterData> partyData)
	{
		// Save party data to a file or database
		DirAccess.MakeDirRecursiveAbsolute(ProjectSettings.GlobalizePath("user://SaveData/Party"));
		foreach (var member in partyData)
		{
			ResourceSaver.Save(member, "user://SaveData/Party/" + member.CharacterName + ".tres");
		}
	}

	public static Array<CharacterData> LoadPartyData()
	{
		Array<CharacterData> partyData = new Array<CharacterData>();
		DirAccess dir = DirAccess.Open("user://SaveData/Party");
		if (dir == null)
		{
			//GD.Print("No party data found.");
			return partyData;
		}
		dir.ListDirBegin();
		string fileName = dir.GetNext();
		while (fileName != "")
		{
			if (fileName.EndsWith(".tres"))
			{
				CharacterData player = ResourceLoader.Load<CharacterData>("user://SaveData/Party/" + fileName);
				if (player != null)
				{
					partyData.Add(player);
				}
			}
			fileName = dir.GetNext();
		}
		dir.ListDirEnd();
		return partyData;
	}
}