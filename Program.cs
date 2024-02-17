


using System;
using System.Data.Common;
using System.Net.Quic;
public class Paciente
{
    public string Nombre {  get; set; }
    public string Telefono { get; set; }
    public string Sangre { get; set; }
    public string Direccion {  get; set; }
    public DateTime FechaNacimiento { get; set; }
    public List<Tratamiento> Tratamientos { get; set; }
    public Paciente (string nombre, string telefono, string tipoSangre, string direccion, DateTime fechaNacimiento )
    {
        Nombre = nombre;
        Telefono = telefono;
        Sangre = tipoSangre;
        Direccion = direccion;
        FechaNacimiento = fechaNacimiento;
        Tratamientos = new List<Tratamiento> ();

    }

    public void AsignarTratamiento(Tratamiento tratamiento)
    {
        Tratamientos.Add (tratamiento);
    }

}
public class Medicamento
{
    public int Codigo {  get; set; }
    public string Nombre { get; set; }
    public int Cantidad {  get; set; }
    
    public Medicamento (int codigo, string nombre, int cantidad )
    {
        Codigo = codigo;
        Nombre = nombre;
        Cantidad = cantidad;
    }
    public void ReducirInventario(int cantidad)
    {
        Cantidad -= cantidad;
    }   
}
public class Tratamiento
{
    public Medicamento Medicamento { get; set; }
    public int Cantidad { get; set; }

    public Tratamiento(Medicamento medicamento, int cantidad)
    {
        Medicamento = medicamento;
        Cantidad = cantidad;
    }
}

class Program
{
    static List<Paciente> pacientes = new List<Paciente>();
    static List<Medicamento> catalogoMedicamentos = new List<Medicamento>();
   

