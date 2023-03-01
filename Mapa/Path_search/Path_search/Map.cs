using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Hledani_cesty
{
    internal class Map
    {
        public int[] size_map { get; }
        Dictionary<int[], int> map_hash;
        public Map(int size_x, int size_y)
        {
            size_map = new int[2];
            size_map[0] = size_x;
            size_map[1] = size_y;
            map_hash = new Dictionary<int[], int>();
        }
        public Map(Dictionary<int[], int> map_hash, int[] n)
        {
            this.size_map = n;
            this.map_hash = map_hash;
        }
        public void add_to_map(int st, int columns, int rows)
        {
            map_hash.Add(new int[2] {columns,rows}, st);
        }
        public void delete_row(int row_index)
        {
            for(int i = 0; i < size_map[0]; i++)
            {
                map_hash.Remove(new int[2] { i, row_index });
            }
            size_map[1] = size_map[1] - 1;
        }
        public void delete_collumn(int columns_index)
        {
            for (int i = 0; i < size_map[1]; i++)
            {
                map_hash.Remove(new int[2] { columns_index, i });
            }
            size_map[0] = size_map[0] - 1;
        }
    }
}
