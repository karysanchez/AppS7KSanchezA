using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using AppS7KSanchezA.Models;


namespace AppS7KSanchezA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Registro()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Verificando ingreso de datos no sean nulos, ni vacios
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasenia.Text))
                {
                    DisplayAlert("IMPORTANTE", "Datos son requeridos", "ok");
                }
                else
                {

                    var Registros = new Estudiante
                    {
                        Nombre = txtNombre.Text,
                        Usuario = txtUsuario.Text,
                        Contrasenia = txtContrasenia.Text
                    };
                    con.InsertAsync(Registros);
                    DisplayAlert("Alerta", "Dato Ingresado", "OK");
                    inicializar();
                    regresar();

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        private void inicializar()
        {
            txtNombre.Text = "";
            txtContrasenia.Text = "";
            txtUsuario.Text = "";
        }

        public async void regresar()
        {
            await Navigation.PushAsync(new Login());
        }
    }
}