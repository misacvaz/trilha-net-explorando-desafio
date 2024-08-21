using System.Text;
using DesafioProjetoHospedagem.Models;

namespace DesafioProjetoHospedagem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Pessoa> hospedes = new List<Pessoa>();
            Reserva reserva = new Reserva();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t ___________________________________");
                Console.WriteLine("\t\t\t\t\t | Menu de Hospedagem de Hotel:     |");
                Console.WriteLine("\t\t\t\t\t |__________________________________|");
                Console.WriteLine("\t\t\t\t\t | 1 - Cadastrar Suíte              |");
                Console.WriteLine("\t\t\t\t\t | 2 - Cadastrar Hóspedes           |");
                Console.WriteLine("\t\t\t\t\t | 3-  Obter Quantidade de Hóspedes |");
                Console.WriteLine("\t\t\t\t\t | 4 - Calcular o Valor da Diária   |");
                Console.WriteLine("\t\t\t\t\t | 5 - Sair                         |");
                Console.WriteLine("\t\t\t\t\t | Escolha uma opção:               |");
                Console.WriteLine("\t\t\t\t\t |__________________________________|");


                string opcao = Console.ReadLine();

                switch (opcao)
                {

                    case "1":
                        CadastrarSuite(reserva);
                        break;

                    case "2":
                        CadastrarHospedes(reserva, hospedes);
                        break;
                    
                    case "3":

                     try {

                        // Esto Verefica se há Hóspedes Cadastrados
                        if(reserva.Hospedes == null || reserva.Hospedes.Count == 0)
                        {
                            Console.WriteLine("Não Exeste hóspede cadastrado. Ou seja cadastre o hóspede.\n");
                        }
                        else{

                                //Esto Mostra  a Quatidade de Hospedes Cadastrados
                                Console.WriteLine($"Quantidade de hóspedes: {reserva.ObterQuantidadeHospedes()}\n");

                                // Mostra os nomes e Sobrenomes dos hóspedes
                                Console.WriteLine("Lista dos Hóspedes");

                                foreach(var hospede in reserva.Hospedes)
                                {
                                    Console.WriteLine($"Nome: {hospede.Nome} {hospede.Sobrenome}");
                                }



                        }


                     } catch (Exception ex)
                     {
                        Console.WriteLine($"Erro ao Oter a lista de Hśopedes: {ex.Message}\n");

                     }
                       
                        Console.WriteLine("\n Pressione qualquer tecla para continuar....");
                        Console.ReadKey();
                        break;
                    case "4":
                        if (reserva.Suite != null)
                        {
                            // Solicitando o número de dias de reservados
                            Console.Write("Digite o número de dias reservados: ");
                            if (int.TryParse(Console.ReadLine(), out int diasReservados))
                            {
                                reserva.DiasReservados = diasReservados;
                                decimal valorDiaria = reserva.CalcularValorDiaria();
                                if(reserva.DiasReservados >= 10){
                                    //reserva com desconto
                                    Console.WriteLine($" O Valor da Reserva é De : {valorDiaria:C} Por isso você teve um desconto de 10% \n");
                                    
                                }else{
                                    //reserva sem desconto
                                Console.WriteLine($" O Valor da Reserva é De : {valorDiaria:C} \n");
                                
                                }
                            }
                            else
                            {
                                Console.WriteLine("Número de dias inválido. tem que ser um Número");
                            }
                        }
                        else
                        {
                            Console.WriteLine("A suíte não está cadastrada.\n");
                        }

                        Console.WriteLine("\n Pressione qualquer tecla para continuar....");
                        Console.ReadKey();
                        break;

                    case "5":
                        return; // Sair do programa

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.\n");

                        Console.WriteLine("Pressione qualquer tecla para continuar....");
                        Console.ReadKey();
                        break;
                }
            }
        }



        static void CadastrarSuite(Reserva reserva)
        {
            Console.Write("Digite o tipo da suíte: ");
            string tipoSuite = Console.ReadLine();

            Console.Write("Digite a capacidade da suíte: ");
            if (int.TryParse(Console.ReadLine(), out int capacidade))
            {
                Console.Write("Digite o valor da diária: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal valorDiaria))
                {
                    Suite suite = new Suite
                    {
                        TipoSuite = tipoSuite,
                        CapaCidade = capacidade,
                        ValorDiaria = valorDiaria
                    };

                    reserva.CadastrarSuite(suite);
                    Console.WriteLine("Suíte cadastrada com sucesso.\n");
                   
                }
                else
                {
                    Console.WriteLine("Valor da diária inválido. tem que ser um Número.\n");
                    
                }
            }
            else
            {
                Console.WriteLine("Capacidade inválida. \n");
               
            }
             Console.WriteLine("\n Pressione qualquer tecla para continuar....");
            Console.ReadKey();
        }


        static void CadastrarHospedes(Reserva reserva, List<Pessoa> hospedes)
        {

            // Esto Verifica se Suíte foi cadastrado
            if(reserva.Suite == null)
            {
                 Console.WriteLine("Nenhuma suíte foi cadastrada. Cadastre uma suíte antes de adicionar hóspedes.\n");
                 Console.WriteLine("Pressione qualquer tecla para continuar...");
                 Console.ReadKey();
                 return;
            }


            Console.Write("Digite o número de hóspedes: ");
            if (int.TryParse(Console.ReadLine(), out int numeroHospedes) && numeroHospedes > 0)

            {
                hospedes.Clear();
                for (int i = 0; i < numeroHospedes; i++)
                {
                    Console.Write($"Digite o nome do hóspede {i + 1}: ");
                    string nome = Console.ReadLine();
                    Console.Write($"Digite o sobrenome do hóspede {i + 1}: ");
                    string sobrenome = Console.ReadLine();
                    // adicina pessoa pelo nome e sobrenome
                    hospedes.Add(new Pessoa(nome: nome, sobrenome: sobrenome));
                }

                try
                {
                    //faz cadastro de Hóspede na reserva
                    reserva.CadastrarHospedes(hospedes);
                    Console.WriteLine("\n Hóspedes cadastrados com sucesso.\n");
                
                    
                }
                catch (Exception ex)
                {
                    
                    // Isso mostra mesagem de erro se o número de h+ospede for superior à capasidade do Suíte
                    Console.WriteLine($"Erro em cadastar Hóspedes, número de hóspede não pode ser superior a capacidade do suíte: {ex.Message}\n");
                   
                }
            }
            else
            {
                Console.WriteLine("Número de hóspedes inválido. tem que ser um número positivo\n ");
               
            }

             Console.WriteLine("\n Pressione qualquer tecla para continuar....");
             Console.ReadKey();
        }

        
    }
}