    static void Main()
    {
        int opcion;
        do
        {

            Console.WriteLine("Menu principal");
            Console.WriteLine("1. Agregar Paciente");
            Console.WriteLine("2. Agregar medicamento al catalogo");
            Console.WriteLine("3. Asignar tratamiento medico a un paciente");
            Console.WriteLine("4. Realizar Consultas");
            Console.WriteLine("5. Salir");
            //int opcion;
            if (int.TryParse(Console.ReadLine(), out opcion))
            {
                switch (opcion)
                {

                    case 1:
                        AgregarPaciente();
                        break;
                    case 2:
                        AgregarMedicamento();
                        break;
                    case 3:
                        AsignarTratamiento();
                        break;
                    case 4:
                        Consultas();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del Programa...¨" +
                                          "Muchas Gracias...");
                        break;
                    default:
                        Console.WriteLine("Opcion Invalida, Por favor seleccione una de las opciones del menu");
                        break;

                }
            }
            else // en caso de que se ingrese una letra en las opciones;
            {
                Console.WriteLine("Digito un numero invalido, Por favor seleccione un numero dentro de la lista");
            }
        }
        while (opcion != 5);
    }

    static void AgregarPaciente()
    {
        Console.WriteLine("Agregar Paciente");
        Console.WriteLine("Ingrese el nombre completo del paciente");
        String nombre = Console.ReadLine();
        Console.WriteLine("Ingrese el Numero de Telefono");
        string telefono = (Console.ReadLine());
        Console.WriteLine("ingrese el tipo de Sangre");
        string sangre = Console.ReadLine();
        Console.WriteLine("ingrese la direccion del domicilio");
        String direccion = Console.ReadLine();
        Console.WriteLine("Ingrese la fecha de nacimiento del paciente en formato (YYYY/MM/DD): ");
        DateTime fechaNacimeinto = DateTime.Parse(Console.ReadLine());

        Paciente paciente = new Paciente(nombre, telefono, sangre, direccion, fechaNacimeinto);
        pacientes.Add(paciente);


        Console.WriteLine("Paciente ingresado correctamente. ");

    }

    static void AgregarMedicamento()
    {
        Console.WriteLine("Agregar Medicamentos");
        Console.WriteLine("Ingrese el Codigo del medicamento:");
        int codigoMedicamento = int.Parse(Console.ReadLine());
        Console.WriteLine("Ingrese el Nombre del Medicamento:");
        string nombreMedicamento = Console.ReadLine();
        Console.WriteLine("Ingrese la Cantidad");
        int cantidad = int.Parse(Console.ReadLine());

        Medicamento medicamento = new Medicamento(codigoMedicamento, nombreMedicamento, cantidad);
        catalogoMedicamentos.Add(medicamento);



        Console.WriteLine("Medicamento Ingresado al catalogo Correctamente ");
    }

    static void AsignarTratamiento()
    {
        if (pacientes.Count == 0 || catalogoMedicamentos.Count == 0)
        {
            Console.WriteLine("no hay pacientes o el catalogo de medicamentos se encuentra vacio");
            return;

        }
        Console.WriteLine("Asignar un tratamiento al paciente");
        MostrarPacientes();
        Console.WriteLine("Selecione el numero de paciente");
        int indicePaciente = Convert.ToInt32(Console.ReadLine()) - 1;

        if (indicePaciente < 0 || indicePaciente >= pacientes.Count)
        {
            Console.WriteLine("Numero de paciente invalido. ");
            return;
        }

        Paciente paciente = pacientes[indicePaciente];
        Console.WriteLine("Catalogo de medicamentos: ");
        MostarMedicamentos();
        Console.Write("Ingrese el codigo del medicamento a asignar: ");
        int codigoMedicamento = Convert.ToInt32(Console.ReadLine());

        Medicamento medicamento = catalogoMedicamentos.FirstOrDefault(m => m.Codigo == codigoMedicamento);

        if (medicamento == null)
        {
            Console.WriteLine("El codigo del medicamento es invalido");
            return;
        }

        Console.WriteLine("Ingrese la cantidad que desea asignar: ");
        int cantidadAsignar = Convert.ToInt32(Console.ReadLine());

        if (cantidadAsignar <= 0 || cantidadAsignar > 3)
        {
            Console.WriteLine("La cantidad maxima asignar es de 3");
            return;
        }

        if (cantidadAsignar == medicamento.Cantidad)
        {
            Console.WriteLine("cantidad no disponible");
            return;
        }
        paciente.AsignarTratamiento(new Tratamiento(medicamento, cantidadAsignar));
        medicamento.ReducirInventario(cantidadAsignar);

        Console.WriteLine("Tratamiento Asignado Correctamente");

    }

    static void Consultas()
    {
        Console.WriteLine("Consultas");
        Console.Write("Seleccione una opción de consulta: ");
        Console.WriteLine("1- Cantidad total de pacientes registrados");
        Console.WriteLine("2- Reporte de medicamentos recetados");
        Console.WriteLine("3- Reporte cantidad de pacientes por edades");
        Console.WriteLine("4- Reporte Pacientes y consultas ordenado por nombre");
        int opcionConsulta = Convert.ToInt32(Console.ReadLine());

        switch (opcionConsulta)
        {
            case 1:
                Console.WriteLine("Cantidad total de pacientes Registrados: ");
                break;
            case 2:
                Console.WriteLine("Reporte de medicamentos recetados: ");
                MostrarMedicamentosRecetados();
                break;
            case 3:
                Console.WriteLine("Reporte cantidad de pacientes por edades: ");
                MostarCantidadPacientesPorEdad();
                break;
            case 4:
                Console.WriteLine("Reporte de pacientes y consultas ordenado por nombre");
                MostarPacientesOrdenadosPorNombre();
                break;
            default:
                Console.WriteLine("Opcion invalidad, seleccione una opcion del menu");
                break;

        }

    }

    static void MostrarPacientes()
    {
        Console.WriteLine("Pacientes: ");
        for (int i = 0; i < pacientes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pacientes[i].Nombre}");
        }
    }

    static void MostarMedicamentos()
    {
        foreach (var medicamento in catalogoMedicamentos)
        {
            Console.WriteLine($"{medicamento.Codigo}. {medicamento.Nombre} - Cantidad: {medicamento.Cantidad}");
        }
    }

    static void MostrarMedicamentosRecetados()
    {
        List<String> medicamentosRecetados = new List<String>();
        foreach (var paciente in pacientes)
        {
            foreach (var tratamiento in paciente.Tratamientos)
            {
                medicamentosRecetados.Add(tratamiento.Medicamento.Nombre);
            }
        }

        foreach (var medicamento in medicamentosRecetados)
        {
            Console.WriteLine(medicamento);
        }
    }

    static void MostarCantidadPacientesPorEdad()
    {
        int[] edades = { 0, 0, 0, 0, };

        foreach (var paciente in pacientes)
        {

            int edad = CalcularEdad(paciente.FechaNacimiento);

            if (edad <= 10)
                edades[0]++;
            else if (edad <= 30)
                edades[1]++;
            else if (edad <= 50)
                edades[2]++;
            else
                edades[3]++;

        }

        Console.WriteLine($"De 0 a 10 años: {edades[0]} pacientes");
        Console.WriteLine($"De 11 a 30 años: {edades[1]} pacientes");
        Console.WriteLine($"De 31 a 50 años: {edades[2]} pacientes");
        Console.WriteLine($"Mayores de 51 años: {edades[3]} pacientes");

    }

    static void MostarPacientesOrdenadosPorNombre()
    {
        var pacientesOrdenados = pacientes.OrderBy(p => p.Nombre);

        foreach (var paciente in pacientesOrdenados)
        {
            Console.WriteLine($"Nombre: {paciente.Nombre}, Telefono: {paciente.Telefono} ");

        }
    }

    static int CalcularEdad(DateTime fechaNacimiento)
    {
        DateTime ahora = DateTime.Now;
        int edad = ahora.Year - fechaNacimiento.Year;

        if (ahora.Month < fechaNacimiento.Month || (ahora.Month == fechaNacimiento.Month && ahora.Day < fechaNacimiento.Day ))
        {
            edad--;
        }
        return edad;
    }

}