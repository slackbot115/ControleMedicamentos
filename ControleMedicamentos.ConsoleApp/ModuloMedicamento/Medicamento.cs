using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloMedicamento
{
    public class Medicamento : EntidadeBase
    {
        private string _nome;
        private string _descricao;
        private int _quantidade;
        private int _qtdRequisitos;

        public Medicamento(string nome, string descricao, int quantidade)
        {
            _nome = nome;
            _descricao = descricao;
            _quantidade = quantidade;
        }

        public string Nome { get => _nome; }
        public string Descricao { get => _descricao; }
        public int Quantidade { get => _quantidade; }
        public int QtdRequisitos { get => _qtdRequisitos; set => _qtdRequisitos = value; }

        public void SomarQtdRequisitos()
        {
            _qtdRequisitos++;
        }

        public void AlterarQuantidade(int quantidadeRequisitada)
        {
            _quantidade -= quantidadeRequisitada;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Descrição: " + Descricao + Environment.NewLine +
                "Quantidade: " + Quantidade + Environment.NewLine;
        }

    }
}
