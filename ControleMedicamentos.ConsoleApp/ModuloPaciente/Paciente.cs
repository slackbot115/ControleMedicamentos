using ControleMedicamentos.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.ConsoleApp.ModuloPaciente
{
    public class Paciente : EntidadeBase
    {
        private string _nome;
        private string _cpf;
        private string _logradouro;

        public Paciente(string nome, string cpf, string logradouro)
        {
            _nome = nome;
            _cpf = cpf;
            _logradouro = logradouro;
        }

        public string Nome { get { return _nome; } }

        public string Cpf { get => _cpf; set => _cpf = value; }

        public string Logradouro { get => _logradouro; set => _logradouro = value; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "CPF: " + Cpf + Environment.NewLine +
                "Logradouro: " + Logradouro + Environment.NewLine;
        }

    }
}
