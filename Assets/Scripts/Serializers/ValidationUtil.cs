using UnityEngine;

public class ValidationUtil
{
    public static float ParseFloat(string stringValue, string name="", float defaultValue = 0f)
    {
        if (float.TryParse(stringValue, out float result))
        {
            return result;
        }
        else
        {
            Debug.Log($"Invalid float. name: {name}, value: {stringValue}");
            return defaultValue;
        }
    }
    
    public static int ParseInt(string stringValue, string name="", int defaultValue = 0)
    {
        if (int.TryParse(stringValue, out int result))
        {
            return result;
        }
        else
        {
            Debug.Log($"Invalid int. name: {name}, value: {stringValue}");
            return defaultValue;
        }
    }
    
    public static Color ParseColor(string stringValue, string name="", Color defaultValue = default(Color))
    {
        var val = stringValue.StartsWith("#") ? stringValue : "#" + stringValue;
        if(UnityEngine.ColorUtility.TryParseHtmlString(val, out Color result))
            return result;
         
        Debug.Log($"Invalid color. name: {name}, value: {stringValue}");
        return Color.red;
    }
}