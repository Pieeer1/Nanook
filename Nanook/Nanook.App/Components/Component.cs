using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanook.App.Components
{
    public abstract class Component
    {

        public Entity Entity { get; set; } = null!;

        public virtual void Init()
        { 
        
        }
        public virtual void Update() 
        {
        
        }
        public virtual void Draw()
        { 
            
        }
    }
}
