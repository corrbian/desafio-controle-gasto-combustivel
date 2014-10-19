/*
Author: Fabian Corrêa Marques - http://correamarques.com.br/

This file is part of ControleDeGastos.

Foobar is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Foobar is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
****************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
					veiculo.Abastecimentos = new List<Abastecimento>();
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

		Abastecimento LeDadosAbastecimento(string[] coluna)
		{
			System.Globalization.NumberStyles numberStyle = System.Globalization.NumberStyles.AllowDecimalPoint;
			return new Abastecimento()
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
