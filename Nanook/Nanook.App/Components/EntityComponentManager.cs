using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanook.App.Components
{
    public class EntityComponentManager
    {
        private HashSet<Entity> entities { get; set; } = new HashSet<Entity>();
        private List<Group> groups { get; set; } = new List<Group>();
        public void Update()
        { 
            foreach (var entity in entities) 
            {
                entity.Update();
            }
        }
        public void Draw()
        { 
            foreach(var entity in entities) 
            {
                entity.Draw();
            }
        }
        public void Refresh()
        {
            foreach (var entity in entities)
            {
                if (!entity.IsActive)
                { 
                    entity.Destroy();
                }
            }
        }

        public Entity AddEntity()
        {
            Entity e = new Entity();
            entities.Add(e);
            return e;
        }
        public void AddGroup(Group group)
        {
            if (groups.Select(x => x.Index).Contains(group.Index) && groups.Select(x => x.Name).Contains(group.Name))
            {
                throw new InvalidOperationException("Cannot Create Group, Duplicate Index or Name");
            }
            groups.Add(group);
        }
        public void AddEntityToGroup(Entity e, Group group)
        {
            if (groups.Select(x => x.Index).Contains(group.Index) && groups.Select(x => x.Name).Contains(group.Name))
            {
                groups.First(x => x.Name == group.Name && x.Index == group.Index).AddEntityToGroup(e);
            }
            else
            {
                group.AddEntityToGroup(e);
                AddGroup(group);
            }
        }
        public void RemoveGroup(Group group)
        {
            if (groups.Any(x => x == group))
            { 
                groups.Remove(group);
            }
        }
        public List<Group> GetGroups() => groups;

    }
}
