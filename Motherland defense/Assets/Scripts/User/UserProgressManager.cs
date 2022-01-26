using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserProgressManager
{
    public static string Path { get; } = "UserData.bin";

    public static UserData LoadUserData(string path)
    {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        try
        {
            return (UserData)new BinaryFormatter().Deserialize(stream);
        }
        catch (System.Exception)
        {
            return null;
        }
        finally
        {
            stream.Close();
        }
    }

    public static void SaveUserData(string path, UserData data)
    {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, data);
        stream.Close();
        Debug.Log("data saved");
    }
}
