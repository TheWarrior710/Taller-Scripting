using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taller_Scripting
{
    class NodoRaiz : NodoBT
    {
        private NodoBT hijo;

        public NodoRaiz(NodoBT hijo) { this.hijo = hijo; }

        public override bool Ejecutar()
        {
            return hijo?.Ejecutar() ?? false;
        }
    }
}
