using ControleMedicamentos.ConsoleApp.Compartilhado;
using ControleMedicamentos.ConsoleApp.ModuloFuncionario;
using ControleMedicamentos.ConsoleApp.ModuloMedicamento;
using ControleMedicamentos.ConsoleApp.ModuloRequisicao;
using System;

namespace ControleMedicamentos.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Notificador notificador = new Notificador();
            TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                {
                    ITelaCadastravel telaCadastravel = (ITelaCadastravel)telaSelecionada;

                    if (opcaoSelecionada == "1")
                        telaCadastravel.Inserir();

                    else if (opcaoSelecionada == "2")
                        telaCadastravel.Editar();

                    else if (opcaoSelecionada == "3")
                        telaCadastravel.Excluir();

                    else if (opcaoSelecionada == "4")
                        telaCadastravel.VisualizarRegistros("Tela");
                }
                else if (telaSelecionada is TelaCadastroMedicamento)
                    GerenciarCadastroMedicamentos(telaSelecionada, opcaoSelecionada);
                else if (telaSelecionada is TelaCadastroRequisicao)
                    GerenciarCadastroRequisicoes(telaSelecionada, opcaoSelecionada);
            }
        }

        private static void GerenciarCadastroMedicamentos(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroMedicamento telaCadastroMedicamento = telaSelecionada as TelaCadastroMedicamento;

            if (telaCadastroMedicamento is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroMedicamento.Inserir();

            else if (opcaoSelecionada == "2")
                telaCadastroMedicamento.Editar();

            else if (opcaoSelecionada == "3")
                telaCadastroMedicamento.Excluir();

            else if (opcaoSelecionada == "4")
                telaCadastroMedicamento.VisualizarRegistros("Tela");

            else if (opcaoSelecionada == "5")
                telaCadastroMedicamento.VisualizarMedicamentosComBaixoEstoque();
        }

        private static void GerenciarCadastroRequisicoes(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroRequisicao telaCadastroRequisicoes = telaSelecionada as TelaCadastroRequisicao;

            if (telaCadastroRequisicoes is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroRequisicoes.Inserir();

            else if (opcaoSelecionada == "2")
                telaCadastroRequisicoes.Editar();

            else if (opcaoSelecionada == "3")
                telaCadastroRequisicoes.Excluir();

            else if (opcaoSelecionada == "4")
                telaCadastroRequisicoes.VisualizarRegistros("Tela");

            else if (opcaoSelecionada == "5")
                telaCadastroRequisicoes.VisualizarMedicamentosAgrupados();
        }

    }
}
