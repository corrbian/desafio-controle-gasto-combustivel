using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeGastos
{
	public class Gasto
	{
		public IList<Veiculo> ImportaDados(string caminhoArquivo)
		{
			List<string> linhas = System.IO.File.ReadAllLines(caminhoArquivo, Encoding.UTF8).ToList<string>();
			#region Informa o usuário se tem registros o arquivo
			if (linhas.Count == 0)
			{
				Console.WriteLine("Arquivo sem conteúdo.");
				return null;
			} 
			#endregion

			List<Veiculo> veiculoLst = new List<Veiculo>();
			int registrosDeVeiculosNaoImportados = 0;

			//Ignorar a primeira linha, pois é o cabeçalho
			for (int linhaAtual = 1; linhaAtual < linhas.Count; linhaAtual++)
			{// Divide a linha em várias colunas (Comma Separated Values)
				string[] coluna = linhas[linhaAtual].Split(',');
				#region Verifica se o registro está de acordo com o layout
				if (coluna.Length != 6)
				{// registro não estava condizente com o layout - informar ao usuário
					registrosDeVeiculosNaoImportados++;
					continue;
				}
				#endregion

				//Verifica se o veículo já foi importado
				Veiculo veiculo = veiculoLst.FirstOrDefault(w => w.Marca == RemoveAspas(coluna[0]) && w.Modelo == RemoveAspas(coluna[1]));
				if (veiculo == null)
				{// Não foi importado registro deste veículo ainda
					//Instancia os objetos a serem utilizados
					veiculo = new Veiculo();
					veiculo.Abastecimentos = new List<Abastatecimento>();
					//Lê os registros
					veiculo.Marca = RemoveAspas(coluna[0]);
					veiculo.Modelo = RemoveAspas(coluna[1]);
					veiculo.Abastecimentos.Add(LeDadosAbastecimento(coluna));
					// Adiciona o veículo a lista
					veiculoLst.Add(veiculo);
				}// já foi importado, apenas lê os dados do abastecimento
				else veiculo.Abastecimentos.Add(LeDadosAbastecimento(coluna));
			}

			if (registrosDeVeiculosNaoImportados == 1)
				Console.WriteLine("Não foi importado 1 registro.");
			else if (registrosDeVeiculosNaoImportados > 1)
				Console.WriteLine(String.Format("Não foram importados {0} registros.", registrosDeVeiculosNaoImportados));

			return veiculoLst;
		}

		Abastatecimento LeDadosAbastecimento(string[] coluna)
		{
			System.Globalization.NumberStyles numberStyle = System.Globalization.NumberStyles.AllowDecimalPoint;
			return new Abastatecimento()
			{
				Combustivel = float.Parse(RemoveAspas(coluna[4]), numberStyle, System.Globalization.CultureInfo.InvariantCulture),
				Data = DateTime.Parse(RemoveAspas(coluna[2])),
				Preco = Decimal.Parse(RemoveAspas(coluna[5]), numberStyle, System.Globalization.CultureInfo.InvariantCulture),
				Quilometragem = float.Parse(RemoveAspas(coluna[3]), numberStyle, System.Globalization.CultureInfo.InvariantCulture),
			};
		}

		string RemoveAspas(string conteudo)
		{
			return conteudo.Replace("\"", "");
		}
	}
}
