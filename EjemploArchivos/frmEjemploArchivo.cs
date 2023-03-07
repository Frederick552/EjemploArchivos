using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemploArchivos
{
    public partial class frmEjemploArchivo : Form
    {
        public frmEjemploArchivo()
        {
            InitializeComponent();
        }
        // Se invoca cuando el usuario oprime una tecla
        private void txtEntrada_KeyDown(object sender, KeyEventArgs e)
        {
            // Determina si el usuario oprimio la tecla Intro
            if(e.KeyCode == Keys.Enter)
            {
                string nombreArchivo; // nombre del archivo

                // Obtiene el archivo o directorio especificado del usuario
                nombreArchivo = txtEntrada.Text;

                // Determina si nombreArchivo es un archivo
                if (File.Exists(nombreArchivo))
                {
                    txtSalida.Text = obtenerInformacion(nombreArchivo);
                    try
                    {
                        StreamReader sr = new StreamReader(nombreArchivo);
                        txtSalida.Text += sr.ReadToEnd();
                    }
                    // Maneja exception si el streamreader no esta disponible
                    catch (IOException)
                    {
                        MessageBox.Show("Error al leer el archivo", "Error de archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;  
                    }
                }
                else if (Directory.Exists(nombreArchivo))
                {
                    string[] listaDirectorios; //arreglo para los directorios

                    txtSalida.Text = obtenerInformacion(nombreArchivo);
                    listaDirectorios = Directory.GetDirectories(nombreArchivo);

                    txtSalida.Text += "\r\n\r\nContenido del directorio:\r\n";

                    for (int i = 0; i < listaDirectorios.Length; i++)
                    {
                        txtSalida.Text += listaDirectorios[i] + "\r\n";
                    }
                }
                else
                {
                    // Notifica al usuario 
                    MessageBox.Show(txtEntrada.Text + "no existe", "Error de archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
        }
        // Obtiene informacion sobre el archivo o directorio
        private string obtenerInformacion(string nombreArchivo)
        {
            String informacion;
            // Imprime mensaje indicado que existe en el archivo o directorio
            informacion = nombreArchivo + " existe\r\n\r\n";

            // imprimir
            informacion += "creacion:" +
                File.GetCreationTime(nombreArchivo) + "\r\n";

            informacion += "Ultima modificacion:" +
                File.GetLastWriteTime(nombreArchivo) + "\r\n";

            informacion += "Ultimo acceso:" +
                File.GetLastAccessTime(nombreArchivo) + "\r\n" + "\r\n";

            return informacion;
        }
    }
}
