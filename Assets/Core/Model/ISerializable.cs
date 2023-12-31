﻿using System.Collections.Generic;

namespace Core.Model {
    public interface ISerializable {
        public void Serialize(Dictionary<string, object> json);
        public void Deserialize(Dictionary<string, object> json);
    }
}
