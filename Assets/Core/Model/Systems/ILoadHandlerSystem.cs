using System.Collections.Generic;

namespace Core.Model.Systems {
    public interface ILoadHandlerSystem {
        public void Load(Dictionary<string, object> json);
    }
}
