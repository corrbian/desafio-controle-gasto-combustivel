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
					Dias = BuscaIntervaloDias(veiculo.Abastecimentos),
				};
				listaDeConsumo.Add(consumo);
			}

			GerarRelatorio(listaDeConsumo);
		}

		void GerarRelatorio(IList<Consumo> listaDeConsumo)
		{
			Console.WriteLine("MARCA\t\tMODELO\t\tKM\t\tR$\t\tLITROS\tDATAINI\tDIAS\tMEDIAKM/L\t\tPIORKM/L\t\tMELHORKM/L\t\tR$/KM");
			foreach (Consumo consumo in listaDeConsumo)
			{
				StringBuilder relatorio = new StringBuilder();
				relatorio.AppendFormat("\"{0}\",", consumo.Marca);
				relatorio.AppendFormat("\"{0}\",", consumo.Modelo);
				relatorio.AppendFormat("\"{0}\",", consumo.KM);
				relatorio.AppendFormat("\"{0}\",", consumo.ValorGasto);
				relatorio.AppendFormat("\"{0}\",", consumo.Litros);
				relatorio.AppendFormat("\"{0}\",", consumo.DataInicial);
				relatorio.AppendFormat("\"{0}\",", consumo.Dias);
				relatorio.AppendFormat("\"{0}\",", consumo.MediaKmL);
				relatorio.AppendFormat("\"{0}\",", consumo.PiorKmL);
				relatorio.AppendFormat("\"{0}\",", consumo.MelhorKmL);
				relatorio.AppendFormat("\"{0}\"", consumo.ValorGastoKmL);
				Console.WriteLine(relatorio.ToString());
			}
			//"Honda","City 1.5","564","221.06","73.71","2012-09- 13","22","7.65","7.50","7.80","0.39"


			//System.Console.WriteLine(veiculo.Marca + " " + veiculo.Modelo + " abatecimentos: " + veiculo.Abastecimentos.Count);
		}

		DateTime BuscaPrimeiroAbastecimento(IList<Abastatecimento> abastecimentos)
		{
			return abastecimentos.OrderBy(w => w.Data).FirstOrDefault().Data;
		}
		int BuscaIntervaloDias(IList<Abastatecimento> abastecimentos)
		{
			DateTime dataInicial = BuscaPrimeiroAbastecimento(abastecimentos);
			
			int totalDays = 0;
			foreach (Abastatecimento item in abastecimentos)
			{
				if (item.Data > dataInicial)
					totalDays = (item.Data - dataInicial).Days;
			}
			return totalDays;
		}
	}
}
