using System.Runtime.CompilerServices;
using System.Xml;
using System.Text;

class Pelicula
{
    // Variables de las diferentes rutas
    private static string rutaCsv = "";
    private static string rutaXml = "";

    //Variables de las peliculas
    private string title;
    private string genre;
    private string premiere;
    private int runtime;
    private double imdbScore;
    private string language;
    public Pelicula(string pTitle, string pGenre, string pPremiere, int pRuntime, double pImdbScore, string pLanguage)
    {
        this.title = pTitle;
        this.genre = pGenre;
        this.premiere = pPremiere;
        this.runtime = pRuntime;
        this.imdbScore = pImdbScore;
        this.language = pLanguage;
    }
    //Getters y setters
    public string TITLE
    {
        get { return this.title; }
        set { this.title = value; }
    }

    public string GENRE
    {
        get { return this.genre; }
        set { this.genre = value; }
    }

    public string PREMIERE
    {
        get { return this.premiere; }
        set { this.premiere = value; }
    }

    public int RUNTIME
    {
        get { return this.runtime; }
        set { this.runtime = value; }
    }

    public double IMDBSCORE
    {
        get { return this.imdbScore; }
        set { this.imdbScore = value; }
    }

    public string LANGUAGE
    {
        get { return this.language; }
        set { this.language = value; }
    }
    public static string RUTACSV 
    {
        get { return rutaCsv; }
        set { rutaCsv = value; }
    }
    public static string RUTAXML 
    {
        get { return rutaXml; }
        set { rutaXml = value; }
    }

    public void ComprobarRutaXml()
    {
        try
        {
            Directory.CreateDirectory("XMLs");
            if (string.IsNullOrEmpty(rutaXml))
            {
                Console.WriteLine("Introduce la ruta o el nombre del archivo XML: ");
                string archivo = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(archivo)) // Si no se introduce nada le pone nombre por defecto al archivo
                {
                    rutaXml = Path.Combine("XMLs", "NetflixOriginals.xml");                    
                    Console.WriteLine("No se introdujo nada asique se usará un nombre por defecto (NetflixOriginals.xml)");
                }
                else
                {
                    if (archivo.Contains("XMLs") == false && archivo.Contains("/") == false && archivo.Contains("\\") == false)
                    {
                    rutaXml = Path.Combine("XMLs", archivo);
                    }
                    else
                    {
                        rutaXml = archivo;
                    }
                }
                Thread.Sleep(1200);
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al configurar la ruta del XML {ex.Message}");
        }
    }
    public void ComprobarRutaCsv()
    {
        try
        {
            Directory.CreateDirectory("CSVs");
            if (string.IsNullOrEmpty(rutaCsv))
            {
                Console.WriteLine("Introduce la ruta o el nombre del archivo CSV: ");
                string archivo = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(archivo)) // Si no se introduce nada le pone nombre por defecto al archivo
                {
                    rutaCsv = Path.Combine("CSVs", "NetflixOriginals.csv");                    
                    Console.WriteLine("No se introdujo nada asique se usará un nombre por defecto (NetflixOriginals.csv)");
                }
                else
                {
                    if (archivo.Contains("CSVs") == false && archivo.Contains("/") == false && archivo.Contains("\\") == false)
                    {
                    rutaCsv = Path.Combine("CSVs", archivo);
                    }
                    else
                    {
                        rutaCsv = archivo;
                    }                
                }
                Thread.Sleep(1200);
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al configurar la ruta del CSV {ex.Message}");
        }
    }

