using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class TelaCadastroMedicamento : TelaBase
    {
        private RepositorioMedicamento repositorioMedicamento;
        private Notificador notificador;

        public TelaCadastroMedicamento(RepositorioMedicamento repositorioMedicamento, Notificador notificador)
            : base("Cadastro de Medicamentos")
        {
            this.repositorioMedicamento = repositorioMedicamento;
            this.notificador = notificador;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar medicamentos com pouco estoque");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void VisualizarMedicamentosComBaixoEstoque()
        {
            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

            Console.WriteLine("Medicamentos com pouco estoque: ");
            foreach (Medicamento medicamento in medicamentos)
            {
                if (medicamento.Quantidade < 100)
                {
                    /////////////////////////////////////////////////////////////////
                }
            }

            Console.ReadKey();
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Medicamento");

            Medicamento novoMedicamento = ObterMedicamento();

            novoMedicamento.SomarQtdRequisitos();
            repositorioMedicamento.Inserir(novoMedicamento);

            notificador.ApresentarMensagem("Medicamento cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Medicamento");

            bool temMedicamentosCadastrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhum medicamento cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            Medicamento medicamentoAtualizado = ObterMedicamento();

            bool conseguiuEditar = repositorioMedicamento.Editar(numeroMedicamento, medicamentoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Medicamento editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Medicamento");

            bool temMedicamentosRegistrados = VisualizarRegistros("Pesquisando");

            if (temMedicamentosRegistrados == false)
            {
                notificador.ApresentarMensagem("Nenhum medicamento cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroMedicamento = ObterNumeroRegistro();

            bool conseguiuExcluir = repositorioMedicamento.Excluir(numeroMedicamento);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Medicamento excluído com sucesso1", TipoMensagem.Sucesso);
        }



        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Medicamentos");

            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

            if (medicamentos.Count == 0)
            {
                notificador.ApresentarMensagem("Nenhum medicamento disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Medicamento medicamento in medicamentos)
                Console.WriteLine(medicamento.ToString());

            Console.ReadLine();

            return true;
        }

        private Medicamento ObterMedicamento()
        {
            Console.WriteLine("Digite o nome do medicamento: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite a descrição do medicamento: ");
            string descricao = Console.ReadLine();

            Console.WriteLine("Digite a quantidade do medicamento: ");
            int quantidade = int.Parse(Console.ReadLine());

            return new Medicamento(nome, descricao, quantidade);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do Medicamento que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioMedicamento.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    notificador.ApresentarMensagem("ID do Medicamento não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}
