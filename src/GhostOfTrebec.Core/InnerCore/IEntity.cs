namespace GhostOfTrebec.Core.InnerCore
{
    public interface IEntity<TId>
    {
        TId Identifier { get; }
    }
}
