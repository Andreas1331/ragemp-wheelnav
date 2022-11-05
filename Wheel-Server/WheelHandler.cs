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

    public void Example(Player player)
    {
        string primaryTitle = $"{player.Name}";
        PrimaryInteractionWheel wheel = new PrimaryInteractionWheel(0, primaryTitle, keyToBind: "c");
        InteractionWheel vehicleWheel = new InteractionWheel(1, "Your vehicle(s)");
        InteractionWheel extraWheel = new InteractionWheel(2, "Walkingstyle");
        wheel.AddSubWheel(vehicleWheel);
        wheel.AddSubWheel(extraWheel);

        List<object> slices = new List<object>()
                {
                    new WheelSliceAction("icon.inventory", () => player.SendChatMessage("Showing your inventory..")),
                    new WheelSliceSubMenu("icon.car", vehicleWheel.ID), // Bind this slice to open a subWheel
                    new WheelSliceSubMenu("icon.list", extraWheel.ID), // Bind this slice to open a subWheel
                };
        wheel.Slices = slices;

        string[] vehicles = { "Volvo", "BMW", "Ford", "Mazda" };
        List<object> vehicleSlices = new List<object>();
        int vehicleWheelID = 3; // Each wheel needs a unique ID - so we increment in each iteration
        foreach (string veh in vehicles)
        {
            InteractionWheel vehWheel = new InteractionWheel(vehicleWheelID, "Vehicle");
            wheel.AddSubWheel(vehWheel);
            var spawnSlice = new WheelSliceAction("icon.palm", () => player.SendChatMessage("Spawning your vehicle.."));
            var despawnSlice = new WheelSliceAction("icon.cross", () => player.SendChatMessage("Despawning your vehicle.."));
            var backSlice = new WheelSliceSubMenu("icon.arrowleft", vehicleWheel.ID);
            vehWheel.Slices = new List<object>();
            vehWheel.Slices.Add(spawnSlice);
            vehWheel.Slices.Add(despawnSlice);

            var slice = new WheelSliceSubMenu(veh, vehicleWheelID);
            vehicleSlices.Add(slice);
            vehicleWheelID++;
        }

        vehicleSlices.Add(new WheelSliceSubMenu("icon.arrowleft", wheel.ID));
        vehicleWheel.Slices = vehicleSlices; // Add all the vehicle menus to the subWheel

        List<object> extraSlices = new List<object>()
            {
                new WheelSliceAction("Normal", () => player.SetSharedData("walkingStyle", "Normal")),
                new WheelSliceAction("Brave", () => player.SetSharedData("walkingStyle", "Brave")),
                new WheelSliceAction("Confident", () => player.SetSharedData("walkingStyle", "Confident")),
                new WheelSliceAction("Drunk", () => player.SetSharedData("walkingStyle", "Drunk")),
                new WheelSliceAction("Fat", () => player.SetSharedData("walkingStyle", "Fat")),
                new WheelSliceAction("Gangster", () => player.SetSharedData("walkingStyle", "Gangster")),
                new WheelSliceAction("Hurry", () => player.SetSharedData("walkingStyle", "Hurry")),
                new WheelSliceAction("Injured", () => player.SetSharedData("walkingStyle", "Injured")),
                new WheelSliceAction("Intimidated", () => player.SetSharedData("walkingStyle", "Intimidated")),
                new WheelSliceAction("Quick", () => player.SetSharedData("walkingStyle", "Quick")),
                new WheelSliceAction("Sad", () => player.SetSharedData("walkingStyle", "Sad")),
                new WheelSliceAction("Tough", () => player.SetSharedData("walkingStyle", "Tough"))
            };
        extraSlices.Add(new WheelSliceSubMenu("icon.arrowleft", wheel.ID));
        extraWheel.Slices = extraSlices;

        wheel.Display(player);
    }
}
