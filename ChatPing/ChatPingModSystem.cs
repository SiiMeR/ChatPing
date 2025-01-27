using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace ChatPing
{
    public class ChatPingModSystem : ModSystem
    {


        public override void StartClientSide(ICoreClientAPI api)
        {
            api.Event.IsPlayerReady += (ref EnumHandling _) =>
            {
                var config = ModConfig.ReadConfig(api);



                api.Event.ChatMessage += (groupId, message, chattype, data) =>
                {
                    if (chattype != EnumChatType.OthersMessage)
                    {
                        return;
                    }

                    if (config.PingForGeneralChat && groupId == 0)
                    {
                        api.World.PlaySoundAt(new AssetLocation("sounds/player/projectilehit"), api.World.Player.Entity,
                            volume: config.Volume, randomizePitch: false);
                        return;
                    }
                    
                    var playerGroups = api.World.Player.Groups;
                    var matchingGroups = playerGroups.Where((group) => config.ChannelsToPingFor.Contains(group.GroupName))
                        .Select(group => group.GroupUid);

                    if (!matchingGroups.Contains(groupId))
                    {
                        return;
                    }

                    api.World.PlaySoundAt(new AssetLocation("sounds/player/projectilehit"), api.World.Player.Entity,
                        volume: config.Volume, randomizePitch: false);
                };
                
                return true;
            };

        }
    }
}    