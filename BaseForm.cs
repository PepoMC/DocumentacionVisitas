using System;
using System.Windows.Forms;

namespace Visitas
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            try
            {
                // El nombre "Icono" es el que pusiste en el campo "Nombre"
                this.Icon = Properties.Resources.Icono;
            }
            catch (Exception)
            {
                // Si no encuentra el icono, continúa sin él
            }
        }
    }
}