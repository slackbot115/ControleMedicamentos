using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloPaciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloRequisicao
{
    public class TelaCadastroRequisicao : TelaBase
    {
        private RepositorioRequisicao repositorioRequisicao;
        private Notificador notificador;

        private TelaCadastroPaciente telaCadastroPaciente;
        private RepositorioPaciente repositorioPaciente;

        private TelaCadastroMedicamento telaCadastroMedicamento;
        private RepositorioMedicamento repositorioMedicamento;

        public TelaCadastroRequisicao(TelaCadastroPaciente telaCadastroPaciente, 
            RepositorioPaciente repositorioPaciente,
            TelaCadastroMedicamento telaCadastroMedicamento,
            RepositorioMedicamento repositorioMedicamento,
            RepositorioRequisicao repositorioRequisicao, 
            Notificador notificador)
            : base("Cadastro de Requisição")
        {
            this.telaCadastroPaciente = telaCadastroPaciente;
            this.repositorioPaciente = repositorioPaciente;
            this.telaCadastroMedicamento = telaCadastroMedicamento;
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioRequisicao = repositorioRequisicao;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar medicamentos agrupados");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void Inserir()
        {
            MostrarTitulo("Inserindo nova requisição");

            Paciente pacienteSelecionado = ObterPaciente();

            Medicamento medicamentoSelecionado = ObterMedicamento();

            if (pacienteSelecionado == null || medicamentoSelecionado == null)
            {
                notificador
                    .ApresentarMensagem("Cadastre um paciente e um medicamento antes de cadastrar requisições!", TipoMensagem.Atencao);
                return;
            }

            Console.Write($"Digite a quantidade de caixas que deseja retirar de {medicamentoSelecionado.Nome}: ");
            int qtdCaixasRetiradas = int.Parse(Console.ReadLine());
            medicamentoSelecionado.AlterarQuantidade(qtdCaixasRetiradas);

            Requisicao novaRequisicao = ObterRequisicao(pacienteSelecionado, medicamentoSelecionado);

            string statusValidacao = repositorioRequisicao.Inserir(novaRequisicao);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Requisição inserida com sucesso", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Requisição");

            bool temRequisicoesCadastradas = VisualizarRegistros("Pesquisando");

            if (temRequisicoesCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma requisição cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            Console.WriteLine();

            Paciente pacienteSelecionado = ObterPaciente();

            Medicamento medicamentoSelecionado = ObterMedicamento();

            Requisicao requisicaoAtualizada = ObterRequisicao(pacienteSelecionado, medicamentoSelecionado);

            bool conseguiuEditar = repositorioRequisicao.Editar(x => x.id == numeroRequisicao, requisicaoAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Requisição editada com sucesso", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Requisição");

            bool temRequisicoesCadastradas = VisualizarRegistros("Pesquisando");

            if (temRequisicoesCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma requisição cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroRequisicao = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioRequisicao.Excluir(x => x.id == numeroRequisicao);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Requisição excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Revistas");

            List<Requisicao> requisicoes = repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma requisição disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Requisicao requisicao in requisicoes)
                Console.WriteLine(requisicao.ToString());

            Console.ReadLine();

            return true;
        }

        public void VisualizarMedicamentosAgrupados()
        {
            List<Requisicao> requisicoes = repositorioRequisicao.SelecionarTodos();

            if (requisicoes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma requisição disponível", TipoMensagem.Atencao);
                return;
            }

            List<Requisicao> requisicoesOrdenadas = new List<Requisicao>(requisicoes);

            requisicoesOrdenadas.Sort((a,b) => b.medicamento.QtdRequisitos.CompareTo(a.medicamento.QtdRequisitos));

            foreach (Requisicao req in requisicoesOrdenadas)
            {
                Console.WriteLine($"{req.medicamento.Nome} foi retirado {req.medicamento.QtdRequisitos} vezes");
            }

            Console.ReadKey();

        }

        private Paciente ObterPaciente()
        {
            bool temPacientesDisponiveis = telaCadastroPaciente.VisualizarRegistros("");

            if (!temPacientesDisponiveis)
            {
                notificador.ApresentarMensagem("Você precisa cadastrar um paciente antes de uma requisição!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do paciente: ");
            int numPacienteSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistro(x => x.id == numPacienteSelecionado);

            return pacienteSelecionado;
        }

        private Medicamento ObterMedicamento()
        {
            bool temMedicamentosDisponiveis = telaCadastroMedicamento.VisualizarRegistros("");

            if (!temMedicamentosDisponiveis)
            {
                notificador.ApresentarMensagem("Você precisa cadastrar um medicamento antes de uma requisição!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do medicamento: ");
            int numMedicamentoSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Medicamento medicamentoSelecionado = repositorioMedicamento.SelecionarRegistro(x => x.id == numMedicamentoSelecionado);

            return medicamentoSelecionado;
        }

        private Requisicao ObterRequisicao(Paciente pacienteSelecionado, Medicamento medicamentoSelecionado)
        {
            //verificar se o medicamento é possivel de ser retirado
            bool status = true;

            DateTime dataRequisicao = DateTime.Now;
            
            Requisicao novaRequisicao = new Requisicao(pacienteSelecionado, medicamentoSelecionado, status, dataRequisicao);

            return novaRequisicao;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da Requisição que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioPaciente.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID da Requisição não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}
