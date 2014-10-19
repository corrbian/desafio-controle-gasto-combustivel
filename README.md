Desafio de Programação – Cálculo de Consumo de Combustível
Dado um arquivo LogCombustivel.csv com registro de veículos e abastecimentos de
combustível, desenvolver um programa que interprete o arquivo de entrada e produza um
arquivo de saída de nome RelatorioConsumo.csv, que calcule, por veículo, os seguintes
valores:<br />
•  A quilometragem total no período (Km)<br />
•  O custo total com combustível no período (R$)<br />
•  O consumo total de combustível no período (Litros)
•  Data inicial do período e total de dias
•  O consumo médio de combustível (Km/L)
•  O melhor e o pior registro de consumo (Km/L)
•  O custo médio por quilômetro rodado (R$ / Km)
O arquivo CSV de entrada (LogCombustivel.csv), localizado na mesma pasta do programa,
possui uma linha de cabeçalho e N linhas de registro de combustível, no seguinte formato:
"MARCA","MODELO","DATA","QUILOMETRAGEM","COMBUSTIVEL","PRECO"
"Honda","City 1.5","2012-09-13","43705","38.020","2.977"
"Honda","City 1.5","2012-09-24","43999","37.690","2.999"
"Honda","City 1.5","2012-10-05","44269","36.020","2.999"
Observações:
• Os abastecimentos são todos totais (completam o tanque do veículo)
• A quilometragem é o valor registrado no odômetro do veículo no momento do
abastecimento
• O arquivo de entrada não está necessariamente ordenado
O arquivo CSV de saída (RelatorioConsumo.csv), a ser produzido na mesma pasta do
programa, possui uma linha de cabeçalho e N linhas de relatório de consumo, no seguinte
formato:
"MARCA","MODELO","KM","R$","LITROS","DATAINI","DIAS","MEDIAKM/L","PIORKM/L","MELHORKM/L","R$/KM"
"Honda","City 1.5","564","221.06","73.71","2012-09-13","22","7.65","7.50","7.80","0.39"
O arquivo LogCombustivel.csv enviado como modelo contém um cenário de teste do
programa. Durante a avaliação do programa, outros arquivos de teste poderão ser
utilizados para garantir o correto funcionamento.
