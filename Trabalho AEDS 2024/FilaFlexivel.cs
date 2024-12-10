using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Trabalho_AEDS_2024
{
    class Celula
    {
        private int elemento;
        private Celula prox;
        public Celula(int elemento)
        {
            this.elemento = elemento;
            this.prox = null;
        }
        public Celula()
        {
            this.elemento = 0;
            this.prox = null;
        }
        public Celula Prox
        {
            get { return prox; }
            set { prox = value; }
        }
        public int Elemento
        {
            get { return elemento; }
            set { elemento = value; }
        }
    }
    internal class FilaFlexivel
    {
        private Celula primeiro,ultimo;
        private int count = 0;
        public int Count => count;
        public FilaFlexivel()
        {
            primeiro = new Celula();
            ultimo = primeiro;
        }
        public void Inserir(int x) {
            ultimo.Prox = new Celula(x);
            ultimo = ultimo.Prox;
            count++;
        }
        public int Remover() {
            if (primeiro == ultimo)
                throw new Exception("Erro!"); 

            Celula tmp = primeiro;
            primeiro = primeiro.Prox;
            int elemento = primeiro.Elemento;
            tmp.Prox = null;
            tmp = null;
            count--;
            return elemento;
        }
        public void Mostrar() {
            Console.Write("[");
            for (Celula i = primeiro.Prox; i != null; i = i.Prox)
            {
                Console.Write(i.Elemento + ((i.Prox == null) ? "" : " "));
            }
            Console.WriteLine("]");
        }
    }
}
