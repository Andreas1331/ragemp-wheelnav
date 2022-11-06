using GTANetworkAPI;
using System.Collections.Generic;

public class WheelHandler : Script
{
    [RemoteEvent("OnWheelSliceClicked::Server")]
    public void OnWheelSliceClicked(Player player, int wheelID, int sliceID)
    {
        if (player == null)
            return;

        if (player.HasData("WheelData"))
        {
            PrimaryInteractionWheel wheel = player.GetData<PrimaryInteractionWheel>("WheelData");
            wheel?.InvokeSliceAction(wheelID, sliceID);
        }
    }
}
