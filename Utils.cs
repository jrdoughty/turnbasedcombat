public class Utils
{
    public static string ReplacePlayerStrings(string str, ICharacter data)
    {
		str = str.Replace("<character>", data.name);
		str = str.Replace("<name>", data.name);
		str = str.Replace("<selves>", data.selves);
		str = str.Replace("<they>", data.they);
		str = str.Replace("<them>", data.them);
		str = str.Replace("<theirs>", data.theirs);
		str = str.Replace("<their>", data.their);
        return str;
    }
    public static string ReplaceOtherPlayerStrings(string str, ICharacter data)
    {
		str = str.Replace("<ocharacter>", data.name);
		str = str.Replace("<oname>", data.name);
		str = str.Replace("<oselves>", data.selves);
		str = str.Replace("<othey>", data.they);
		str = str.Replace("<othem>", data.them);
		str = str.Replace("<otheirs>", data.theirs);
		str = str.Replace("<otheir>", data.their);
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
}