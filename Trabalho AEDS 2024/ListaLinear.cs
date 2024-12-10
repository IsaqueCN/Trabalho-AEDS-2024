using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AEDS_2024
{
    internal class ListaLinear
    {

        private Carta[] array;
        private int n;

        public int Count => n;
        public ListaLinear() { inicializar(0); }
        public ListaLinear(int tamanho)
        {
            inicializar(tamanho);
        }
        private void inicializar(int tamanho)
        {
            array = new Carta[tamanho];
            n = 0;
        }
        public void inserirInicio(Carta x)
        {
            if (n >= array.Length)
                throw new Exception("Lista está cheia!");

            for (int i = n; i > 0; i--)
            {
                array[i] = array[i - 1];
            }
            array[0] = x;
            n++;
        }
        public void inserirFim(Carta x)
        {
            if (n >= array.Length)
                throw new Exception("Lista está cheia!");
            array[n] = x;
            n++;
        }
        public void inserir(Carta x, int pos)
        {
            if (n >= array.Length || pos < 0 || pos > n)
                throw new Exception("Index incorreto!");

            for (int i = n; i > pos; i--)
            {
                array[i] = array[i - 1];
            }
            array[pos] = x;
            n++;
        }
        public Carta removerInicio()
        {
            if (n == 0)
                throw new Exception("Erro!");
            Carta resp = array[0];
            n--;
            for (int i = 0; i < n; i++)
            {
                array[i] = array[i + 1];
            }

            return resp;
        }
        public Carta removerFim()
        {
            if (n == 0)
                throw new Exception("Erro!");
            n = n - 1;
            return array[n];
        }
        public Carta remover(int pos)
        {
            if (n == 0 || pos < 0 || pos >= n)
                throw new Exception("Erro!");
            Carta resp = array[pos];
            n--;
            for (int i = pos; i < n; i++)
            {
                array[i] = array[i + 1];
            }
            return resp;
        }
        public Carta ElementoEm(int index)
        {
            if (index < 0 || index >= n)
                throw new Exception("Index inválido!");

            return array[index];
        }
        public void mostrar()
        {
            Console.Write("[");
            for (int i = 0; i < n; i++)
            {
                Console.Write(array[i].Nome + " ");
            }
            Console.WriteLine("]");
        }
    }
}
