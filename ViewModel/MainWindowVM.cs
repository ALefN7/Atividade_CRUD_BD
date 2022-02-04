using Atividade1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atividade1
{
    public class MainWindowVM : BaseNotify
    {
        public ObservableCollection<Ser> listaSeres { get; private set; }
        public Ser SerSelecionado { get; set; }

        public ICommand criarComando { get; private set; }
        public ICommand editarComando { get; private set; }
        public ICommand removerComando { get; private set; }

        private Conexao conexao = new Conexao();
        public MainWindowVM()
        {
            try
            {
                listaSeres = new ObservableCollection<Ser>(conexao.Select());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            listaSeres = new ObservableCollection<Ser>();
            IniciaComandos();
        }

        public void IniciaComandos()
        {
            criarComando = new RelayCommand( (object _) => {
                Ser cadastrarSer = new Ser();
                CriarWindow telaCria = new CriarWindow();

                telaCria.DataContext = cadastrarSer;
                telaCria.ShowDialog();
                if (telaCria.DialogResult == true)
                {
                    try
                    {
                        conexao.Insert(cadastrarSer);
                        listaSeres.Add(cadastrarSer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });


            editarComando = new RelayCommand((object _) => {
                Ser serTemp = new Ser();
                EditarWindow telaEdita = new EditarWindow();

                serTemp.Nome = SerSelecionado.Nome;
                serTemp.Nascimento = SerSelecionado.Nascimento;
                serTemp.Sexo = SerSelecionado.Sexo;
                serTemp.Raça = SerSelecionado.Raça;

                telaEdita.DataContext = serTemp;
                telaEdita.ShowDialog();
                if (telaEdita.DialogResult == true)
                {                    
                    try
                    {
                        conexao.Update(serTemp); 
                        SerSelecionado.Nome = serTemp.Nome;
                        SerSelecionado.Nascimento = serTemp.Nascimento;
                        SerSelecionado.Sexo = serTemp.Sexo;
                        SerSelecionado.Raça = serTemp.Raça;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }                    
                }
            }, (object _) => {
                return SerSelecionado != null;
            });

            removerComando = new RelayCommand((object _) => {
                try
                {
                    conexao.Delete(SerSelecionado);
                    listaSeres.Remove(SerSelecionado);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }, (object _) => {
                return listaSeres.Count > 0;
            });
        }
    }
}
