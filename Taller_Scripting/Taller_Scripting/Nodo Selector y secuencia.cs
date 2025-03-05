using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Scripting
{
    class NodoSelector : NodoBT
    {
        private List<NodoBT> hijos = new List<NodoBT>();

        public void AgregarHijo(NodoBT hijo) => hijos.Add(hijo);

        public override bool Ejecutar()
        {
            foreach (var hijo in hijos)
            {
                if (hijo.Ejecutar()) return true; 
            }
            return false;
        }
    }

    class NodoSecuencia : NodoBT
    {
        private List<NodoBT> hijos = new List<NodoBT>();

        public void AgregarHijo(NodoBT hijo) => hijos.Add(hijo);

        public override bool Ejecutar()
        {
            foreach (var hijo in hijos)
            {
                if (!hijo.Ejecutar()) return false; 
            }
            return true;
        }
    }
}
