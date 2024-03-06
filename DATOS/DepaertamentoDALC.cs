﻿using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class DepaertamentoDALC
    {
        public void Agregar(Departamento dpto)
        {
            using (var db= new ProyectosContext())
            {
                db.Departamento.Add(dpto);
                db.SaveChanges();
            }
        }

        public List<Departamento> ListarDepartamento()
        {
            using (var  db = new ProyectosContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Departamento.ToList();
            }
        }
        public Departamento GetDepartamento(int id)
        {
            using (var db= new ProyectosContext())
            {
                return db.Departamento.Where(d => d.DepartamentoId == id).FirstOrDefault();
            }
        }
        public void Editar(Departamento dpto)
        {
            using (var  db = new ProyectosContext())
            {
                var d = db.Departamento.Find(dpto.DepartamentoId);
                d.NombreDepartamento = dpto.NombreDepartamento;
                db.SaveChanges();
            }
        }
        public void Eliminar(int id)
        {
            using (var db= new ProyectosContext())
            {
                var dpto = db.Departamento.Find(id);
                db.Departamento.Remove(dpto);
                db.SaveChanges();
            }
        }
    }
}
