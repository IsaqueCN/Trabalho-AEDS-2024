using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_AEDS_2024
{
    internal class ListaLinear
    {

        private Jogador[] array;
        private int n;

        public int Count => n;
        public ListaLinear() { inicializar(0); }
        public ListaLinear(int tamanho)
        {
            inicializar(tamanho);
        }
        private void inicializar(int tamanho)
        {
            array = new Jogador[tamanho];
            n = 0;
        }
        public void inserirInicio(Jogador x)
        {
            if (n >= array.Length)
                throw new Exception("Lista está cheia!");
            //levarelementosparaofimdoarray
            for (int i = n; i > 0; i--)
            {
                array[i] = array[i - 1];
            }
            array[0] = x;
            n++;
        }
        public void inserirFim(Jogador x)
        {
            if (n >= array.Length)
                throw new Exception("Lista está cheia!");
            array[n] = x;
            n++;
        }
        public void inserir(Jogador x, int pos)
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
        public Jogador removerInicio()
        {
            if (n == 0)
                throw new Exception("Erro!");
            Jogador resp = array[0];
            n--;
            for (int i = 0; i < n; i++)
            {
                array[i] = array[i + 1];
            }

            return resp;
        }
        public Jogador removerFim()
        {
            if (n == 0)
                throw new Exception("Erro!");
            n = n - 1;
            return array[n];
        }
        public Jogador remover(int pos)
        {
            if (n == 0 || pos < 0 || pos >= n)
                throw new Exception("Erro!");
            Jogador resp = array[pos];
            n--;
            for (int i = pos; i < n; i++)
            {
                array[i] = array[i + 1];
            }
            return resp;
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
