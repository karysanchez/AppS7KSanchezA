using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using AppS7KSanchezA.Models;
using System.IO;

namespace AppS7KSanchezA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Login()
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
        }

        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario, string contrasenia)
        {
            return db.Query<Estudiante>("SELECT * FROM Estudiante where Usuario = ? and Contrasenia = ?", usuario, contrasenia);
        }

        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Verificando ingreso de datos no sean nulos, ni vacios
                if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContrasenia.Text))
                {
                    DisplayAlert("IMPORTANTE", "Usuario y clave son requeridos", "ok");
                }
                else
                {

                    var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                    var db = new SQLiteConnection(documentPath);
                    db.CreateTable<Estudiante>();
                    IEnumerable<Estudiante> resultado = SELECT_WHERE(db, txtUsuario.Text, txtContrasenia.Text);
                    if (resultado.Count() > 0)
                    {
                        Navigation.PushAsync(new ConsultaRegistro());
                    }
                    else
                    {
                        DisplayAlert("Alerta", "Usuario no existe, por favor Registrarse", "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "ok");
            }
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registro());
        }
    }
}