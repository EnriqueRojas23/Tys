﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NPOI.SS.UserModel;


namespace Componentes.Common.Utilidades.CellStyleFactory
{
    public class CellStyleIzquierda : IEstilo
    {
        ICellStyle estilo;

        public CellStyleIzquierda() { }

        public void Set(IWorkbook wb)
        {
            if (wb == null) throw new ArgumentNullException("El libro al que pertenece el estilo no puede estar vacío.", "wb");

            estilo = wb.CreateCellStyle();
            estilo.Alignment = HorizontalAlignment.LEFT;
        }

        public void Set(IWorkbook wb, string alineamiento, string formato) { throw new Exception("Este estilo no implementa esta propiedad"); }

        public ICellStyle Get()
        {
            if (estilo == null) throw new Exception("Aún no se ha invocado el método Set(wb).");

            return estilo;
        }

        public short GetIndex()
        {
            return estilo.Index;
        }
    }
}
