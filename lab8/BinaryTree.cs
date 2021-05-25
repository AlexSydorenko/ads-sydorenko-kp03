using System;

namespace lab8
{
    public class BinaryTree<T>
    {
        private T[] nodes;

        public BinaryTree(int size)
        {
            this.nodes = new T[size];
        }

        public void AddNode(int index, T val)
        {
            if (index >= this.nodes.Length)
            {
                Array.Resize(ref this.nodes, Math.Max(this.nodes.Length * 2 + 1, index + 1));
            }
            this.nodes[index] = val;
        }

        public T[] PreorderTraversal()
        {
            T[] traversedNodes = new T[this.nodes.Length];
            int counter = 0;
            if (this.nodes.Length != 0 && this.nodes[0] != null)
            {
                traversedNodes[counter] = this.nodes[0];
                counter++;
            }

            if (this.nodes.Length > 1 && this.nodes[1] != null)
            {
                traversedNodes = PreorederTraversealSubtree(traversedNodes, 1, counter, out counter);
            }

            if (this.nodes.Length > 2 && this.nodes[2] != null)
            {
                traversedNodes = PreorederTraversealSubtree(traversedNodes, 2, counter, out counter);
            }
            Array.Resize(ref traversedNodes, counter);
            return traversedNodes;
        }

        private T[] PreorederTraversealSubtree(T[] traversedNodes, int index, int buf, out int counter)
        {
            counter = buf;
            traversedNodes[counter] = this.nodes[index];
            counter++;
            
            if (this.nodes.Length > index*2 + 1 && this.nodes[index*2 + 1] != null)
            {
                traversedNodes = PreorederTraversealSubtree(traversedNodes, index*2 + 1, counter, out counter);
            }

            if (this.nodes.Length > index*2 + 2 && this.nodes[index*2 + 2] != null)
            {
                traversedNodes = PreorederTraversealSubtree(traversedNodes, index*2 + 2, counter, out counter);
            }

            return traversedNodes;
        }

        public void PrintTree()
        {
            if (this.nodes.Length != 0 && this.nodes[0] != null)
            {
                Console.WriteLine("Root: " + this.nodes[0]);
            }

            if (this.nodes.Length > 1 && this.nodes[1] != null)
            {
                PrintRight(1, "     ");
            }
            
            if (this.nodes.Length > 2 && this.nodes[2] != null)
            {
                PrintLeft(2, "     ");
            }
        }

        private void PrintRight(int index, string shift)
        {
            Console.WriteLine(shift + "[R]: " + this.nodes[index]);

            if (this.nodes.Length > 2*index + 1 && this.nodes[2*index + 1] != null)
            {
                PrintRight(2*index + 1, shift + "      ");
            }

            if (this.nodes.Length > 2*index + 2 && this.nodes[2*index + 2] != null)
            {
                PrintLeft(2*index + 2, shift + "      ");
            }
        }

        private void PrintLeft(int index, string shift)
        {
            Console.WriteLine(shift + "[L]: " + this.nodes[index]);

            if (this.nodes.Length > 2*index + 1 && this.nodes[2*index + 1] != null)
            {
                PrintRight(2*index + 1, shift + "      ");
            }
            
            if (this.nodes.Length > 2*index + 2 && this.nodes[2*index + 2] != null)
            {
                PrintLeft(2*index + 2, shift + "      ");
            }
        }
    }
}
