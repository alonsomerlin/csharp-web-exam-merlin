﻿using System.Linq.Expressions;

namespace csharp_web_exam_api.Repository.IRepository
{
    public interface IRepository<T> where T: class          
    {
        Task Crear(T entidad);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>>? filtro = null);
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked = true);
        Task Remover(T entidad);
        Task Grabar();
    }
}
