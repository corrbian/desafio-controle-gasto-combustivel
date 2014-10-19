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
	public class Controle
	{
		public void Carregar(string caminhoArquivo)
		{
			Gasto controleDeGasto = new Gasto();
			IList<Veiculo> listaDeVeiculos = controleDeGasto.ImportaDados(caminhoArquivo);

			List<Consumo> listaDeConsumo = new List<Consumo>();
			foreach (Veiculo veiculo in listaDeVeiculos)
			{

				Consumo consumo = new Consumo()
				{
					Marca = veiculo.Marca,
					Modelo = veiculo.Modelo,
					DataInicial = BuscaPrimeiroAbastecimento(veiculo.Abastecimentos),
					Dias = CalculaIntervaloDias(veiculo.Abastecimentos),
					KM = CalculaKmsPercorridos(veiculo.Abastecimentos),
					Litros = BuscaQuantidadeLitrosAbastecidos(veiculo.Abastecimentos),
					ValorGasto = CalculaValorGasto(veiculo.Abastecimentos),
					MelhorKmL = CalculaMelhorKmL(veiculo.Abastecimentos),
					PiorKmL = CalculaPiorKmL(veiculo.Abastecimentos),
				};
				consumo.MediaKmL = consumo.KM / consumo.Litros;
				consumo.ValorGastoKmL = float.Parse(consumo.ValorGasto.ToString()) / consumo.KM;
				listaDeConsumo.Add(consumo);
			}

			GerarRelatorio(listaDeConsumo);
		}

		void GerarRelatorio(IList<Consumo> listaDeConsumo)
		{
			try
			{
				StringBuilder relatorioConsumo = new StringBuilder();
				relatorioConsumo.AppendLine("\"MARCA\",\"MODELO\",\"KM\",\"R$\",\"LITROS\",\"DATAINI\",\"DIAS\",\"MEDIAKM/L\",\"PIORKM/L\",\"MELHORKM/L\",\"R$/KM\"");
				foreach (Consumo consumo in listaDeConsumo)
				{
					StringBuilder relatorio = new StringBuilder();
					relatorio.AppendFormat("\"{0}\",", consumo.Marca);
					relatorio.AppendFormat("\"{0}\",", consumo.Modelo);
					relatorio.AppendFormat("\"{0}\",", consumo.KM);
					relatorio.AppendFormat("\"{0}\",", consumo.ValorGasto);
					relatorio.AppendFormat("\"{0}\",", consumo.Litros);
					relatorio.AppendFormat("\"{0}\",", consumo.DataInicial.ToString("yyyy-MM-dd"));
					relatorio.AppendFormat("\"{0}\",", consumo.Dias);
					relatorio.AppendFormat("\"{0}\",", consumo.MediaKmL);
					relatorio.AppendFormat("\"{0}\",", consumo.PiorKmL);
					relatorio.AppendFormat("\"{0}\",", consumo.MelhorKmL);
					relatorio.AppendFormat("\"{0}\"", consumo.ValorGastoKmL);
					relatorioConsumo.AppendLine(relatorio.ToString());
				}
				string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
				string filePath = string.Format(@"{0}\RelatorioConsumo.csv", folder);
				System.IO.File.WriteAllText(filePath, relatorioConsumo.ToString());
				Console.WriteLine("Documento exportado com sucesso.");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		DateTime BuscaPrimeiroAbastecimento(IList<Abastecimento> abastecimentos)
		{
			return abastecimentos.OrderBy(w => w.Data).FirstOrDefault().Data;
		}
		int CalculaIntervaloDias(IList<Abastecimento> abastecimentos)
		{
			DateTime dataInicial = BuscaPrimeiroAbastecimento(abastecimentos);
			
			int totalDays = 0;
			foreach (Abastecimento item in abastecimentos)
			{
				if (item.Data > dataInicial)
					totalDays = (item.Data - dataInicial).Days;
			}
			return totalDays;
		}
		float CalculaKmsPercorridos(IList<Abastecimento> abastecimentos)
		{
			float kmInicial = abastecimentos.OrderBy(w => w.Quilometragem).FirstOrDefault().Quilometragem;
			float kmFinal = abastecimentos.OrderByDescending(w => w.Quilometragem).FirstOrDefault().Quilometragem;
			return kmFinal - kmInicial;
		}
		float BuscaQuantidadeLitrosAbastecidos(IList<Abastecimento> abastecimentos)
		{
			float litros = 0;
			foreach (Abastecimento item in abastecimentos)
				litros += item.Combustivel;

			return litros;
		}
		Decimal CalculaValorGasto(IList<Abastecimento> abastecimentos)
		{
			decimal valor = 0;
			foreach (Abastecimento item in abastecimentos)
				valor += (item.Preco * Convert.ToDecimal(item.Combustivel));

			return valor;
		}
		float CalculaMelhorKmL(IList<Abastecimento> abastecimentos)
		{
			float valorAtual = 0;
			float melhorKmL = 0;
			//ordenar por data
			List<Abastecimento> listaDeAbastecimentos = abastecimentos.OrderBy(w => w.Data).ToList<Abastecimento>();
			
			for (int index = 1; index < listaDeAbastecimentos.Count; index++)
			{
				float kmPercorrido = listaDeAbastecimentos[index].Quilometragem - listaDeAbastecimentos[index - 1].Quilometragem;
				valorAtual = kmPercorrido / listaDeAbastecimentos[index - 1].Combustivel;

				if ((melhorKmL == 0) || (valorAtual > melhorKmL))
					melhorKmL = valorAtual;
			}
			return melhorKmL;
		}
		float CalculaPiorKmL(IList<Abastecimento> abastecimentos)
		{
			float valorAtual = 0;
			float piorKmL = 0;
			//ordenar por data
			List<Abastecimento> listaDeAbastecimentos = abastecimentos.OrderBy(w => w.Data).ToList<Abastecimento>();

			for (int index = 1; index < listaDeAbastecimentos.Count; index++)
			{
				float kmPercorrido = listaDeAbastecimentos[index].Quilometragem - listaDeAbastecimentos[index - 1].Quilometragem;
				valorAtual = kmPercorrido / listaDeAbastecimentos[index - 1].Combustivel;

				if ((piorKmL == 0) || (valorAtual < piorKmL))
					piorKmL = valorAtual;
			}
			return piorKmL;
		}
	}
}
