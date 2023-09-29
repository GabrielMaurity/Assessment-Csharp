using System.Globalization;
using CarBusiness;
using CarLibrary;
using OperacoesCarro;

class Program
{
    static List<Carro> carros = new List<Carro>();
    static string arquivoCarros = "carros.txt";

    static void Main(string[] args)
    {

        IMRepository repCarro = new RepCarroFile();
        CarOperation operacoes = new CarOperation(repCarro);
        //operacoes.Adicionar();
        repCarro = new RepCarroLista();
        operacoes = new CarOperation(repCarro);
        //operacoes.Adicionar();



        CarregarCarros();

        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("1 - Carregar dados de um arquivo");
        Console.WriteLine("2 - Iniciar com uma lista vazia");
        Console.Write("Opção: ");

        int opcao;
        if (int.TryParse(Console.ReadLine(), out opcao))
        {
            switch (opcao)
            {
                case 1:
                    CarregarCarros();
                    break;
                case 2:
                    // Iniciar com uma lista vazia (não faz nada aqui)
                    break;
                default:
                    Console.WriteLine("Opção inválida. Iniciando com uma lista vazia.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Iniciando com uma lista vazia.");
        }

        ExibirUltimos5Carros();

        bool sair = false;
        while (!sair)

        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1 - Adicionar Carro");
            Console.WriteLine("2 - Listar Carros");
            Console.WriteLine("3 - Atualizar Carro");
            Console.WriteLine("4 - Excluir Carro");
            Console.WriteLine("5 - Sair");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        AdicionarCarro();
                        break;
                    case 2:
                        ListarCarros();
                        break;
                    case 3:
                        AtualizarCarro();
                        break;
                    case 4:
                        ExcluirCarro();
                        break;
                    case 5:
                        sair = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }

            Console.WriteLine();
        }


        SalvarCarros();
    }

    static void CarregarCarros()
    {
        if (File.Exists(arquivoCarros))
        {
            using (StreamReader sr = new StreamReader(arquivoCarros))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] partes = linha.Split(',');
                    if (partes.Length == 5)
                    {
                        Guid id = Guid.Parse(partes[0]);
                        string marca = partes[1];
                        string modelo = partes[2];
                        DateTime ano = DateTime.ParseExact(partes[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        double preco = double.Parse(partes[4]);

                        carros.Add(new Carro { Id = id, Marca = marca, Modelo = modelo, Ano = ano, Preco = preco });
                    }
                }
            }
        }
    }

    static void SalvarCarros()
    {
        using (StreamWriter sw = new StreamWriter(arquivoCarros))
        {
            foreach (var carro in carros)
            {
                sw.WriteLine($"{carro.Id},{carro.Marca},{carro.Modelo},{carro.Ano:yyyy-MM-dd},{carro.Preco}");
            }
        }
    }

    static void AdicionarCarro()
    {
        Carro carro = new Carro();
        carro.Id = Guid.NewGuid();

        Console.Write("Marca: ");
        carro.Marca = Console.ReadLine();
        Console.Write("Modelo: ");
        carro.Modelo = Console.ReadLine();
        Console.Write("Ano (yyyy-MM-dd): ");
        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ano))
        {
            carro.Ano = ano;
        }
        else
        {
            Console.WriteLine("Ano inválido. O carro não foi adicionado.");
            return;
        }
        Console.Write("Preço: ");
        if (double.TryParse(Console.ReadLine(), out double preco))
        {
            carro.Preco = preco;
        }
        else
        {
            Console.WriteLine("Preço inválido. O carro não foi adicionado.");
            return;
        }

        Console.Write("O carro possui 4 portas? (S/N): ");
        string resposta = Console.ReadLine();
        carro.Possui4Portas = (resposta.ToUpper() == "S");
        carros.Add(carro);
        Console.WriteLine("Carro adicionado com sucesso!");
    }

    static void ListarCarros()
    {
        if (carros.Count == 0)
        {
            Console.WriteLine("Nenhum carro cadastrado.");
        }
        else
        {
            Console.WriteLine("Lista de Carros:");
            foreach (var carro in carros)
            {
                string possui4Portas = carro.Possui4Portas ? "Sim" : "Não";
                Console.WriteLine($"ID: {carro.Id}, Marca: {carro.Marca}, Modelo: {carro.Modelo}, Ano: {carro.Ano:yyyy-MM-dd}, Preço: {carro.Preco}, 4 Portas: {possui4Portas}");
            }
        }
    }


    static void AtualizarCarro()
    {
        Console.Write("Digite o ID do carro que deseja atualizar: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Carro carroExistente = carros.Find(c => c.Id == id);
            if (carroExistente != null)
            {
                Console.Write("Nova Marca: ");
                carroExistente.Marca = Console.ReadLine();
                Console.Write("Novo Modelo: ");
                carroExistente.Modelo = Console.ReadLine();
                Console.Write("Novo Ano (yyyy-MM-dd): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime novoAno))
                {
                    carroExistente.Ano = novoAno;
                }
                else
                {
                    Console.WriteLine("Ano inválido. O carro não foi atualizado.");
                    return;
                }
                Console.Write("Novo Preço: ");
                if (double.TryParse(Console.ReadLine(), out double novoPreco))
                {
                    carroExistente.Preco = novoPreco;
                }
                else
                {
                    Console.WriteLine("Preço inválido. O carro não foi atualizado.");
                    return;
                }
                Console.WriteLine("Carro atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Carro não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido. Tente novamente.");
        }
    }

    static void ExcluirCarro()
    {
        Console.Write("Digite o ID do carro que deseja excluir: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Carro carroExistente = carros.Find(c => c.Id == id);
            if (carroExistente != null)
            {
                carros.Remove(carroExistente);
                Console.WriteLine("Carro excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Carro não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido. Tente novamente.");
        }
    }
    static void ExibirUltimos5Carros()
    {

        SalvarCarros();
        int numeroCarrosExibidos = Math.Min(2, carros.Count);

        Console.WriteLine("Últimos 5 carros salvos:");
        for (int i = carros.Count - numeroCarrosExibidos; i < carros.Count; i++)
        {
            var carro = carros[i];
            string possui4Portas = carro.Possui4Portas ? "Sim" : "Não";
            Console.WriteLine($"ID: {carro.Id}, Marca: {carro.Marca}, Modelo: {carro.Modelo}, Ano: {carro.Ano:yyyy-MM-dd}, Preço: {carro.Preco}, 4 Portas: {possui4Portas}");
        }
        Console.WriteLine();
    }
}








