namespace PoorMansECS.Systems {
    public abstract class SystemBase : ISystem {
        protected readonly SystemsContext _context;

        protected SystemBase(SystemsContext context) {
            _context = context;
        }

        public void Start() {
            OnStart();
        }

        public void Update(float delta) {
            OnUpdate(delta);
        }

        public void Stop() {
            OnStop();
        }

        protected abstract void OnStart();
        protected abstract void OnUpdate(float delta);
        protected abstract void OnStop();
    }
}
