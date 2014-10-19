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

using System.Collections.Generic;
namespace ControleDeGastos
{
	public class Veiculo
	{
		/// <summary>
		/// Marca do veículo
		/// </summary>
		public string Marca { get; set; }
		/// <summary>
		/// Modelo do veículo
		/// </summary>
		public string Modelo { get; set; }
		/// <summary>
		/// Lista de abastecimentos que foi realizado no veículo
		/// </summary>
		public List<Abastecimento> Abastecimentos { get; set; }
	}
}
