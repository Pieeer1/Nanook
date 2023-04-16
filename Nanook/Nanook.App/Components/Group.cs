using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanook.App.Components
{
    public class Group
    {
        public Group(int index, string name)
        {
            Index = index;
            Name = name;
        }

        public int Index { get; private set; }
        public string Name { get; private set; }
        public List<Entity> Entities { get; private set; } = new List<Entity>();
        public void AddEntityToGroup(Entity entity) => Entities.Add(entity);
    }
}
