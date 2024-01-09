using System.Text.RegularExpressions;
using DesafioFundamentos.Models;
using Repository;

namespace Services
{
    public class VeiculoService
    {
        public static void AdicionarVeiculo()
        {
            Console.Clear();
            Console.Write("Digite a placa do veículo para estacionar: ");
            string placa = Console.ReadLine().ToUpper().Replace("-", "");

            bool placaValidada = ValidarPlaca(placa);

            if (placaValidada)
            {
                if (VeiculoRepositorio.ExisteVeiculoPorPlaca(placa))
                {
                    Console.WriteLine($"Veículo de Placa: {placa}, já está estacionado.");
                    return;
                }

                Veiculo veiculo = new Veiculo()
                {
                    Placa = placa,
                };
                VeiculoRepositorio.AdicionarVeiculo(veiculo);

                Console.WriteLine($"A placa informada é válida! \nVeículo {placa} estacionado com sucesso!!");
            }
            else
            {
                Console.WriteLine("A placa informada NÃO é válida!");
            }

            Console.ReadKey();
            Console.Clear();
            Menu.ExibirMenuSecundario();
        }

        public static void ListarVeiculos()
        {
            var veiculos = VeiculoRepositorio.ListarTodosVeiculos();

            if (veiculos.Any())
            {
                Console.Clear();
                Console.WriteLine("Os veículos estacionados são:");

                int contadorVeiculo = 1;
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine($"Vaga {contadorVeiculo} - Veículo de Placa: {veiculo.ToString()} está estacionado!");
                    contadorVeiculo++;
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        public static void ListarVeiculoPorPlaca()
        {
            Console.Clear();
            Console.Write("Digite a placa do veículo estacionado: ");
            string placa = Console.ReadLine().ToUpper().Replace("-", "");

            if (VeiculoRepositorio.ExisteVeiculoPorPlaca(placa))
            {
                Console.WriteLine($"Veículo de Placa: {placa} está estácionado.");
            }
            else
            {
                Console.WriteLine($"O veículo informado NÃO está cadastrado em nosso sistema!");
            }
            Console.ReadKey();
            return;
        }

        public static void RemoverVeiculo()
        {
            Console.Clear();
            Console.Write("Digite a placa do veículo para remover: ");
            string placa = Console.ReadLine().ToUpper().Replace("-", "");

            Estacionamento estacionamento = new Estacionamento(10.50M, 10.50M);

            var veiculo = VeiculoRepositorio.ListarUmVeiculo(placa);

            if (veiculo != null)
            {
                Console.Write("Digite a quantidade de horas que o veículo permaneceu estacionado: ");
                int horas = int.Parse(Console.ReadLine());

                decimal valorTotal = estacionamento.CalculaPrecoInicialMaisPrecoPorHora() * horas;
                VeiculoRepositorio.DeletarVeiculo(placa);

                Console.WriteLine($"O veículo de Placa: {placa}, foi removido e o preço total foi de: R$ {valorTotal.ToString("F2")}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }

            Console.ReadKey();
            Console.Clear();
            Menu.ExibirMenuSecundario();
        }

        public static bool ValidarPlaca(string placa)
        {
            if (string.IsNullOrWhiteSpace(placa)) { return false; }

            if (placa.Length > 7 || placa.Length < 7) { return false; }

            if (char.IsLetter(placa, 4))
            {
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                return padraoMercosul.IsMatch(placa);
            }
            else
            {
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                return padraoNormal.IsMatch(placa);
            }
        }
    }
}