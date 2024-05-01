using System.Collections.Generic;
using Vintagestory.API.Common;

namespace ChatPing;

public static class ModConfig
{
    private const string ConfigName = "ChatPingConfig.json";

    public static Config ReadConfig(ICoreAPI api)
    {
        Config config;

        try
        {
            config = LoadConfig(api);

            if (config == null)
            {
                GenerateConfig(api);
                config = LoadConfig(api);
            }
        }
        catch
        {
            GenerateConfig(api);
            config = LoadConfig(api);
        }

        return config;
    }


    private static Config LoadConfig(ICoreAPI api)
    {
        return api.LoadModConfig<Config>(ConfigName);
    }

    private static void GenerateConfig(ICoreAPI api)
    {
        api.StoreModConfig(new Config(), ConfigName);
    }

}

public class Config
{
    public HashSet<string> ChannelsToPingFor;
    public float Volume;

    public Config()
    {
        FillDefault();
    }

    private void FillDefault()
    {
        ChannelsToPingFor = new HashSet<string>{"Proximity"};
        Volume = 0.5f;
    }
}