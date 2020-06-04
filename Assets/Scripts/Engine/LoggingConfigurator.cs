using System.IO;
using log4net.Config;
using UnityEngine;

namespace VoxelValley.Engine
{
    public class LoggingConfigurator
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Configure()
        {
            log4net.GlobalContext.Properties["LogFileName"] = $"{Application.dataPath}/VoxelValley.log";
            XmlConfigurator.Configure(new FileInfo($"{Application.streamingAssetsPath}/Config/log4net.xml"));
        }
    }
}