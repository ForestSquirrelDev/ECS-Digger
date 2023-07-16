namespace PoorMansECS.Systems {
    public interface ISystemsEventListener {
        void ReceiveEvent<T>(T systemEvent) where T : ISystemEvent;
    }
}
