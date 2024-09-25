namespace DesafioFundamentos.Models{
    public class Estacionamento{
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora){
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }
        public void AdicionarVeiculo(string placa){
            veiculos.Add(placa);
        }
        public bool RemoverVeiculo(string placa, int horas){
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper())){
                veiculos.Remove(placa);
                decimal valorTotal = precoInicial + (precoPorHora * horas);
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
                return true;
            }else{
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                return false;
            }
        }
        public void ListarVeiculos(){
            if (veiculos.Any()){
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var veiculo in veiculos){
                    Console.WriteLine(veiculo);
                }
            }else{
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
