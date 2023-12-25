using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Dominio
{
    public class userModel
    {
        userDao userDao = new userDao();
        public bool LoginUser(string nombreApellido, string clave)
        {
            return userDao.Login(nombreApellido, clave);
        }
    }
}
