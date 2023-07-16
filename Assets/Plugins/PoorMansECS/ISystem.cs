namespace PoorMansECS.Systems {
    public interface ISystem : IUpdateable {
        void Start();
        void Stop();
    }
}
