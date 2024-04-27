using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Inventario_Ejecicio_TEAM
{
    public partial class Form1 : Form
    {
        DataTable tablanueva = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        struct vehiculo
        {
            public string matricula, marca, linea, modelo, color, peso, motor, tipoCaja, tipoCombustible;
        }
        static List<vehiculo> vehiculos = new List<vehiculo>();
        static string ubicacion = @"C:\Users\Carlos Alvarado\OneDrive\Imágenes\Vehiculos.txt";
        private void IngresarDatos_Click(object sender, EventArgs e)
        {
            if (textMarca.Text != "" && textMatricula.Text != "" && textModelo.Text != "" && textLinea.Text != "" && textCaja.Text != "" && textPeso.Text != "" && textMotor.Text != "" && textCombustible.Text != "")
            {


                vehiculo auto = new vehiculo();
                auto.marca = textMarca.Text;
                auto.matricula = textMatricula.Text;
                auto.linea = textLinea.Text;
                auto.modelo = textModelo.Text;
                auto.color = textColor.Text;
                auto.peso = textPeso.Text;
                auto.motor = textMotor.Text;
                auto.tipoCaja = textCaja.Text;
                auto.tipoCombustible = textCombustible.Text;
                vehiculos.Add(auto);
                string lineanueva = auto.marca + "," + auto.matricula + "," + auto.linea + "," + auto.modelo + "," + auto.color + "," + auto.peso + "," + auto.motor + "," + auto.tipoCaja + "," + auto.tipoCombustible;
                StreamWriter escribir = File.AppendText(ubicacion);
                escribir.WriteLine(lineanueva);
                escribir.Close();
                llenarData();
            }
            else
            {
                MessageBox.Show("Ingresa Valores en TODOS los campos para agregar!");
            }
        }
        private void llenarData()
        {
            tablanueva.Columns.Clear();
            tablanueva.Rows.Clear();
            tablanueva.Columns.Add("Marca");
            tablanueva.Columns.Add("Matricula");
            tablanueva.Columns.Add("Linea");
            tablanueva.Columns.Add("Peso");
            tablanueva.Columns.Add("Modelo");
            tablanueva.Columns.Add("Color");
            tablanueva.Columns.Add("Motor");
            tablanueva.Columns.Add("Tipo de Combustible");
            tablanueva.Columns.Add("Tipo de Caja");
            int cantidadVehicular = vehiculos.Count;
            for (int i = 0; i < cantidadVehicular; i++)
            {
                tablanueva.Rows.Add(vehiculos[i].marca, vehiculos[i].matricula, vehiculos[i].linea, vehiculos[i].modelo, vehiculos[i].color, vehiculos[i].peso, vehiculos[i].motor, vehiculos[i].tipoCaja, vehiculos[i].tipoCombustible);
            }
            dataGridView1.DataSource = tablanueva;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int columna = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            MessageBox.Show($"Desea operar la fila {columna.ToString()}?");
            textMarca.Text = dataGridView1.Rows[columna].Cells[0].Value.ToString();
            textMatricula.Text = dataGridView1.Rows[columna].Cells[1].Value.ToString();
            textLinea.Text = dataGridView1.Rows[columna].Cells[2].Value.ToString();
            textModelo.Text = dataGridView1.Rows[columna].Cells[3].Value.ToString();
            textColor.Text = dataGridView1.Rows[columna].Cells[4].Value.ToString();
            textPeso.Text = dataGridView1.Rows[columna].Cells[5].Value.ToString();
            textMotor.Text = dataGridView1.Rows[columna].Cells[6].Value.ToString();
            textCaja.Text = dataGridView1.Rows[columna].Cells[7].Value.ToString();
            textCombustible.Text = dataGridView1.Rows[columna].Cells[8].Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader leerArchivo = new StreamReader(ubicacion);
            while (!leerArchivo.EndOfStream)
            {
                string linea = leerArchivo.ReadLine();
                string[] aux = linea.Split(',');
                vehiculo auto = new vehiculo();
                auto.marca = aux[0];
                auto.matricula = aux[1];
                auto.linea = aux[2];
                auto.modelo = aux[3];
                auto.color = aux[4];
                auto.peso = aux[5];
                auto.motor = aux[6];
                auto.tipoCaja = aux[7];
                auto.tipoCombustible = aux[8];
                vehiculos.Add(auto);
            }
            leerArchivo.Close();
            llenarData();
        }

        private void EditarDatos_Click(object sender, EventArgs e)
        {
                int columna = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
                MessageBox.Show($"Desea editar la fila {columna.ToString()}?");
                vehiculo autoeditado = new vehiculo();
                autoeditado.marca = textMarca.Text;
                autoeditado.matricula = textMatricula.Text;
                autoeditado.linea = textLinea.Text;
                autoeditado.modelo = textModelo.Text;
                autoeditado.color = textColor.Text;
                autoeditado.peso = textPeso.Text;
                autoeditado.motor = textMotor.Text;
                autoeditado.tipoCaja = textCaja.Text;
                autoeditado.tipoCombustible = textCombustible.Text;
                vehiculos[columna] = autoeditado;
                llenarData();
                File.Delete(ubicacion);
                for (int i = 0; i < vehiculos.Count; i++)
                {
                string lineanueva = vehiculos[i].marca + "," + vehiculos[i].matricula + "," + vehiculos[i].linea + "," + vehiculos[i].modelo + "," + vehiculos[i].color + "," + vehiculos[i].peso + "," + vehiculos[i].motor + "," + vehiculos[i].tipoCaja + "," + vehiculos[i].tipoCombustible;
                StreamWriter escritura = File.AppendText(ubicacion);
                    escritura.WriteLine(lineanueva);
                    escritura.Close();
                }
        }

        private void BorrarDatos_Click(object sender, EventArgs e)
        {
            int columna = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            MessageBox.Show($"Desea eliminar la fila {columna.ToString()}?");
            vehiculos.RemoveAt(columna);
            llenarData();
            File.Delete(ubicacion);
            for (int i = 0; i < vehiculos.Count; i++)
            {
                string lineanueva = vehiculos[i].marca + "," + vehiculos[i].matricula + "," + vehiculos[i].linea + "," + vehiculos[i].modelo + "," + vehiculos[i].color + "," + vehiculos[i].peso + "," + vehiculos[i].motor + "," + vehiculos[i].tipoCaja + "," + vehiculos[i].tipoCombustible;
                StreamWriter escribir = File.AppendText(ubicacion);
                escribir.WriteLine(lineanueva);
                escribir.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textMarca.Clear();
            textMatricula.Clear();
            textLinea.Clear();
            textModelo.Clear();
            textColor.Clear();
            textPeso.Clear();
            textMotor.Clear();
            textCaja.Clear();
            textCombustible.Clear();
            listadeConteo.ClearSelected();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void contarElementos_Click(object sender, EventArgs e)
        {
            string fileUrl = ubicacion;
            List<string[]> data = new List<string[]>();
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(fileUrl);
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] rowData = line.Split(',');
                    data.Add(rowData);
                }
            }
            Dictionary<string, int> brandCounts = new Dictionary<string, int>();
            Dictionary<string, int> gearboxCounts = new Dictionary<string, int>();
            Dictionary<string, int> fuelCounts = new Dictionary<string, int>();

            foreach (string[] rowData in data)
            {
                string brand = rowData[0];

                if (!string.IsNullOrEmpty(brand))
                {
                    if (!brandCounts.ContainsKey(brand))
                    {
                        brandCounts.Add(brand, 0);
                    }

                    brandCounts[brand]++;
                }
                string gearbox = rowData[8];

                if (!string.IsNullOrEmpty(gearbox))
                {
                    if (!gearboxCounts.ContainsKey(gearbox))
                    {
                        gearboxCounts.Add(gearbox, 0);
                    }

                    gearboxCounts[gearbox]++;
                }
                string fuel = rowData[7];

                if (!string.IsNullOrEmpty(fuel))
                {
                    if (!fuelCounts.ContainsKey(fuel))
                    {
                        fuelCounts.Add(fuel, 0);
                    }

                    fuelCounts[fuel]++;
                }
            }
            listadeConteo.Items.Clear();

            foreach (KeyValuePair<string, int> pair in brandCounts)
            {
                listadeConteo.Items.Add("Marca: " + pair.Key + " = " + pair.Value);
            }

            foreach (KeyValuePair<string, int> pair in gearboxCounts)
            {
                listadeConteo.Items.Add("Tipo de Caja: " + pair.Key + " = " + pair.Value);
            }

            foreach (KeyValuePair<string, int> pair in fuelCounts)
            {
                listadeConteo.Items.Add("Tipo de combustible: " + pair.Key + " = " + pair.Value);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void infoBTN_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creado por: \n\nCarlos Fernando Alvarado Lima \nBachiller en Computación", "Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
