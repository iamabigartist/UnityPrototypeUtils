﻿using System.IO;
using System.Runtime.Serialization.Json;
namespace PrototypeUtils
{
    public static class MemoryUtility
    {
        public static long GetObjectByteSize<T>(this T t) where T : class
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer( typeof(T) );
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.WriteObject( stream, t );
                return stream.Length;
            }
        }
    }
}