using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class GameObject
    {
        public string name;
        public Point position = Point.Create(0, 0);
        public Texture graphic = Texture.Create();
        public bool screenSpaceGraphic = false;
        private Component[] components = new Component[0];
        public List<Component> GetAllComponents()
        {
            return new List<Component>(components);
        }
        public Component GetComponent(int index)
        {
            if (index < 0 || index >= components.Length)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            else
            {
                return components[index];
            }
        }
        public Component GetComponent(Type targetType)
        {
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i].GetType() == targetType)
                {
                    return components[i];
                }
            }
            return null;
        }
        public int GetComponentCount()
        {
            return components.Length;
        }
        public void AddComponent(Component newComponent)
        {
            if (GetComponent(newComponent.GetType()) != null)
            {
                throw new Exception("Duplicate Component Exception!");
            }
            newComponent.parent = this;
            List<Component> temp = new List<Component>(components);
            temp.Add(newComponent);
            components = temp.ToArray();
        }
        public void RemoveComponent(Type targetType)
        {
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i].GetType() == targetType)
                {
                    List<Component> temp = new List<Component>(components);
                    temp.RemoveAt(i);
                    components = temp.ToArray();
                    return;
                }
            }
        }
        public void RemoveComponent(int index)
        {
            List<Component> temp = new List<Component>(components);
            temp.RemoveAt(index);
            components = temp.ToArray();
        }
        public void Initialize()
        {
            foreach (Component c in components)
            {
                c.parent = this;
                c.Initialize();
            }
        }
        public void Update(UpdatePacket packet)
        {
            foreach (Component c in components)
            {
                c.Update(packet);
            }
        }
        public static GameObject Create()
        {
            GameObject output = new GameObject();
            output.components = new Component[0];
            output.position = Point.Create(0, 0);
            output.screenSpaceGraphic = false;
            return output;
        }
        public static GameObject Create(Component[] components)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = Point.Create(0, 0);
            output.graphic = Texture.Create();
            output.name = "Unnamed GameObject";
            output.screenSpaceGraphic = false;
            return output;
        }
        public static GameObject Create(Component[] components, Point position)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = position.Clone();
            output.graphic = Texture.Create();
            output.name = "Unnamed GameObject";
            output.screenSpaceGraphic = false;
            return output;
        }
        public static GameObject Create(Component[] components, Point position, Texture graphic)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = position.Clone();
            output.graphic = graphic.Clone();
            output.name = "Unnamed GameObject";
            output.screenSpaceGraphic = false;
            return output;
        }
        public static GameObject Create(Component[] components, Point position, Texture graphic, bool screenSpaceGraphic, string name)
        {
            GameObject output = new GameObject();
            output.components = new List<Component>(components).ToArray();
            for (int c = 0; c < components.Length; c++)
            {
                components[c].parent = output;
            }
            output.position = position.Clone();
            output.graphic = graphic.Clone();
            output.name = name;
            output.screenSpaceGraphic = screenSpaceGraphic;
            return output;
        }
        public static int nextFreeID = 0;
        private readonly int ID = 0;
        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != typeof(GameObject))
            {
                return false;
            }
            else
            {
                return this == (GameObject)obj;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(GameObject a, GameObject b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            else if (a is null || b is null)
            {
                return false;
            }
            return a.ID == b.ID;
        }
        public static bool operator !=(GameObject a, GameObject b)
        {
            return !(a == b);
        }
        private GameObject()
        {
            ID = nextFreeID;
            nextFreeID++;
        }
    }
}