using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanook.App.Components
{
    public class Entity
    {
        private HashSet<Component> components { get; set; } = new HashSet<Component>();
        public bool IsActive { get; private set; }

        public void Update()
        { 
            foreach (var component in components) 
            {
                component.Update();
            }
        }
        public void Draw()
        {
            foreach (var component in components)
            {
                component.Draw();
            }
        }
        public T AddComponent<T>(T newComponent) where T : Component
        {
            if (components.Any(x => x.GetType() == typeof(T)))
            {
                throw new InvalidOperationException("Entities may only have one of each Component.");
            }
            newComponent.Init();
            components.Add(newComponent);
            return newComponent;
        }
        public T GetComponent<T>() where T : Component => (components.First(x => x.GetType() == typeof(T)) as T ?? throw new InvalidCastException($"Could not Find any Components of Type {typeof(T).Name}"));
        public bool HasComponent<T>() where T : Component => components.Any(x => x.GetType() == typeof(T));
        public void Destroy() => IsActive = false;
    }
}
