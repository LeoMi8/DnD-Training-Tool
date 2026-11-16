namespace DnDTrainingTool.Domain.Characters.State;

public sealed class CreatureInstance
{
    public Guid InstanceId { get; }
    public CreatureSnapshot Snapshot { get; }
    public CreatureState State { get; }

        public CreatureInstance(
        Guid instanceId,
        CreatureSnapshot snapshot,
        CreatureState state)
    {
        if (instanceId == Guid.Empty)
            throw new ArgumentException(
                "Instance ID cannot be empty.",
                nameof(instanceId));

        InstanceId = instanceId;

        Snapshot = snapshot
            ?? throw new ArgumentNullException(nameof(snapshot));

        State = state
            ?? throw new ArgumentNullException(nameof(state));
    }
}