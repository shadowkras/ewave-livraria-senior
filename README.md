# ewave-livraria-senior

﻿<p>Este projeto é uma prova de aptidão realizada pela Ewave do Brasil (<a href="https://github.com/shadowkras/ewave-livraria-senior/issues/1">ver issue</a>) Este projeto é a <b>Biblioteca Virtual</b>. Um projeto de uma livraria utilizando Asp.NET Core, com EntityFramework e utilizando arquitetura DDD (Domain-Driven Design).</p>
<br />
<p>Ela foi construída por <a href="emailto:lsr.sena@gmail.com">Leonardo Sena</a>, e pode ser encontrado no <a href="https://github.com/shadowkras/ewave-livraria-senior/">Github</a> para download.</p>

<p>O projeto foi divido em três camadas, <b>Presentation</b> (views, arquivos estáticos e controllers), <b>Application</b> (serviços, view models e mensagens para o usuário) e por fim, <b>Data</b> (entidades, o mapeamento destas, repositórios, contexto e migrations). Estas camadas seguem os princípios do DDD, com a informação navegando da view, para os serviços, para os repositórios, e finalmente chegando ao banco de dados.</p>

<p>A aplicação é bem simples, e constitui de quatro cadastros com a finalidade do usuário ter uma lista de livros para pesquisar por titulo e autor. O usuario primeiramente cadastra as editoras, autores e categorias, e em seguida, utiliza estas informações para cadastrar os livros.</p>

<p>A aplicação de apresentação utiliza bootstrap 4.0, vue.js para realizar o two-way databind no componente da grade de livros e nos componentes que populam os elementos de select no cadastro de livros. Por fim, todos os arquivos estáticos estão utilizando cache para melhor desempenho. O Vue foi escolhido pela sua simplicidade e tamanho reduzido, atendendo a necessidade básica de realizar o binding com os elementos com pouco código javascript. E por fim, também foi criado um componente para enviar a imagem com a capa do livro para o servidor.</p>
<p>A aplicação de serviços constitui no meio-termo entre as controllers e os repositórios, utilizando de uma DTO (data transfer object) para transportar essas informações dos repositórios para as controllers na primeira camada.</p>
<p>Por fim, a aplicação de dados utiliza de classes para realizar o mapeamento das tabelas com o EntityFramework. E disponibiliza repositórios para serem utilizados pelas camadas superiores, por meio de interfaces. Foi criado um repositório base para centralizar os métodos mais utilizados pelos repositórios, para atender os princípios do DRY (don't repeat yourself).</p>

<P>Sobre os requerimentos do projeto:</p>
<ul>.NET Core. A aplicação foi toda desenvolvida utilizando .NET Core.</li>
<li>Testes de Unidade: A solução possui um projeto com testes unitários das controllers criadas pela aplicação.</li>
<li>Conteinerização: A aplicação funciona em docker. Apesar de não conseguir realizar todas as configurações, ela levanta no docker desktop e se comunica com um banco em conteiner no Google Cloud. Ela também pode ser publicada em um container em kubernetes do Google Clouds, mas esbarrei em um problema dop cabeçalho encriptados dos requests de login não serem aceitos entre o container do banco e da aplicação, e apesar de ter tentado algumas soluções para corrigir, acredito que apenas mudando o tipo do container no google (flex) para uma maquina virtual resolve este problema.</li>
<li>REST: A aplicação possui endpoints REST.</li>
<li>Mensageria</li>
<li>Serviços de Cache: A aplicação utiliza-se de três tipos de cacheamento. Cacheamento de views, de respostas e de memória (IMemoryCache da Microsoft).
<li>Kubernetes: A aplicação roda em um containers em kubernetes no Google Cloud.</li>
<li>DDD: A aplicação foi arquitetada utilizando a estrutura DDD.</li>
<li>CQRS: A aplicação utiliza a arquitetura CQRS em um dos seus serviços, que centraliza a conexão com mais de um repositório para devolver dados de ambos em uma única resposta aos endpoints.</li>
 </ul>

<p>O que ficou pendente (TO-DO):</p>
<ul>
<li>Frontend em angular. Eu cheguei a criar o projeto do front-end, mas não conseguiria entregar algo apresentável no tempo que foi dado.</li>
 </ul>
