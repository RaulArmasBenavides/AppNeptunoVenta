using System.Collections.Generic;
using System.Data.SqlClient;

namespace Neptuno.Libreria.Service
{
    public interface IServicioNeptuno <T>
    {
        #region Firmas
        void create(SqlConnection c, T t);
        void update(SqlConnection c, T t);
        void delete(SqlConnection c, T t);
        T FindForId(SqlConnection c, object t);
        List<T> readAll(SqlConnection c);

        #endregion
    }

    public interface IServicioNeptunoGenerico<T>
    {
        //definir las firmas
        void create(T o);
        void update(T o);
        void delete(T o);
        T findForId(int o);
        List<T> readAll();

    }
}
