/*
Dado um arquivo LogCombustivel.csv com registro de veículos e abastecimentos de 
combustível, desenvolver um programa que interpreteo arquivo de entrada e produza um 
arquivo de saída de nome RelatorioConsumo.csv, que calcule, por veículo, os seguintes 
valores:
•  A quilometragem total no período (Km) 
•  O custo total com combustível no período (R$) 
•  O consumo total de combustível no período (Litros) 
•  Data inicial do período e total de dias 
•  O consumo médio de combustível (Km/L) 
•  O melhor e o pior registro de consumo (Km/L) 
•  O custo médio por quilômetro rodado (R$ / Km)

Author: Fabian Corrêa Marques - http://correamarques.com.br/
 
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;

namespace ControleDeGastos
{
	class Program
	{
		static void Main(string[] args)
		{
			//Solicitar ao usuário depois
			string caminhoArquivo = String.Empty;

#if DEBUG
			caminhoArquivo = @"C:\temp\gastos\LogCombustivel.csv"; 
#endif

			if (string.IsNullOrEmpty(caminhoArquivo))
				Console.WriteLine("Caminho para o arquivo não foi informado.");

			Controle control = new Controle();
			control.Carregar(caminhoArquivo);

			//Pausa para informar o usuário sobre os acontecimentos do sistema.
			Console.Read();
		}

	}
}
