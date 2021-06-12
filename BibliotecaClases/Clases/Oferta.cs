﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;


namespace BibliotecaClases.Clases
{
    public class Oferta
    {
        [Key]
        public int IdOferta { get; set; }
        public String OfertaTitulo { get; set; }
        public bool Activo { get; set; }
        public DateTime OfertaFechaDesde { get; set; }
        public DateTime OfertaFechaHasta { get; set; }

        public virtual List<Imagen> Imagenes { get; set; }
        public String OfertaDescripcion { get; set; }
        public decimal OfertaPrecio { get; set; }

    }

    }
