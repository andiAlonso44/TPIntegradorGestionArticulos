using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace presentacion
{
    public partial class FrmAgregarModificar : Form
    {
        private Articulo articulo = null;
        public FrmAgregarModificar()
        {
            InitializeComponent();
        }

        public FrmAgregarModificar(Articulo articulo)
        {  
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar artículo";
            btnAceptar.BackColor = Color.Olive;
            btnCancelar.BackColor = Color.Olive;
           
        }

        private void FrmAgregarModificar_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            cbxMarca.DataSource = marcaNegocio.listar();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";
            cbxCategoria.DataSource = categoriaNegocio.listar();
            cbxCategoria.ValueMember = "Id";
            cbxCategoria.DisplayMember = "Descripcion";

            if(articulo != null)
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtImagen.Text = articulo.ImagenUrl;
                txtPrecio.Text = articulo.Precio.ToString("0");
                cbxCategoria.SelectedValue = articulo.Categoria.Id;
                cbxMarca.SelectedValue = articulo.Marca.Id;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();             

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.ImagenUrl = txtImagen.Text; 
                if(string.IsNullOrWhiteSpace(txtPrecio.Text)) {
                    MessageBox.Show("El campo precio solo acepta valores numéricos y es obligatorio");
                    return;
                } else
                {
                    articulo.Precio = decimal.Parse(txtPrecio.Text);                        
                }
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
               

                if (ValidarCajasDeTexto(txtCodigo, txtNombre, txtDescripcion, txtImagen, txtPrecio))
                {
                    if (articulo.Id != 0)
                    {
                        negocio.modificar(articulo);
                        MessageBox.Show("Modificado exitosamente");
                    }
                    else
                    {
                        negocio.agregar(articulo);
                        MessageBox.Show("Agregado exitosamente");
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }


            }

            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
                
            }
        }
           
        private void btnCancelar_Click(object sender, EventArgs e)
        {
           
            Close();
        }

        private bool ValidarCajasDeTexto(params TextBox[] cajasDeTexto)
        {
            foreach (TextBox textBox in cajasDeTexto)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {

                    return false;
                }
            }
            return true;
        }   

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                lblValidacionPrecio.Visible = true;
                e.Handled = true;
            }
            else
            {
                lblValidacionPrecio.Visible = false;

            }
        }
    }
}
