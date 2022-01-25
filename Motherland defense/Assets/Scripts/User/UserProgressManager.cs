using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserProgressManager
{
    public static UserData LoadUserData(string path)
    {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        return (UserData)new BinaryFormatter().Deserialize(stream);
    }

    public static void SaveUserData(string path, UserData data)
    {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        new BinaryFormatter().Serialize(stream, data);
    }
}
