using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace ETool
{
    [CreateAssetMenu(menuName = "ETool/GameData")]
    public class EGameData : ScriptableObject
    {
        public GameDataStruct gameDataStruct = new GameDataStruct();

        public void Save_Json(string path)
        {
            File.WriteAllText(path, JsonUtility.ToJson(gameDataStruct, true));
        }

        public void Save_Json_Binary(string path)
        {
            File.WriteAllBytes(path + ".buffer", Zip(JsonUtility.ToJson(gameDataStruct, true)));
            EncryptFile(path + ".buffer", path);
            File.Delete(path + ".buffer");
        }

        public void Load_Json(string path)
        {
            gameDataStruct = JsonUtility.FromJson<GameDataStruct>(File.ReadAllText(path));
        }

        public void Load_Json_Binary(string path)
        {
            DecryptFile(path, path + ".buffer");
            string str = Unzip(File.ReadAllBytes(path + ".buffer"));
            gameDataStruct = JsonUtility.FromJson<GameDataStruct>(str);
            File.Delete(path + ".buffer");
        }

        public void SetData(string category, string element, object o, FieldType type)
        {
            GameDataCategory c = GetCate(category, gameDataStruct.gameDataCategories);
            if (c == null)
            {
                Debug.LogWarning("Cannot data category find: " + category);
                return;
            }
            BlueprintVariable g = GetElement(element, c.gameDataElements);
            if (g == null)
            {
                Debug.LogWarning("Cannot data element find: " + element);
                return;
            }
            Field.SetObjectByFieldType(type, g.variable, o);
        }

        public object GetData(string category, string element, FieldType type)
        {
            GameDataCategory c = GetCate(category, gameDataStruct.gameDataCategories);
            if (c == null)
            {
                Debug.LogWarning("Cannot data category find: " + category);
                return null;
            }
            BlueprintVariable g = GetElement(element, c.gameDataElements);
            if (g == null)
            {
                Debug.LogWarning("Cannot data element find: " + element);
                return null;
            }
            return Field.GetObjectByFieldType(type, g.variable);
        }

        private GameDataCategory GetCate(string label, List<GameDataCategory> dataCategories)
        {
            foreach(var i in dataCategories)
            {
                if (i.label == label) return i;
            }
            return null;
        }

        private BlueprintVariable GetElement(string label, List<BlueprintVariable> GameDataElement)
        {
            foreach (var i in GameDataElement)
            {
                if (i.label == label) return i;
            }
            return null;
        }

        private void EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                Debug.LogError("Encryption failed!");
            }
        }

        private void DecryptFile(string inputFile, string outputFile)
        {

            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();

            }
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
    }
}
