public class Utils
{
    public static string ReplacePlayerStrings(string str, ICharacter data)
    {
		str = str.Replace("<character>", data.name);
		str = str.Replace("<selves>", data.selves);
		str = str.Replace("<they>", data.they);
		str = str.Replace("<them>", data.them);
		str = str.Replace("<theirs>", data.theirs);
		str = str.Replace("<their>", data.their);
        return str;
    }
}