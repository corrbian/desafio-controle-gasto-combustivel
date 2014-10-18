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
namespace ControleDeGastos
{
	public class Abastatecimento
	{
		/// <summary>
		/// Quantidade de litros que foi abastecido
		/// </summary>
		public float Combustivel { get; set; }
		/// <summary>
		/// Data que foi realizado o abastecimento
		/// </summary>
		public DateTime Data { get; set; }
		/// <summary>
		/// Valor total do abastecimento
		/// </summary>
		public decimal Preco { get; set; }
		/// <summary>
		/// Quilometragem que o veículo estava ao ser abastecido
		/// </summary>
		public float Quilometragem { get; set; }
	}
}
