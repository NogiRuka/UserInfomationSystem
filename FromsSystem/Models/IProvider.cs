using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FromsSystem.Models
{
    public interface IProvider<T> where T:class
    {
        List<T> Select();

        int Insert(T t);
         
        int Update(T t);

        int Delete(T t);


    }
}