    public List<Pelicula> LeerCsv(string ruta)
    {
        if (string.IsNullOrEmpty(ruta))
        {
            ComprobarRutaCsv();
            ruta = RUTACSV;
        }
        List<Pelicula> pelis = new List<Pelicula>();
        try
        {
            string[] lineas = File.ReadAllLines(ruta);
            bool primeraLinea = true;
            foreach (string linea in lineas)
            {
                if (primeraLinea)
                {
                    primeraLinea = false;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(linea))
                {
                    continue;
                }

                // carácter a carácter para ignorar comas dentro de comillas dobles
                List<string> camposList = new List<string>();
                bool enComillas = false;
                StringBuilder campoActual = new StringBuilder();
                int indice = 0;
                while (indice < linea.Length)
                {
                    char caracter = linea[indice];
                    if (caracter == '"')
                    {
                        if (enComillas == false)
                        {
                            enComillas = true;
                        }
                        else
                        {
                            enComillas = false;
                        }
                    }
                    else
                    {
                        if (caracter == ',' && enComillas == false)
                        {
                            camposList.Add(campoActual.ToString());
                            campoActual.Clear();
                        }
                        else
                        {
                            campoActual.Append(caracter);
                        }
                    }
                    indice++;
                }
                camposList.Add(campoActual.ToString());

                // Pasamos la lista a array para mantener la estructura de índices creada 
                string[] campos = camposList.ToArray();

                int numCampos = 0;
                foreach (string c in campos)
                {
                    numCampos++;
                }

                if (numCampos >= 6)
                {
                    string titulo = campos[0].Replace("\"", "").Trim();
                    string genero = campos[1].Replace("\"", "").Trim();
                    string premiere = campos[2].Replace("\"", "").Trim();
                    int runtime = int.Parse(campos[3].Replace("\"", "").Trim());
                    double imdb = double.Parse(campos[4].Replace("\"", "").Trim(), System.Globalization.CultureInfo.InvariantCulture);
                    string idioma = campos[5].Replace("\"", "").Trim();

                    Pelicula peli = new Pelicula(titulo, genero, premiere, runtime, imdb, idioma);
                    pelis.Add(peli);
                }
            }
            Console.WriteLine("El fichero CSV se leyó correctamente.");
            Thread.Sleep(1000);
        } 
        catch (FileNotFoundException)
        {
            Console.WriteLine($"No se encuentra el archivo de la ruta. Comprueba que está en la carpeta del proyecto.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al leer el CSV: {ex.Message}");
        }
        return pelis;
    }
    public List<Pelicula> LeerXml(string ruta)
    {
        if (string.IsNullOrEmpty(ruta))
        {
            ComprobarRutaXml();
            ruta = RUTAXML;
        }
        List<Pelicula> pelis = new List<Pelicula>();
        try
        {
            XmlReader lectura = XmlReader.Create(ruta);
            string titulo = "";
            string genero = "";
            string premiere = "";
            int runtime = 0;
            double imdbScore = 0;
            string idioma = "";
        
            while (lectura.Read())
            {
                if (lectura.NodeType == XmlNodeType.Element){
                    if (lectura.Name == "Title")
                    {
                        lectura.Read();
                        titulo = lectura.Value;
                    }
                    else if (lectura.Name == "Genre")
                    {
                        lectura.Read();
                        genero = lectura.Value;
                    }
                    else if (lectura.Name == "Premiere")
                    {
                        lectura.Read();
                        premiere = lectura.Value;
                    }
                    else if (lectura.Name == "Runtime")
                    {
                        lectura.Read();
                        runtime = int.Parse(lectura.Value);
                    }
                    else if (lectura.Name == "IMDB_Score")
                    {
                        lectura.Read();
                        imdbScore = double.Parse(lectura.Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else if (lectura.Name == "Language")
                    {
                        lectura.Read();
                        idioma = lectura.Value;
                    }
                }
                else if (lectura.NodeType == XmlNodeType.EndElement)
                {
                    if (lectura.Name == "Pelicula")
                    {
                        Pelicula peli = new Pelicula(titulo, genero, premiere, runtime, imdbScore, idioma);
                        pelis.Add(peli);
                    }
                }
            }
            lectura.Close();
            Console.WriteLine("El archivo XML se leyó correctamente.");
            Thread.Sleep(1000);
        } catch (FileNotFoundException)
        {
            Console.WriteLine($"No se encuentra el archivo de la ruta. Comprueba que está en la carpeta del proyecto.");
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al leer el XML: {ex.Message}");
        }
        return pelis;
    }

    public void EscribirXmlConLista(List <Pelicula> pelis)
    {
        rutaXml = "";
        ComprobarRutaXml();
        XmlWriter xmlWriter = XmlWriter.Create(RUTAXML);
        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("Peliculas");

        foreach (Pelicula p in pelis)
        {
            xmlWriter.WriteStartElement("Pelicula");

            xmlWriter.WriteStartElement("Title");
            xmlWriter.WriteString(p.TITLE);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Genre");
            xmlWriter.WriteString(p.GENRE);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Premiere");
            xmlWriter.WriteString(p.PREMIERE);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Runtime");
            xmlWriter.WriteString(p.RUNTIME.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("IMDB_Score");
            xmlWriter.WriteString(p.IMDBSCORE.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Language");
            xmlWriter.WriteString(p.LANGUAGE);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
    }
    public void EscribirCsvConLista(List<Pelicula> pelis)
    {
        try
        {
            rutaCsv = "";
            ComprobarRutaCsv();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Title,Genre,Premiere,Runtime,IMDB_Score,Language");
            foreach (Pelicula p in pelis)
            {
                //Para que el csv que se crea no este descuadrado
                string linea = $"\"{p.TITLE}\",\"{p.GENRE}\",\"{p.PREMIERE}\",{p.RUNTIME},{p.IMDBSCORE.ToString(System.Globalization.CultureInfo.InvariantCulture)},\"{p.LANGUAGE}\"";                sb.AppendLine(linea);
            }

            File.WriteAllText(RUTACSV, sb.ToString());
            Console.WriteLine("Archivo CSV creado correctamente en: " + RUTACSV);
            Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hubo un fallo al escribir el CSV: " + ex.Message);
        }
    }
    public void ExportarTodoElXml()
    {
        try
        {
            rutaCsv="";
            ComprobarRutaCsv();
            List<Pelicula> pelis = LeerCsv(RUTACSV);
            bool hayPeliculas = false;
            foreach (Pelicula p in pelis)
            {
                hayPeliculas = true;
                break;
            }
            if (hayPeliculas == true)
            {
                EscribirXmlConLista(pelis);
                Console.WriteLine("Archivo XML creado correctamente en: " + RUTAXML);
                Thread.Sleep(1000);

            } else
            {
                Console.WriteLine("No hay películas para exportar.");
                Thread.Sleep(1000);

            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar el XML entero: {ex.Message}");
        }
    }
    public void ExportarTodoElCsv()
    {
        try
        {
            rutaXml = "";
            ComprobarRutaXml();
            List<Pelicula> pelis = LeerXml(RUTAXML);
            int cont = 0;
            foreach (Pelicula p in pelis)
            {
                cont++;
            }
            if (cont > 0)
            {
                EscribirCsvConLista(pelis);
                Thread.Sleep(1000);
            } else
            {
                Console.WriteLine("No hay peliculas para exportar.");
                Thread.Sleep(1000);
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar el CSV entero: {ex.Message}");
        }
    }
    public void ExportarPorGeneroXml()
    {
        try
        {
            rutaCsv = "";
            ComprobarRutaXml();
            List<Pelicula> pelis = LeerCsv(RUTACSV);
            int contPelis = 0;
            foreach(Pelicula p in pelis)
            {
                contPelis++;
            }
            if (contPelis > 0)
            {
                List<string> generos = new List<string>();
                foreach(Pelicula p in pelis)
                {
                    bool existe = false;
                    foreach (string genero in generos)
                    {
                        if (genero.Equals(p.GENRE))
                        {
                            existe = true;
                        }
                    }
                    if (existe == false)
                    {
                        generos.Add(p.GENRE);
                    }
                }
                Console.WriteLine("Estos son los géneros a exportar:");
                int i = 1;
                    foreach (string genero in generos)
                    {
                        Console.WriteLine(i + ". " + genero);
                        i++;
                    }
                Console.WriteLine("Escriba el que desea exportar.");
                string eleccionGenero = Console.ReadLine();
                List<Pelicula> pelisFiltradas = new List<Pelicula>();
                foreach (Pelicula p in pelis)
                {
                    if (p.GENRE.ToLower().Equals(eleccionGenero.ToLower()))
                    {
                        pelisFiltradas.Add(p);
                    }
                }
                int contFiltradas = 0;
                foreach (Pelicula p in pelisFiltradas)
                {
                    contFiltradas++;
                }
                if (contFiltradas > 0)
                {
                    EscribirXmlConLista(pelisFiltradas);
                    Console.WriteLine($"Se exportaron {contFiltradas} películas del genero {eleccionGenero}.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna película con ese género.");
                    Thread.Sleep(1000);
                }
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar por género a XML: {ex.Message}");
        } 
    }
    public void ExportarPorGeneroCsv()
    {
        try
        {
            rutaXml = "";
            ComprobarRutaXml();
            List<Pelicula> pelis = LeerXml(RUTAXML);
            int cont = 0;
            foreach (Pelicula p in pelis)
            {
                cont++;
            }
            if (cont > 0)
            {
                List<string> generos = new List<string>();
                foreach (Pelicula p in pelis)
                {
                    bool existe = false;
                    foreach (string genero in generos)
                    {
                        if (genero.Equals(p.GENRE))
                        {
                            existe = true;
                        }
                    }
                        if (existe == false)
                        {
                            generos.Add(p.GENRE);
                        }
                }
                Console.WriteLine("Estos son los géneros disponibles en el XML:");
                int i = 1;
                foreach (string genero in generos)
                { 
                    Console.WriteLine(i + ". " + genero);
                    i++;
                }
                Console.WriteLine("Escriba el género que desea exportar al CSV.");
                string eleccionGenero = Console.ReadLine();
                List<Pelicula> pelisFiltradas = new List<Pelicula>();
                foreach (Pelicula pelicula in pelis)
                {
                    if (pelicula.GENRE.ToLower().Equals(eleccionGenero.ToLower()))
                    {
                        pelisFiltradas.Add(pelicula);
                    }
                }
                int contFiltradas = 0;
                foreach(Pelicula pelicula in pelisFiltradas)
                {
                    contFiltradas++;
                }
                if (contFiltradas > 0)
                {
                    EscribirCsvConLista(pelisFiltradas);
                    Console.WriteLine($"Se exportaron {contFiltradas} películas del género {eleccionGenero} a CSV.");
                    Thread.Sleep(1000);
                } else
                {
                    Console.WriteLine("No se encontró ninguna película con ese género en el XML.");
                    Thread.Sleep(1000);
                    
                }
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar por género a CSV: {ex.Message}");
        }
    }
    public int ConseguirAnio()
    {
        try
        {
            if (string.IsNullOrEmpty(this.premiere) == true)
            {
                return 0;
            }

            // Dividimos por espacios y por comas para limpiar cualquier formato raro que pueda tener nuestro CSv
            string textoLimpio = this.premiere.Replace(",", " ");
            string[] partes = textoLimpio.Split(" ");
        
            foreach (string parte in partes)
            {
                int posibleAnio = 0;
                if (int.TryParse(parte, out posibleAnio) == true)
                {
                    // Compruebo que el año sea lógico porque Netflix salió en 2007 en USA
                    if (posibleAnio >= 2007 && posibleAnio <= 2030)
                    {
                        return posibleAnio;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Hubo un error consiguiendo el año: " + ex.Message);
        }
        return 0;
    }
    public void ExportarPorAnioXml()
    {
        try
        {
            rutaCsv = "";
            ComprobarRutaXml();
            List<Pelicula> pelis = LeerCsv(RUTACSV);
            int contPelis = 0;
            foreach(Pelicula p in pelis)
            {
                contPelis++;
            }
            if (contPelis > 0)
            {
                Console.WriteLine("Introduce el año del que apartir de ese año se exportarán las películas: ");
                int anio = int.Parse(Console.ReadLine());
                List<Pelicula> pelisFiltradas = new List<Pelicula>();
                foreach (Pelicula p in pelis)
                {
                    if (p.ConseguirAnio() >= anio)
                    {
                        pelisFiltradas.Add(p);
                    }
                }
                int contFiltradas = 0;
                foreach (Pelicula p in pelisFiltradas)
                {
                    contFiltradas++;
                }
                if (contFiltradas > 0)
                {
                    EscribirXmlConLista(pelisFiltradas);
                    Console.WriteLine($"Se exportaron {contFiltradas} películas del año {anio} en adelante.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna película con ese año.");
                    Thread.Sleep(1000);
                }
            }
        } catch(Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar por año a XML: {ex.Message}: ");
        }
    }
    public void ExportarPorAnioCsv()
    {
        try
        {
            rutaXml = "";
            ComprobarRutaXml();
            List<Pelicula> pelis = LeerXml(RUTAXML);
            int contPelis = 0;
            foreach (Pelicula p in pelis)
            {
                contPelis++;   
            }
            if (contPelis > 0)
            {
                Console.WriteLine("Introduce el año del que apartir de ese año se exportarán las películas: ");
                int anio = int.Parse(Console.ReadLine());
                List<Pelicula> pelisFiltradas = new List<Pelicula>();
                foreach (Pelicula p in pelis)
                {
                    if (p.ConseguirAnio() >= anio)
                    {
                        pelisFiltradas.Add(p);
                    }
                }
                int contFiltradas = 0;
                foreach (Pelicula p in pelisFiltradas)
                {
                    contFiltradas++;
                }
                if (contFiltradas > 0)
                {
                    EscribirCsvConLista(pelisFiltradas);
                    Console.WriteLine($"Se exportaron {contFiltradas} películas del año {anio} en adelante.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna película con ese año.");
                    Thread.Sleep(1000);
                }            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar por año a CSV: {ex.Message}");
        }
    }
    public void ExportarPorIdiomaXml()
    {
        try
        {
            rutaCsv = "";
            ComprobarRutaCsv();
            List<Pelicula> pelis = LeerCsv(RUTACSV);
            int contPelis = 0;
            foreach(Pelicula p in pelis)
            {
                contPelis++;
            }
            if (contPelis > 0)
            {
                List<string> idiomas = new List<string>();
                foreach(Pelicula p in pelis)
                {
                    bool existe = false;
                    foreach (string idioma in idiomas)
                    {
                        if (idioma.Equals(p.LANGUAGE))
                        {
                            existe = true;
                        }
                    }
                    if (existe == false)
                    {
                        idiomas.Add(p.LANGUAGE);
                    }
                }
                Console.WriteLine("Estos son los idiomas a exportar:");
                int i = 1;
                    foreach (string idioma in idiomas)
                    {
                        Console.WriteLine(i + ". " + idioma);
                        i++;
                    }
                Console.WriteLine("Escriba el que desea exportar.");
                string eleccionIdioma = Console.ReadLine();
                List<Pelicula> pelisFiltradas = new List<Pelicula>();
                foreach (Pelicula p in pelis)
                {
                    if (p.LANGUAGE.ToLower().Equals(eleccionIdioma.ToLower()))
                    {
                        pelisFiltradas.Add(p);
                    }
                }
                int contFiltradas = 0;
                foreach (Pelicula p in pelisFiltradas)
                {
                    contFiltradas++;
                }
                if (contFiltradas > 0)
                {
                    EscribirXmlConLista(pelisFiltradas);
                    Console.WriteLine($"Se exportaron {contFiltradas} películas con idioma {eleccionIdioma}.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna película con ese idioma.");
                    Thread.Sleep(1000);
                }
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar por idioma a XML: {ex.Message}");
        }
    }
    public void ExportarPorIdiomaCsv()
    {
        try
        {
            rutaXml="";
            ComprobarRutaXml();
            List<Pelicula> pelis = LeerXml(RUTAXML);
            int contPelis = 0;
            foreach(Pelicula p in pelis)
            {
                contPelis++;
            }
            if (contPelis > 0)
            {
                List<string> idiomas = new List<string>();
                foreach(Pelicula p in pelis)
                {
                    bool existe = false;
                    foreach (string idioma in idiomas)
                    {
                        if (idioma.Equals(p.LANGUAGE))
                        {
                            existe = true;
                        }
                    }
                    if (existe == false)
                    {
                        idiomas.Add(p.LANGUAGE);
                    }
                }
                Console.WriteLine("Estos son los idiomas a exportar:");
                int i = 1;
                    foreach (string idioma in idiomas)
                    {
                        Console.WriteLine(i + ". " + idioma);
                        i++;
                    }
                Console.WriteLine("Escriba el que desea exportar.");
                string eleccionIdioma = Console.ReadLine();
                List<Pelicula> pelisFiltradas = new List<Pelicula>();
                foreach (Pelicula p in pelis)
                {
                    if (p.LANGUAGE.ToLower().Equals(eleccionIdioma.ToLower()))
                    {
                        pelisFiltradas.Add(p);
                    }
                }
                int contFiltradas = 0;
                foreach (Pelicula p in pelisFiltradas)
                {
                    contFiltradas++;
                }
                if (contFiltradas > 0)
                {
                    EscribirCsvConLista(pelisFiltradas);
                    Console.WriteLine($"Se exportaron {contFiltradas} películas con idioma {eleccionIdioma}.");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna película con ese idioma.");
                    Thread.Sleep(1000);
                }
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Hubo un fallo al exportar por idioma a CSV: {ex.Message}");
        }
    }
}
