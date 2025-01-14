using NAudio.Wave;

namespace ListaDoble
{
    public class Nodo
    {
        public string nota; 
        public string figura; 
        public Nodo next; 
        public Nodo prev; 

        // Constructor del nodo
        public Nodo(string nota, string figura)
        {
            this.nota = nota;
            this.figura = figura;
            this.next = null;
            this.prev = null;
        }
    }

    public class ListaDoble
    {
        private Nodo head; // Cabeza de la lista

        // notas y figuras válidas
        private readonly HashSet<string> notasValidas = new HashSet<string> { "do", "re", "mi", "fa", "sol", "la", "si" };
        private readonly HashSet<string> figurasValidas = new HashSet<string> { "redonda", "blanca", "negra", "corchea", "semicorchea" };

        // Diccionario para almacenar las duraciones de cada figura rítmica
        private readonly Dictionary<string, double> duracionesFiguras = new Dictionary<string, double>();

        public ListaDoble()
        {
            this.head = null;
        }

        // Método para agregar partituras a la lista a partir de una entrada en formato (nota, figura)
        public bool AddPartituras(string entrada)
        {
            // Separar las partituras por "), ("
            string[] partituras = entrada.Split(new[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var partitura in partituras)
            {
                // Limpia los paréntesis y espacios alrededor
                string limpia = partitura.Trim('(', ')').Trim();
                var partes = limpia.Split(',');

                // Valida la nota, la figura y si estan ambas
                if (partes.Length != 2 || !notasValidas.Contains(partes[0].Trim()) || !figurasValidas.Contains(partes[1].Trim()))
                {
                    Console.WriteLine($"Partitura inválida: ({limpia}). Use el formato (nota, figura).");
                    return false;
                }

                string nota = partes[0].Trim();
                string figura = partes[1].Trim();

                // Crear un nuevo nodo
                Nodo newNode = new Nodo(nota, figura);

                // Insertar el nodo en la lista
                if (this.head == null)
                {
                    this.head = newNode;
                }
                else
                {
                    Nodo current = this.head;
                    while (current.next != null)
                    {
                        current = current.next;
                    }
                    current.next = newNode;
                    newNode.prev = current;
                }
            }

            return true;
        }

        // Método de duraciones de las figuras
        public void DefinirDuraciones(double duracionNegra)
        {
            // Rango permitido
            if (duracionNegra < 0.1 || duracionNegra > 5.0)
            {
                Console.WriteLine("Entrada invalida o fuera de rango");
                return;
            }

            // Ajuste de valores de acuerdo a la negra
            duracionesFiguras["redonda"] = duracionNegra * 4;
            duracionesFiguras["blanca"] = duracionNegra * 2;
            duracionesFiguras["negra"] = duracionNegra;
            duracionesFiguras["corchea"] = duracionNegra / 2;
            duracionesFiguras["semicorchea"] = duracionNegra / 4;

        }

        // Método para reproducir las notas de la lista
        public void ReproducirCancion()
        {
            if (head == null)
            {
                Console.WriteLine("La lista esta vacia.");
                return;
            }

            Nodo current = head;
            while (current != null)
            {
                // Obtener duración y frecuencia de la nota actual
                double duracion = duracionesFiguras[current.figura];
                double frecuencia = ObtenerFrecuencia(current.nota);

                // Reproducir la nota
                ReproducirNota(frecuencia, duracion);

                // Avanzar al siguiente nodo
                current = current.next;
            }

        }

        // Método para obtener la frecuencia en Hz de una nota
        private double ObtenerFrecuencia(string nota)
        {
            return nota switch
            {
                "do" => 261.63,
                "re" => 293.66,
                "mi" => 329.63,
                "fa" => 349.23,
                "sol" => 392.00,
                "la" => 440.00,
                "si" => 493.88,
                _ => 0.0,
            };
        }

        // Método para reproducir una nota con una frecuencia y duración específicas
        private void ReproducirNota(double frecuencia, double duracion)
        {
            var waveProvider = new SineWaveProvider(frecuencia, 0.5f);
            using (var waveOut = new WaveOutEvent())
            {
                waveOut.Init(waveProvider);
                waveOut.Play();
                Thread.Sleep((int)(duracion * 1000));
                waveOut.Stop();
            }
        }
    }

    // Clase para generar una onda sinusoidal
    public class SineWaveProvider : WaveProvider32
    {
        private readonly double frequency; // Frecuencia de la onda
        private readonly float amplitude; // Amplitud de la onda
        private double phaseAngle; // Ángulo de fase actual

        // Constructor de la clase
        public SineWaveProvider(double frequency, float amplitude)
        {
            this.frequency = frequency;
            this.amplitude = amplitude;
        }

        // Método para generar los datos de audio de la onda sinusoidal
        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            int sampleRate = WaveFormat.SampleRate;
            for (int i = 0; i < sampleCount; i++)
            {
                buffer[offset + i] = amplitude * (float)Math.Sin(phaseAngle);
                phaseAngle += 2 * Math.PI * frequency / sampleRate;
                if (phaseAngle > 2 * Math.PI)
                    phaseAngle -= 2 * Math.PI;
            }
            return sampleCount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaDoble lista = new ListaDoble();

            // Solicitar al usuario que ingrese las partituras
            Console.WriteLine("Ingrese las partituras en el formato (nota, figura), (nota, figura), ...:");
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                lista.AddPartituras(input);
            }

            // SSolicita al usuario la entrada del tiempo de la negra
            Console.WriteLine("Defina la duración de la figura negra (0.1s a 5s):");
            if (double.TryParse(Console.ReadLine(), out double duracionNegra))
            {
                lista.DefinirDuraciones(duracionNegra);

                // Reproducir la canción
                lista.ReproducirCancion();
            }
            else
            {
                Console.WriteLine("Entrada inválida para la duración.");
            }
        }
    }
}
