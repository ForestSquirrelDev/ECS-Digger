using System.Collections.Generic;
using UnityEngine;

namespace Core.Input {
    public class ManagerInputCommandsPipe : MonoBehaviour {
        public readonly Queue<IManagerInputCommand> Commands = new ();
    }
}
