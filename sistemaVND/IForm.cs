using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaVND
{
	public interface IForm
	{
		void ChangeInfoCliente(string nombreApellido, int cliente);

		void ChangeInfoArticulos(string[] art);
	}
}
