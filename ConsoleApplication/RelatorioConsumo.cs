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
	public class Consumo
	{
		public string Marca { get; set; }
		public string Modelo { get; set; }
		public string KM { get; set; }
		public string ValorGasto { get; set; }
		public float Litros { get; set; }
		public DateTime DataInicial { get; set; }
		public int Dias { get; set; }
		public float MediaKmL { get; set; }
		public float PiorKmL { get; set; }
		public float MelhorKmL { get; set; }
		public float ValorGastoKmL { get; set; }
	}
}
