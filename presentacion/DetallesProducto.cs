using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentacion
{
    public partial class DetallesProducto : Form
    {
        private Articulo articulo;
      

        public DetallesProducto(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text= "Detalle articulo";
          
        }

        private void DetallesProducto_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = articulo.Nombre;
            lblCodigo.Text = "( " + articulo.Codigo + " )";
            lblDescuento.Text = articulo.Descripcion;
            lblMarca.Text = articulo.Marca.Descripcion;
            lblCategoria.Text = articulo.Categoria.Descripcion;
            lblPrecio.Text = "$" + articulo.Precio.ToString("0");
            cargarImagen(articulo.ImagenUrl);

        }


        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {

                pbxArticulo.Load("https://th.bing.com/th/id/R.3e77a1db6bb25f0feb27c95e05a7bc57?rik=DswMYVRRQEHbjQ&riu=http%3a%2f%2fwww.coalitionrc.com%2fwp-content%2fuploads%2f2017%2f01%2fplaceholder.jpg&ehk=AbGRPPcgHhziWn1sygs8UIL6XIb1HLfHjgPyljdQrDY%3d&risl=&pid=ImgRaw&r=0");
            }
        }

    }
}
