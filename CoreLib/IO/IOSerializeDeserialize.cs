using System;
using System.Collections.Generic;
using System.IO;
using CoreLib.Models;
using Newtonsoft.Json;
namespace CoreLib.IO
{
    public class IOSerializeDeserialize
    {
        public static MainResult Serialize(List<ShutdownModel> list)
        {
            if (list == null)
                return new MainResult(false, "Parameter `list` cannot be null");
            bool success = true;
            string error = "";
            try
            {
                string path = Global.GetDataFileLocation();
                if(!String.IsNullOrEmpty(path))
                {
                    if (list.Count == 0)
                        File.WriteAllText(path, null);
                    else
                    {
                        string json = JsonConvert.SerializeObject(list);
                        File.WriteAllText(path, json);
                    }
                }
                else
                {
                    success = false;
                    error = "Error while writing serialized data to desired location";
                }
            }
            catch(Exception ex)
            {
                success = false;
                error = $"Error: `{ex.Message}`";
            }
            return new MainResult(success, error);
        }
        public static DataResult<List<ShutdownModel>> Deserialize()
        {
            bool success = true;
            string error = "";
            List<ShutdownModel> list = new List<ShutdownModel>();
            try
            {
                string path = Global.GetDataFileLocation();
                if (!String.IsNullOrEmpty(path))
                {
                    if(File.Exists(path))
                    {

                        string json = File.ReadAllText(path);
                        list = JsonConvert.DeserializeObject<List<ShutdownModel>>(json);
                    }
                    else
                    {
                        list = new List<ShutdownModel>();
                    }
                }
                else
                {
                    list = null;
                    success = false;
                    error = "Error while reading serialized data from desired location";
                }

            }
            catch (Exception ex)
            {
                list = null;
                success = false;
                error = $"Error: `{ex.Message}`";
            }

            return new DataResult<List<ShutdownModel>>(list,new MainResult(success, error));
        }
    }
}
