using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;

namespace XmlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            const string clienteFile = @"..\..\..\Cliente.xml";
            XDocument xmlDocument = XDocument.Load(clienteFile);

            // Buscando elementos específicos de um xml
            Console.WriteLine("Imprimindo elementos do xml:" + Environment.NewLine);
            Console.WriteLine("Nome: " + xmlDocument.Element("Cliente").Element("Nome").Value);
            Console.WriteLine("CPF: " + xmlDocument.Element("Cliente").Element("Cpf").Value);

            // Realizando o parse de um único elemento
            Cliente cliente = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Cliente));
            cliente = (Cliente)serializer.Deserialize(xmlDocument.CreateReader());
            Console.WriteLine(Environment.NewLine + "Imprimindo cliente:" + Environment.NewLine);
            Console.WriteLine(cliente);

            // Realizando o parse de uma lista de elementos
            const string clientesFile = @"..\..\..\Clientes.xml";
            XDocument clientesXmlDocument = XDocument.Load(clientesFile);

            // Estrutura e funcionamento semelhante a um foreach
            List<Cliente> clientes = (from xml in clientesXmlDocument.Elements("Clientes").Elements("Cliente")
                                      // Algo semelhante a uma consulta SQL, pode ser realizado wheres, groupby, orderby, joins
                                      select new Cliente
                                      {
                                          Id = long.Parse(xml.Element("Id").Value),
                                          Nome = xml.Element("Nome").Value,
                                          Cpf = xml.Element("Cpf").Value,
                                          Rg = xml.Element("Rg").Value,
                                          DataNascimento = xml.Element("DataNascimento").Value,
                                          Sexo = xml.Element("Sexo").Value,
                                          Email = xml.Element("Email").Value,
                                          Celular = xml.Element("Celular").Value,
                                          Endereco = new Endereco
                                          {
                                              Cep = xml.Element("Endereco").Element("Cep").Value,
                                              Rua = xml.Element("Endereco").Element("Rua").Value,
                                              Numero = int.Parse(xml.Element("Endereco").Element("Numero").Value),
                                              Bairro = xml.Element("Endereco").Element("Bairro").Value,
                                              Cidade = xml.Element("Endereco").Element("Cidade").Value,
                                              Estado = xml.Element("Endereco").Element("Estado").Value,
                                          }
                                      }).ToList();

            Console.WriteLine(Environment.NewLine + "Imprimindo a lista de clientes:" + Environment.NewLine);
            foreach (var item in clientes) Console.WriteLine(item + Environment.NewLine);
        }
    }

    public class Endereco
    {
        public string Cep { get; set; }
        public string Rua { get; set; }
        public long Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public override string ToString()
        {
            return "Endereço: " + Cep + " - " + Rua + ", " + Numero + ", " + Bairro + ", " + Cidade + " - " + Estado;
        }
    }

    public class Cliente
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public Endereco Endereco { get; set; }

        public override string ToString()
        {
            return "Cliente " + Id + ":" + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "CPF: " + Cpf + Environment.NewLine +
                "Rg: " + Rg + Environment.NewLine +
                "Data de Nascimento: " + DataNascimento + Environment.NewLine +
                "Sexo: " + Sexo + Environment.NewLine +
                "Email: " + Email + Environment.NewLine +
                "Celular: " + Celular + Environment.NewLine +
                "Endereço: " + Endereco;
        }
    }
}
