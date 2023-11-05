using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace presentacion
{
    public partial class Form1 : Form
    {
        public List<Articulo> listaArticulos { get; set; }
        public Form1()
        {
            InitializeComponent();
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarTabla();
            dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "F0";

            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            cbxCategoria.DataSource = negocioCategoria.listar();
            cbxCategoria.ValueMember = "Id";
            cbxCategoria.DisplayMember = "Descripcion";

            MarcaNegocio negocioMarcas = new MarcaNegocio();
            cbxMarca.DataSource = negocioMarcas.listar();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";
           

            cbxPrecio.Items.Add("[0-20000]");
            cbxPrecio.Items.Add("[20000-40000]");
            cbxPrecio.Items.Add("[+40000]");
            cbxPrecio.SelectedIndex = 0;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                string categoria = cbxCategoria.SelectedItem.ToString();
                string marca = cbxMarca.SelectedItem.ToString();
                string precio = cbxPrecio.SelectedItem.ToString();
                dgvArticulos.DataSource = negocio.filtrar(categoria, marca, precio);
                ocultarColumnas();
                btnRestablecerTabla.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnRestablecerTabla_Click(object sender, EventArgs e)
        {
            cargarTabla();
            btnRestablecerTabla.Visible = false;
        }

        private void cargarTabla()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                ocultarColumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
          
        }
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;

        }


        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtNombre.Text;

            listaFiltrada = listaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()));
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtCodigo.Text;

            listaFiltrada = listaArticulos.FindAll(x => x.Codigo.ToUpper().Contains(filtro.ToUpper()));
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

       

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAgregarModificar alta = new FrmAgregarModificar();
            alta.ShowDialog();
            cargarTabla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {

                try
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    FrmAgregarModificar modificar = new FrmAgregarModificar(seleccionado);
                    modificar.ShowDialog();
                    cargarTabla();
                    ocultarColumnas();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            } else
            {
                MessageBox.Show("Debe seleccionar un artículo a modificar");
                cargarTabla();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo;

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Estas seguro que queres eliminar el artículo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    articulo = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(articulo);
                    cargarTabla();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null)
            {
                try
                {
                    Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    DetallesProducto detalles = new DetallesProducto(seleccionado);
                    detalles.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    
                }
            } else
            {
                MessageBox.Show("Debe seleccionar un artículo");
            }
          
        }      

        private void dgvArticulos_DataSourceChanged(object sender, EventArgs e)
        {
            btnEliminar.Enabled = dgvArticulos.Rows.Count > 0;
        }

       
    }
}

            
