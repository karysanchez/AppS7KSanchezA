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
    public partial class Elemento : ContentPage
    {
        public int idSeleccionado;
        private SQLiteAsyncConnection con;
        IEnumerable<Estudiante> ResultadoDelete;
        IEnumerable<Estudiante> ResultadoUpdate;


        public Elemento(int Id)
        {
            InitializeComponent();
            con = DependencyService.Get<DataBase>().GetConnection();
            idSeleccionado = Id;

        }


        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante where Id = ?", id);
        }

        public static IEnumerable<Estudiante> Update(SQLiteConnection db, string nombre, string usuario, string contrasenia, int id)
        {
            return db.Query<Estudiante>("UPDATE Estudiante SET Nombre = ?, Usuario = ?, " +
                "Contrasenia = ? where Id = ?", nombre, usuario, contrasenia, id);
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
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

                    var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                    var db = new SQLiteConnection(databasePath);
                    ResultadoUpdate = Update(db, txtNombre.Text, txtUsuario.Text, txtContrasenia.Text, idSeleccionado);
                    DisplayAlert("Alerta", "Actualización correctamete", "ok");
                    inicializar();
                    regresar();

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "ERROR " + ex.Message, "ok");
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

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                ResultadoDelete = Delete(db, idSeleccionado);
                DisplayAlert("Alerta", "Registro eliminado correctamete", "ok");
                regresar();
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "ERROR " + ex.Message, "ok");
            }
        }
    }
}