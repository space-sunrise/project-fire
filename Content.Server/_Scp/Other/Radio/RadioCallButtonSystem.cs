using Content.Server.Radio.EntitySystems;
using Content.Shared.Interaction;
using Content.Shared.Pinpointer;
using Content.Shared.Timing;
using Robust.Server.GameObjects;

namespace Content.Server._Scp.Other.Radio;

public sealed class RadioCallButtonSystem : EntitySystem
{
    [Dependency] private readonly RadioSystem _radio = default!;
    [Dependency] private readonly TransformSystem _transform = default!;
    [Dependency] private readonly UseDelaySystem _delay = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<RadioCallButtonComponent, InteractHandEvent>(OnButtonPressed);
    }

    private void OnButtonPressed(Entity<RadioCallButtonComponent> ent, ref InteractHandEvent args)
    {
        // Check if the button is on cooldown.
        if (!_delay.TryResetDelay(ent.Owner, checkDelayed: true))
            return;

        var locationName = Loc.GetString("scp-radio-button-unknown-location");
        if (string.IsNullOrEmpty(ent.Comp.RoomName))
        {
            locationName = ExtractLocationName(ent, locationName);
        }
        else
        {
            locationName = ent.Comp.RoomName;
        }
        // Get the localized message.
        var message = Loc.GetString(ent.Comp.MessageKey, ("location", locationName));

        // Send the radio message.
        foreach (var channel in ent.Comp.RadioChannel)
        {
            _radio.SendRadioMessage(ent.Owner, message, channel, ent.Owner);
        }
    }

    private string ExtractLocationName(Entity<RadioCallButtonComponent> ent, string locationName)
    {
        // Get button's MapCoordinates via TransformSystem
        var coordinates = _transform.GetMapCoordinates(ent);

        // Initialize closest distance tracking variables
        var closest = ent.Comp.BeaconSearchRadius;

        var query = EntityQueryEnumerator<NavMapBeaconComponent, TransformComponent>();

        while (query.MoveNext(out var beaconUid, out var beacon, out var beaconXform))
        {
            if(!beacon.Enabled || !beaconXform.Anchored || coordinates.MapId != beaconXform.MapID)
                continue;

            var beaconCoords = _transform.GetMapCoordinates(beaconUid, beaconXform);
            var distance = (coordinates.Position - beaconCoords.Position).Length();

            if (distance <= closest && !string.IsNullOrEmpty(beacon.Text))
            {
                closest = distance;
                locationName = beacon.Text;
            }
        }

        return locationName;
    }
}
