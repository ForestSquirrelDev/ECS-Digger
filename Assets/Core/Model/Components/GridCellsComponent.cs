using System.Collections.Generic;
using Core.Model.Entities;
using Core.Utils;
using PoorMansECS.Components;
using UnityEngine;

namespace Core.Model.Components {
    public readonly struct GridCellsComponent : IComponent, ISerializable {
        public Vector2Int Size { get; }
        public GridCellEntity[,] GridCells { get; }
    
        public GridCellsComponent(GridCellEntity[,] gridCells, Vector2Int size) {
            GridCells = gridCells;
            Size = size;
        }

        public void Serialize(Dictionary<string, object> json) {
            for (int x = 0; x < GridCells.GetLength(0); x++) {
                for (int y = 0; y < GridCells.GetLength(1); y++) {
                    var cellNode = new Dictionary<string, object>();
                    var cell = GridCells[x, y];
                    cell.Serialize(cellNode);
                    json[$"cell_{x}_{y}"] = cell;
                }
            }
        }

        public void Deserialize(Dictionary<string, object> json) {
            for (int x = 0; x < GridCells.GetLength(0); x++) {
                for (int y = 0; y < GridCells.GetLength(1); y++) {
                    var cellNode = json.GetNode($"cell_{x}_{y}");
                    var cell = GridCells[x, y];
                    cell.Deserialize(cellNode);
                }
            }
        }
    }
}