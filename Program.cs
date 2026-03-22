using System;

namespace ProyectoBST
{
    public class Nodo
    {
        public int Valor;
        public Nodo Izquierdo;
        public Nodo Derecho;

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    public class ArbolBinario
    {
        public Nodo Raiz;

        public void Insertar(int valor) => Raiz = InsertarRecursivo(Raiz, valor);
        private Nodo InsertarRecursivo(Nodo actual, int valor)
        {
            if (actual == null) return new Nodo(valor);
            if (valor < actual.Valor) actual.Izquierdo = InsertarRecursivo(actual.Izquierdo, valor);
            else if (valor > actual.Valor) actual.Derecho = InsertarRecursivo(actual.Derecho, valor);
            return actual;
        }

        public bool Buscar(int valor) => BuscarRecursivo(Raiz, valor);
        private bool BuscarRecursivo(Nodo actual, int valor)
        {
            if (actual == null) return false;
            if (valor == actual.Valor) return true;
            return valor < actual.Valor ? BuscarRecursivo(actual.Izquierdo, valor) : BuscarRecursivo(actual.Derecho, valor);
        }

        public void Eliminar(int valor) => Raiz = EliminarRecursivo(Raiz, valor);
        private Nodo EliminarRecursivo(Nodo actual, int valor)
        {
            if (actual == null) return null;
            if (valor < actual.Valor) actual.Izquierdo = EliminarRecursivo(actual.Izquierdo, valor);
            else if (valor > actual.Valor) actual.Derecho = EliminarRecursivo(actual.Derecho, valor);
            else {
                if (actual.Izquierdo == null) return actual.Derecho;
                if (actual.Derecho == null) return actual.Izquierdo;
                actual.Valor = ObtenerMinimo(actual.Derecho);
                actual.Derecho = EliminarRecursivo(actual.Derecho, actual.Valor);
            }
            return actual;
        }

        public void Preorden(Nodo n) { if (n != null) { Console.Write(n.Valor + " "); Preorden(n.Izquierdo); Preorden(n.Derecho); } }
        public void Inorden(Nodo n) { if (n != null) { Inorden(n.Izquierdo); Console.Write(n.Valor + " "); Inorden(n.Derecho); } }
        public void Postorden(Nodo n) { if (n != null) { Postorden(n.Izquierdo); Postorden(n.Derecho); Console.Write(n.Valor + " "); } }

        public int ObtenerMinimo(Nodo n) => n.Izquierdo == null ? n.Valor : ObtenerMinimo(n.Izquierdo);
        public int ObtenerMaximo(Nodo n) => n.Derecho == null ? n.Valor : ObtenerMaximo(n.Derecho);
        public int CalcularAltura(Nodo n) => n == null ? 0 : 1 + Math.Max(CalcularAltura(n.Izquierdo), CalcularAltura(n.Derecho));
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArbolBinario bst = new ArbolBinario();
            int opcion = 0;
            while (opcion != 8)
            {
                Console.WriteLine("\n--- SISTEMA ÁRBOL BINARIO (BST) ---");
                Console.WriteLine("1. Insertar  2. Buscar  3. Eliminar  4. Recorridos");
                Console.WriteLine("5. Min/Max   6. Altura  7. Limpiar   8. Salir");
                Console.Write("Seleccione una opción: ");
                
                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                switch (opcion)
                {
                    case 1:
                        Console.Write("Valor a insertar: "); 
                        if(int.TryParse(Console.ReadLine(), out int valIns)) bst.Insertar(valIns);
                        break;
                    case 2:
                        Console.Write("Valor a buscar: "); 
                        if(int.TryParse(Console.ReadLine(), out int valBus)) 
                            Console.WriteLine(bst.Buscar(valBus) ? "¡El valor existe!" : "El valor NO existe.");
                        break;
                    case 3:
                        Console.Write("Valor a eliminar: "); 
                        if(int.TryParse(Console.ReadLine(), out int valEli)) bst.Eliminar(valEli);
                        break;
                    case 4:
                        Console.WriteLine("\n--- Recorridos ---");
                        Console.Write("Inorden: "); bst.Inorden(bst.Raiz);
                        Console.Write("\nPreorden: "); bst.Preorden(bst.Raiz);
                        Console.Write("\nPostorden: "); bst.Postorden(bst.Raiz); Console.WriteLine();
                        break;
                    case 5:
                        if (bst.Raiz != null) Console.WriteLine($"Mínimo: {bst.ObtenerMinimo(bst.Raiz)}, Máximo: {bst.ObtenerMaximo(bst.Raiz)}");
                        else Console.WriteLine("Árbol vacío.");
                        break;
                    case 6:
                        Console.WriteLine("Altura del árbol: " + bst.CalcularAltura(bst.Raiz));
                        break;
                    case 7:
                        bst.Raiz = null; Console.WriteLine("Árbol vaciado correctamente.");
                        break;
                }
            }
        }
    }
}