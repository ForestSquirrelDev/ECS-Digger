namespace PoorMansECS.Systems {
    public class SystemsContext {
        public World World { get; }
        public SystemsEventBus EventBus { get; }

        public SystemsContext(World world, SystemsEventBus eventBus) {
            World = world;
            EventBus = eventBus;
        }
    }
}
