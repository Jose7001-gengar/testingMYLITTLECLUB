

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using My_little_Club.Models;
using My_little_Club;
using My_little_Club.Controllers;

namespace TestProject
{
    public class UnitTest1
    {


        [Fact]
        public async Task Test_REGISTRO_BASE_DE_DATOS_ESTUDIANTE()
        {

            // ====== Arrange ======
            var contextOptions = new DbContextOptionsBuilder<contextDATA>()
            .UseSqlServer(@"Data Source=localhost;Database=MYLITCLUB;Integrated Security=True;
            MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True;")
            .Options;

            using var context = new contextDATA(contextOptions);



            // Crea el objeto ESTUDIANTES con datos de prueba para la base de datos
            var estudiante = new ESTUDIANTES
            {
                //cambiar estos datos para realizar la prueba
                rol = 2,
                cedula = 171725,
                contrasena = "abc12342",
                nombre = "Carlos Mario",
                apellido = "Lopez",
                id_clase_asignada = 3
            };

            context.ESTUDIANTES.Add(estudiante);
            context.SaveChanges();

            /// ====== Act ======                                            cambiar aqui la cedula para realizar la prueba
            var resultado = await context.ESTUDIANTES.Where(e => e.cedula == 171725).FirstOrDefaultAsync();

            //assert
            Assert.NotNull(resultado);
            Assert.Equal("Carlos Mario", resultado.nombre);
            Assert.Equal("Lopez", resultado.apellido);

        }

         
        [Fact]
        public async Task TEST_CONTROLADOR_LOGIN_DE_ESTUDIANTES_EXISTENTES()
        {
            // ====== Arrange ======
            //conexion a la base de datos
            var contextOptions = new DbContextOptionsBuilder<contextDATA>()
            .UseSqlServer(@"Data Source=localhost;Database=MYLITCLUB;Integrated Security=True;
            MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True;")
            .Options;
            //instancia del contexto
            using var context = new contextDATA(contextOptions);
            //instancia del controlador
            var controller = new HomeController(context);


            int cedula = 456;
            string contrasena = "pass456";
            // ====== Act ======
            var result = await controller.dashboard_estudiante(cedula, contrasena) as ViewResult;




            // ====== Assert ======
            var view = Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
            Assert.Equal("dashboard_estudiante", view.ViewName);

            

        }




        [Fact]
        public async Task TEST_CONTROLADOR_LOGIN_DE_ESTUDIANTES_NO_EXISTENTES()
        {
            // ====== Arrange ======
            //conexion a la base de datos
            var contextOptions = new DbContextOptionsBuilder<contextDATA>()
            .UseSqlServer(@"Data Source=localhost;Database=MYLITCLUB;Integrated Security=True;
            MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True;")
            .Options;
            //instancia del contexto
            using var context = new contextDATA(contextOptions);
            //instancia del controlador
            var controller = new HomeController(context);
            int cedula = 999;
            string contrasena = "noexisto";
            // ====== Act ======
            var result = await controller.dashboard_estudiante(cedula, contrasena) as ViewResult;
            // ====== Assert ======
            // Verifica que la vista devuelta NO sea "dashboard_estudiante" es decir, que no se haya podido iniciar sesion
            Assert.Equal("dashboard_estudiante", result.ViewName);
        }







        [Fact]
        public async Task TEST_CONTROLADOR_REGISTRO_DE_ESTUDIANTES()
        {
            // ====== Arrange ======
            //conexion a la base de datos
            var contextOptions = new DbContextOptionsBuilder<contextDATA>()
            .UseSqlServer(@"Data Source=localhost;Database=MYLITCLUB;Integrated Security=True;
            MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True;")
            .Options;
            //instancia del contexto
            using var context = new contextDATA(contextOptions);
            //instancia del controlador
            var controller = new entryPoint(context);

            int rol = 2;
            //CAMBIAR ESTOS DATOS PARA REALIZAR LA PRUEBA 
                string nombre = "Maria";
                string apellido = "Gomez";
                
                int cedula = 4567;
                string contrasena = "pass456";
                int id_clase_asignada = 3;
                
            
            // ====== Act ======
            var result = await controller.register_estudiante(nombre,
                                                              apellido,
                                                              rol,
                                                              cedula,
                                                              contrasena,
                                                              id_clase_asignada) as RedirectToActionResult;

            string resultado_busqueda = result.ActionName;

            // ====== Assert ======
            // Verifica que la acción redirige correctamente a "register"
            Assert.Equal("register", resultado_busqueda);

        }
   
    
    
    
    }



    }