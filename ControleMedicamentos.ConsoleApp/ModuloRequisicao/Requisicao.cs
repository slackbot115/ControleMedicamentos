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
    public class Requisicao : EntidadeBase
    {
        public Paciente paciente;
        public Medicamento medicamento;
        private bool _status;
        private DateTime _dataRequisicao;

        public Requisicao(Paciente paciente, Medicamento medicamento, bool status, DateTime dataRequisicao)
        {
            this.paciente = paciente;
            this.medicamento = medicamento;
            _status = status;
            _dataRequisicao = dataRequisicao;
        }

        public bool Status { get => _status; set => _status = value; }
        public DateTime DataRequisicao { get => _dataRequisicao; set => _dataRequisicao = value; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Paciente: " + paciente.Nome + Environment.NewLine +
                "Medicamento: \n" + 
                "Nome: " + medicamento.Nome + Environment.NewLine +
                "Quantidade: " + medicamento.Quantidade + Environment.NewLine +
                "Status: " + Status + Environment.NewLine + 
                "Data e Hora: " + DataRequisicao + Environment.NewLine;
        }

    }
}
